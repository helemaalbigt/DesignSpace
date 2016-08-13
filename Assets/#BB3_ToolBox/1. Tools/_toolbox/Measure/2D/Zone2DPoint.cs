using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Zone2DPoint : MonoBehaviour {


	public Zone2D father;

	public float Width {
		get{
			return father==null?
				0f:
					father.ApplyRatio(father.GetLastCalculatedWidth());
		}
	}
	
	public float Height {
		get{
			return father==null?
				0f:
					father.ApplyRatio(father.GetLastCalculatedHeight());
		}
	}

	public void Start(){
		LookForTheFather ();
	}

	public void LookForTheFather(bool withErase =true)
	{
		if(withErase) father =null;

		if (father == null && this.gameObject.transform.parent !=null) {
			
			father= this.gameObject.transform.parent.GetComponent<Zone2D>() as Zone2D;
			if(father==null)
			{
				//	throw new UnityException("The GameObject father is  not a Zone2D: "+this.gameObject);
			}
		}
	}
}
