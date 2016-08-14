using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataVizController : MonoBehaviour {

    public Transform _JsonButtonsParent;
    public Transform _DataWrapper;

    public GameObject _PositionVolumePrefab;
    public GameObject _GazeVolumePrefab;
    public GameObject _AvatarPrefab;

    private int[] arrContent;
    public int[] arrCountNumbers
    {
        get { return arrContent; }
    }

    private List<JSONbutton> JsonButtons = new List<JSONbutton>();
    private bool _IsRendering = false;
    private bool _FinishedRendering = false;
    private bool _Paused = false;
    private bool _FastRendering = false;

    private Vector3 ptTest;
    private float hSize = 1f;
    private int count = 0;
    private List<Vector3> lstPtsCube = new List<Vector3>();

    // Use this for initialization
    void Start () {
      //  Invoke("RefreshJsonButtons", 0.5f);
	}

    public void RefreshJsonButtons()
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

        //list of spawned cubes
        List<Vector3> lstPtsCube = new List<Vector3>();
        ptTest = path._lookStatePath[0]._lookState._rootPosition;
        count = 0;

        //list of spawned sphere
      /*  List<Vector3> lstPtsCube = new List<Vector3>();
        ptTest = path._lookStatePath[0]._lookState._rootPosition;
        count = 0;*/

        //loop through all points and execute render functions
        for (int i = 0; i < path._lookStatePath.Count; i++)
        {
            TimeLinkedLookState state = path._lookStatePath[i];

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

        arrContent = new int[lstPtsCube.Count];
        int c = 0;
        foreach (Vector3 vec in lstPtsCube)
        {
            count = 0;
            foreach (TimeLinkedLookState state in path._lookStatePath)
            {
                Vector3 p = state._lookState._rootPosition;
                if (TestInside(p, vec, hSize))
                    count++;
            }
            arrContent[c] = count;
            c++;
        }


        yield return null;

        _IsRendering = false;
        _FastRendering = false;
    }

    private void SpawnPlayerPos(Vector3 pos)
    {
        if(!TestInside(pos, ptTest, hSize))
        { 
            lstPtsCube.Add(ptTest);
            ptTest = pos;

            GameObject cube = Instantiate(_PositionVolumePrefab, Vector3.zero, Quaternion.identity) as GameObject;
            cube.transform.parent = _DataWrapper;
            cube.transform.localPosition = pos;
            cube.transform.localScale = Vector3.one;
        }
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

    private bool TestInside(Vector3 p, Vector3 ptCube, float hSize)
    {
        Vector3 ptStart = new Vector3(ptCube.x - hSize, ptCube.y - hSize, ptCube.z - hSize);
        Vector3 ptEnd = new Vector3(ptCube.x + hSize, ptCube.y + hSize, ptCube.z + hSize);

        if ((p.x > ptStart.x & p.x < ptEnd.x) & (p.z > ptStart.z & p.z < ptEnd.z) & (p.y > ptStart.y & p.y < ptEnd.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
