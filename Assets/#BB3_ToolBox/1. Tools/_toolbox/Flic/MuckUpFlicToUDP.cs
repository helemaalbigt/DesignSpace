using UnityEngine;
using System.Collections;

public class MuckUpFlicToUDP : MonoBehaviour {


    public UDP_Sender udpSender;
    public string buttonNameID;


    public string onClickMessage = "Click";
    public string onDoubleClickMessage = "DoubleClick";
    public string onPressedkMessage = "Pressed";

    private bool _isPressed;

    public bool IsPressed
    {
        get { return _isPressed; }
        set {
            bool oldValue = _isPressed;
            bool newValue = value;

            _isPressed = newValue;


            float clickTime = Time.time;
            float clickTimeBetween = clickTime - lastClick;
            if (!oldValue && newValue && clickTimeBetween < 0.2f)
            {
                if (onDoubleClick!=null)
                {    onDoubleClick();  }
            }
            else if (!oldValue && newValue)
            {
                if (onClick != null)
                    onClick(); 
            }
            

            else if (oldValue && !newValue)
            {
                if (clickTimeBetween > 0.5f)
                    if (onPressed != null)
                        onPressed();


            }
            if (!oldValue && newValue)
                lastClick = clickTime;
                
            
        
        }
    }

    private float lastClick;

    public delegate void OnClick();
    public delegate void OnDoubleClick();
    public delegate void OnPressed();
    public OnClick onClick;
    public OnDoubleClick onDoubleClick;
    public OnPressed onPressed;


    public bool withDebug;
    
    // Use this for initialization
	void Start () {
        lastClick = Time.time;
        if(string.IsNullOrEmpty(buttonNameID))
        {
            buttonNameID = "";
            for (int i = 0; i < 4; i++)
            {
                buttonNameID += (char) Random.Range(48, 58);

            }

        }
        

        
            onClick += DisplayOnClick;
            onDoubleClick += DisplayOnDoubleClick;
            onPressed += DisplayOnPress;
        


	}
	

   void Update () {

       IsPressed = Input.GetMouseButton(0);

	}

   private void DisplayOnPress()
   {
       if (withDebug)
       print("Button:" + buttonNameID + ":Pressed");
       if(udpSender)
       udpSender.Send("FlickAction:" + buttonNameID + ":Pressed"+ ":"+onPressedkMessage);
   }

   private void DisplayOnDoubleClick()
   {
       if (withDebug)
           print("Button:" + buttonNameID + ":DoubleClick");
       if (udpSender)
           udpSender.Send("FlickAction:" + buttonNameID + ":DoubleClick" + ":" + onDoubleClickMessage);
   }

   private void DisplayOnClick()
   {
       if (withDebug)
           print("Button:" + buttonNameID + ":Click");
       if (udpSender)
           udpSender.Send("FlickAction:" + buttonNameID + ":Click" + ":" + onClickMessage);
   }
}
