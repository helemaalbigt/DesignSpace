using UnityEngine;
using System.Collections;

public class DuplicateTool : MonoBehaviour {

    public GameObject targetToDuplicate;
    public int onZ = 10;
    public int onY = 10;
    public int onX = 10;
    public float distance=2f;
    public float scale=1f;
	// Use this for initialization
	void Start () {

        if (targetToDuplicate) {

            for (int z = 0; z < onZ; z++)
            {
                for (int x = 0; x < onX; x++)
                {
                    for (int y = 0; y < onY; y++)
                    {
                        GameObject gamo = GameObject.Instantiate(targetToDuplicate) as GameObject;
                        gamo.transform.parent = this.transform;
                        gamo.transform.localPosition = new Vector3(x * distance, y * distance, z * distance);
                        gamo.transform.rotation = targetToDuplicate.transform.rotation;
                        gamo.transform.localScale = Vector3.one * scale;
                        gamo.name= string.Format("Cube(x:{0},y:{1},z:{2})", x, y, z);
                    }
                    
                }
                
            }
        }
	}
	

}
