/*Coded by PrIMD (primd42@gmail.com)*/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;


/// <summary>
/// This class give the possiblity to interact with GUI 4.6 
/// </summary>
public class ScreenToIngameInteraction : MonoBehaviour
{

    public static ScreenToIngameInteraction InstanceInScene;

    
    private float _timeLeftHover;
    private float _timeLeftActive;
    
    public float GetTimeLeft_StayHover() { return _timeLeftHover;}
    public float GetTimeLeft_StayActived() { return _timeLeftActive;}

  
    private bool isAllowToBeActive = true;
    public void SetActive(bool onOff)
    {
        if (!onOff)
            Deactivate();
        isAllowToBeActive = onOff;
    }
    public void Deactivate()
    {

        if (isButtonDown)
            Up();
        Exit();
    }

    public string deviceName = "Kinect";
    public enum SubmitWhen { Down, Up, Enter, StayHover, StayActive, Exit, None }
    public SubmitWhen [] submitWhen = new SubmitWhen []{ SubmitWhen.Down };
    public float defaultStayOver = 2.5f;
    public float defaultStayActive = 1f;

    public bool submitOnlyOnce;
    private int submitCount;
    public float submitCooldown = 2f;
    private float submitCooldownCount = 0;


    public bool IsOnFocusableObject { get; private set; }
    //Private later;
    public bool isSelectorActive;
    //Private later;
    public bool isButtonDown;
    //Private later;
    public float hoverSince;
    //Private later;
    public float activeSince;

    public bool withButtonEvent = true;
    public bool withBroadcast;
    public bool withListener;
    public bool allowButtonCustumTimeForStay = true;

    //Private later;
    #region Button targeted state
    public GameObject _selectedObject;
    public IScreenInteractionListener SelectedObjectListener{ get; private set;}
    public IScreenInteractionCustom SelectedObjectCustom {  get; private set; }
    public GameObject SelectedObject
    {
        get { return _selectedObject; }
        set
        {
            GameObject oldValue = _selectedObject;
            _selectedObject = value;

            if (oldValue != value)
            {
                if (value == null)
                {
                    SelectedObjectListener = null;
                    SelectedObjectCustom = null;
                }
                else {

					SelectedObjectListener = value.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
                    SelectedObjectCustom = value.GetComponent(typeof(IScreenInteractionCustom)) as IScreenInteractionCustom;
                    
                }
                ButtonChangeDetected(oldValue, value);
            }
        }
    }


    public LayerMask layersAllowForObject;
    public Vector3 HitPosition;

    public delegate void CursorEvent(ScreenToIngameInteraction screenInter);
    public CursorEvent onMouseEnter;
    public CursorEvent onMouseDown;
    public CursorEvent onMouseSubmit;
    public CursorEvent onMouseUp;
    public CursorEvent onMouseExit;

    public delegate void CursorStayEvent(ScreenToIngameInteraction screenInter, float timeLeft, float initialTime);
    public CursorStayEvent onMouseHover;
    public CursorStayEvent onMouseStay;
    public bool debug = false;
    public IScreenCursorInfoSource[] Cursors { get {
            List<IScreenCursorInfoSource> cursorsActive = new List<IScreenCursorInfoSource>();
            for (int i = 0; i < cursorsObserved.Count; i++)
            {
                if (cursorsObserved[i] != null && cursorsObserved[i].IsCursorCanBeUse())
                    cursorsActive.Add(cursorsObserved[i]);
            }
            return cursorsActive.ToArray(); } }

    private List<IScreenCursorInfoSource> cursorsObserved = new List<IScreenCursorInfoSource>();
    public GameObject[] cursorsToObserve;
    public void AddCursors(IScreenCursorInfoSource cursor)
    {
        if (cursor != null)
            cursorsObserved.Add(cursor);
    }
    public void RemoveCursors(IScreenCursorInfoSource cursor)
    {
        if (cursor != null)
            cursorsObserved.Remove(cursor);
    }


    private void ButtonChangeDetected(GameObject oldButt, GameObject newButt)
    {
        if (oldButt != null)
        {
            SwitchOff(oldButt);
        }
        if (newButt != null)
            SwitchOn(newButt);

        if (oldButt == null)
            IsOnFocusableObject = true;
        if (newButt == null)
            IsOnFocusableObject = false;

        hoverSince = 0f;
        activeSince = 0f;
    }

    private void SwitchOn(GameObject newButt)
    {
        Enter(newButt);
        Hover(newButt);
    }

    private void SwitchOff(GameObject oldButt)
    {
        Exit(oldButt);
    }
    #endregion
    #region Hand management


    public Vector3 ScreenPosition
    { get; set; }


    private void Press()
    {
        SetPressState(true);
    }
    private void Release()
    {
        SetPressState(false);
    }
    private void SetPressState(bool isPress)
    {

        isSelectorActive = isPress;

    }



    #endregion
    #region Update and method linked

    void Start()
    {

        InstanceInScene = this;
        foreach (GameObject cursorGamo in cursorsToObserve)
        {
            IScreenCursorInfoSource source = cursorGamo.GetComponent(typeof(IScreenCursorInfoSource)) as IScreenCursorInfoSource;
            AddCursors(source);
        }
    }
    void Update()
    {

        DoTheHandsAreOverAnyButton();

        if (submitCooldownCount > 0f)
        {
            submitCooldownCount = Mathf.Clamp(submitCooldownCount - Time.deltaTime, 0f, float.MaxValue);
        }
        CheckAndSendButtonState();
    }

    //Var in aim to do not instanciate it every time
    private List<IScreenCursorInfoSource> cursorsHover = new List<IScreenCursorInfoSource>();
    private bool DoTheHandsAreOverAnyButton()
    {
        //Set Screen Position
        PointerEventData pe = new PointerEventData(EventSystem.current);
        //If not button is selected,  look for one cursor that is over.
        if (SelectedObject == null)
        {
            GameObject butt;

            butt = GetFirstButtonWithCursorHover(ref pe);
            SelectedObject = butt;
        }

        if (SelectedObject != null)
        {
            GetAllCursorsPointingAtButton(SelectedObject, ref cursorsHover, ref pe);

            if (cursorsHover.Count <= 0)
            {
                //Unselect the current object if none is over it
                SetPressState(false);
                SelectedObject = null;
            }
            else
            {
                SetPressState(IsAnyCursorsPressing(ref cursorsHover));
            }
        }

        return SelectedObject == null;
    }

    private bool IsAnyCursorsPressing(ref List<IScreenCursorInfoSource> cursorsHoverList)
    {
        bool isAnyPressing = false;
        foreach (IScreenCursorInfoSource cursor in cursorsHoverList)
        {
            if (cursor.IsCursorActive())
            {
                isAnyPressing = true;
                break;
            }
        }
        return isAnyPressing;
    }

    private void GetAllCursorsPointingAtButton(GameObject button, ref List<IScreenCursorInfoSource> cursorHoverList, ref PointerEventData pe)
    {
        GameObject butt;
        Camera cameraUsed = null;
        cursorHoverList.Clear();
        foreach (IScreenCursorInfoSource source in Cursors)
        {
            float distance;
            //NB: Deal with first one, it could have several buttun one beside an other
            butt = GetButtonByRaycast(source.GetScreenPosition(ref cameraUsed), cameraUsed, ref pe, out distance);
            if (butt == button)
                cursorHoverList.Add(source);
        }
    }

    private GameObject GetFirstButtonWithCursorHover(ref PointerEventData pe)
    {
        GameObject butt = null;
        Camera cameraUsed = null;
        float closerDistance = float.MaxValue;
        GameObject closerButton = null;
        foreach (IScreenCursorInfoSource source in Cursors)
        {
            float buttonDistance;
            butt = GetButtonByRaycast(source.GetScreenPosition(ref cameraUsed), cameraUsed, ref pe, out buttonDistance);
            if (butt != null && buttonDistance<closerDistance)
            {
                closerDistance = buttonDistance;
                closerButton = butt;
            }

        }

        return closerButton;
    }

    private GameObject GetButtonByRaycast(Vector3 ScreenPosition, Camera orignalCamera, ref PointerEventData pointEvent, out float distanceBetween)
    {
        distanceBetween = 0f;
        pointEvent.position = ScreenPosition;

        if (pointEvent != null && EventSystem.current!=null)
        {
            //User raycast
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointEvent, hits);
            //Explore the result
            Button butt = null;
            foreach (RaycastResult h in hits)
            {
                GameObject gamoHit = h.gameObject;
                butt = gamoHit.GetComponent<Button>() as Button;

                if (butt != null) break;
            }
            if (butt != null)
            {
                distanceBetween = Vector3.Distance(butt.transform.position, orignalCamera.transform.position);
                return butt.gameObject;
            }
            if (orignalCamera == null)
                return null;
        }
            Ray ray = orignalCamera.ScreenPointToRay(ScreenPosition);

            if (debug)
                Debug.DrawRay(ray.origin, ray.direction, Color.red, Time.deltaTime);
     
        RaycastHit [] ojectsHit =  Physics.RaycastAll(ray, float.MaxValue, layersAllowForObject);

        if (ojectsHit.Length <= 0)
            return null;
        float closerDistance = float.MaxValue;
        GameObject closer=null;
        foreach (RaycastHit hit in ojectsHit)
        {
            float camToObjectDistance = Vector3.Distance(orignalCamera.transform.position, hit.point);
            if ( camToObjectDistance< closerDistance) {
                closerDistance = camToObjectDistance;
                closer = hit.transform.gameObject;
            }
        }
        if (closer != null)
        {
            distanceBetween = closerDistance;
        }
        return closer;
    }
    private void CheckAndSendButtonState()
    {
        if (SelectedObject == null) return;
        if (isSelectorActive && !isButtonDown)
        {
            isButtonDown = true;
            Down();

        }
        else if (!isSelectorActive && isButtonDown)
        {

            isButtonDown = false;
            Up();
        }

        hoverSince += Time.deltaTime;
        Hover();
        if (isButtonDown)
        {
            activeSince += Time.deltaTime;
            Stay();
        }

    }

    #endregion
    #region Enter, Hover, Down, Stay, Up ,Exit -?-> Submit + Broadcast
    private void Stay(GameObject button = null, PointerEventData pointer = null)
    {
        if (!isAllowToBeActive) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;
        float timeToBeActive = GetTimeNeededToBeActive(SubmitWhen.StayActive, ref button);

        _timeLeftActive = timeToBeActive - activeSince;
        if (CheckIfSubmitMustBeCall(SubmitWhen.StayActive))
        {
            if (activeSince > timeToBeActive)
                Submit(button, pointer);
        }

        if (onMouseStay != null)
            onMouseStay(this, _timeLeftActive, timeToBeActive);
        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Stay", _timeLeftActive, SendMessageOptions.DontRequireReceiver);
        }
        if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionStay(this, deviceName, _timeLeftActive, timeToBeActive);

        }

    }
    private void Hover(GameObject button = null, PointerEventData pointer = null)
    {
        if (!isAllowToBeActive) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;

        float timeToBeActive = GetTimeNeededToBeActive(SubmitWhen.StayHover, ref button);
        _timeLeftHover = timeToBeActive - hoverSince;
        if (CheckIfSubmitMustBeCall(SubmitWhen.StayHover))
        {
            if (hoverSince > timeToBeActive)
                Submit(button, pointer);
        }
        if (onMouseHover != null)
            onMouseHover(this, _timeLeftHover, timeToBeActive);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Over", _timeLeftHover, SendMessageOptions.DontRequireReceiver);
        }
        if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionOver(this, deviceName, _timeLeftHover, timeToBeActive);

        }
    }

    private void Enter(GameObject button = null, PointerEventData pointer = null)
    {
        if (!isAllowToBeActive) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;

        if (CheckIfSubmitMustBeCall(SubmitWhen.Enter))
            Submit(button, pointer);

        if (withButtonEvent)
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
        if (onMouseEnter != null)
            onMouseEnter(this);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Enter", SendMessageOptions.DontRequireReceiver);
        }
        if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionEnter(this, deviceName);

        }
    }

    private void Down(GameObject button = null, PointerEventData pointer = null)
    {
        if (!CheckAndComplete(ref pointer, ref button)) return;
        if (CheckIfSubmitMustBeCall(SubmitWhen.Down))
            Submit(button, pointer);
        if (onMouseDown != null)
            onMouseDown(this);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Down", SendMessageOptions.DontRequireReceiver);
        } if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionDown(this, deviceName);

        }
    }

    private void Up(GameObject button = null, PointerEventData pointer = null)
    {
        if (!isAllowToBeActive) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;
        if (CheckIfSubmitMustBeCall(SubmitWhen.Up))
            Submit(button, pointer);
        if (withButtonEvent)
        {
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.selectHandler);
        }
        if (onMouseUp != null)
            onMouseUp(this);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Up", SendMessageOptions.DontRequireReceiver);
        } if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionUp(this, deviceName);

        }
    }

    private void Exit(GameObject button = null, PointerEventData pointer = null)
    {
        if (!isAllowToBeActive) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;

        if (CheckIfSubmitMustBeCall(SubmitWhen.Exit))
            Submit(button, pointer);

        if (withButtonEvent)
        {
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.deselectHandler);
        }
        if (onMouseExit != null)
            onMouseExit(this);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Exit", SendMessageOptions.DontRequireReceiver);
        } if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionExit(this, deviceName);

        }


        submitCooldownCount = 0;
        submitCount = 0;
    }

    private void Submit(GameObject button = null, PointerEventData pointer = null)
    {

        if (!isAllowToBeActive) return;

        if (submitOnlyOnce && submitCount > 0) return;
        if (submitCooldownCount > 0) return;
        if (!CheckAndComplete(ref pointer, ref button)) return;

        if (withButtonEvent)
            ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
        if (onMouseSubmit != null)
            onMouseSubmit(this);

        if (withBroadcast)
        {
            button.BroadcastMessage("On" + deviceName + "Submit", SendMessageOptions.DontRequireReceiver);
        } if (withListener)
        {
            IScreenInteractionListener listener = button.GetComponent(typeof(IScreenInteractionListener)) as IScreenInteractionListener;
            if (listener != null)
                listener.OnScreenInteractionSubmit(this, deviceName);

        }
        submitCooldownCount = submitCooldown;
        activeSince = 0;
        submitCount++;
    }
    #endregion
    #region Procedure Methode
    private bool CheckIfSubmitMustBeCall(SubmitWhen submitZone)
    {
        for (int i = 0; i < submitWhen.Length; i++)
        {
            if (submitWhen[i] == submitZone) return true;
        }
      
        return false;
    }
    
    private bool CheckAndComplete(ref PointerEventData pointer, ref GameObject button)
    {
        if (pointer == null)
        {
            pointer = new PointerEventData(EventSystem.current);
        }
        if (button == null)
        {
            button = SelectedObject;
        }
        return pointer != null && button != null;
    }
    private float GetTimeNeededToBeActive(SubmitWhen submitWhen, ref GameObject button)
    {

        float time = defaultStayOver;
        if (submitWhen == SubmitWhen.StayActive)
            time = defaultStayActive;

        if (!allowButtonCustumTimeForStay) return time;

        IScreenInteractionCustom customTime = button.gameObject.GetComponent(typeof(IScreenInteractionCustom)) as IScreenInteractionCustom;
        if (customTime != null)
        {
            if (submitWhen == SubmitWhen.StayActive)
                time = customTime.GetTimeToBeSubmitOnStay();
            if (submitWhen == SubmitWhen.StayHover)
                time = customTime.GetTimeToBeSubmitOnOver();
        }
        return time;
    }

    #endregion




    
}



public interface IScreenCursorInfoSource
{
    Vector3 GetScreenPosition(ref Camera usedCamera);
    bool IsCursorActive();

    bool IsCursorCanBeUse();
}
public interface IScreenInteractionCustom
{
    float GetTimeToBeSubmitOnStay();
    float GetTimeToBeSubmitOnOver();
    bool IsDisplayCursorNeeded();

}

public interface IScreenInteractionListener
{
    void OnScreenInteractionSubmit(ScreenToIngameInteraction from, string device);
    void OnScreenInteractionEnter(ScreenToIngameInteraction from, string device);
    void OnScreenInteractionDown(ScreenToIngameInteraction from, string device);
    void OnScreenInteractionOver(ScreenToIngameInteraction from, string device, float timeLeft, float initalTime);
    void OnScreenInteractionStay(ScreenToIngameInteraction from, string device, float timeLeft, float initalTime);
    void OnScreenInteractionUp(ScreenToIngameInteraction from, string device);
    void OnScreenInteractionExit(ScreenToIngameInteraction from, string device);

}