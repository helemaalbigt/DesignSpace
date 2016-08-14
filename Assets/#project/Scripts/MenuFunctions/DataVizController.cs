using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataVizController : MonoBehaviour {

    public Transform _JsonButtonsParent;
    public Transform _DataWrapper;

    public GameObject _PositionVolumePrefab;
    public GameObject _GazeVolumePrefab;
    public GameObject _AvatarPrefab;

    private List<JSONbutton> JsonButtons = new List<JSONbutton>();
    private bool _IsRendering = false;
    private bool _FinishedRendering = false;
    private bool _Paused = false;
    private bool _FastRendering = false;

	// Use this for initialization
	void Start () {
        Invoke("RefreshJsonButtons", 0.5f);
	}

    private void RefreshJsonButtons()
    {
        foreach (Transform child in _JsonButtonsParent)
        {
            if (child.GetComponent<JSONbutton>() != null)
                JsonButtons.Add(child.GetComponent<JSONbutton>());
        }
    }

    public void StartPlayBack()
    {
        //dont spawn new replays, just continue existing ones
        if (_Paused || _IsRendering)
            return;

        foreach(LookPath path in GetActiveButtonData())
        {
            StartCoroutine(RenderPath(path));
        }
    }

    public void Pause()
    {
        _Paused = true;
    }

    public void Resume()
    {
        _Paused = false;
    }

    public void Stop()
    {
        StopAllCoroutines();
        _IsRendering = false;
        _FastRendering = false;

        foreach (Transform trans in _DataWrapper)
        {
            Destroy(trans.gameObject);
        }
    }

    public void ToEnd()
    {
        _FastRendering = true;

        if (!_IsRendering)
            StartPlayBack();
    }

    private List<LookPath> GetActiveButtonData()
    {
        List<LookPath> returnList = new List<LookPath>();
        foreach(JSONbutton jsb in JsonButtons)
        {
            if (jsb.GetComponent<MenuCheckbox>()._IsSelected)
                returnList.Add(jsb._LookPath);
        }
        return returnList;
    }

    IEnumerator RenderPath(LookPath path)
    {
        //make sure there are points in this path
        if (path._lookStatePath.Count == 0)
            yield break;

        _IsRendering = true;

        //variables used to calculate timestep
        float lastTime = path._lookStatePath[0]._timeSinceStartLooking;
        float timeSinceLastPoint = 0;

        //create avatar
        Vector3 avatarStartPos = path._lookStatePath[0]._lookState._rootPosition;
        GameObject avatar = SpawnAvatar(avatarStartPos);

        int statesRendered = 0;

        //loop through all points and execute render functions
        foreach (TimeLinkedLookState state in path._lookStatePath)
        {
            while (_Paused)
            {
                yield return null;
            }

            //instantiate cube each player position
            SpawnPlayerPos(state._lookState._rootPosition);
            //instantiate sphere at each gaze position
            SpawnPlayerGaze(state._lookState._lookAtPosition);
            //update Avatar
            StartCoroutine( UpdateAvatar(avatar, state._lookState._rootPosition, state._lookState._lookAtPosition, timeSinceLastPoint) );

            //calculate & execute timestep
            timeSinceLastPoint = state._timeSinceStartLooking - lastTime;
            lastTime = state._timeSinceStartLooking;
            if (_FastRendering)
            {
                //if(statesRendered % 50 == 0)
                  //  yield return null;
            }
            else
            {
                yield return new WaitForSeconds(timeSinceLastPoint);
            }

            statesRendered++;
        }

        yield return null;

        _IsRendering = false;
        _FastRendering = false;
    }

    private void SpawnPlayerPos(Vector3 pos)
    {
        GameObject cube = Instantiate(_PositionVolumePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        cube.transform.parent = _DataWrapper;
        cube.transform.localPosition = pos;
        cube.transform.localScale = Vector3.one;
        //cube.GetComponentInChildren<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void SpawnPlayerGaze(Vector3 pos)
    {
        GameObject sphere = Instantiate(_GazeVolumePrefab, Vector3.zero, Quaternion.identity) as GameObject;
        sphere.transform.parent = _DataWrapper;
        sphere.transform.localPosition = pos;
        sphere.transform.localScale = Vector3.one;
    }

    private GameObject SpawnAvatar(Vector3 startPos)
    {
        GameObject avatar = Instantiate(_AvatarPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        avatar.transform.parent = _DataWrapper;
        avatar.transform.localPosition = startPos;
        avatar.transform.localScale = Vector3.one;

        return avatar;
    }

    IEnumerator UpdateAvatar(GameObject avatar, Vector3 newPos, Vector3 newLookAtPos, float duration)
    {
        float timePassed = 0;
        Vector3 startPos = avatar.transform.localPosition;
        Quaternion startRot = avatar.transform.localRotation;

        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;

            avatar.transform.localPosition = Vector3.Slerp(startPos, newPos, timePassed / duration);
            yield return null;
        } 
    }
}
