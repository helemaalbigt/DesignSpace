using UnityEngine;
using System.Collections;

public class Tracked_Group : MonoBehaviour, Conditions.IGroupConditionsOwner {

    public GroupCondition_List groupCondition = new GroupCondition_List();
    public MonoBehaviour[] conditionScripts;
    public Transform [] objWithConditionScripts;
	// Use this for initialization
	void Start () {
        // Add script that are condition
        foreach (MonoBehaviour script in conditionScripts)
            if (script != null)
            {
                Conditions.IConditionOwner owner = script as Conditions.IConditionOwner;
                if (owner != null) groupCondition.AddCondition(owner.GetCondition());

            }
        //Add from object the script that are condition 
        foreach(Transform trans in objWithConditionScripts){
            if (trans == null) continue;
            MonoBehaviour [] scripts = trans.GetComponents<MonoBehaviour>() as MonoBehaviour[];
            if (scripts == null) continue;
            foreach(MonoBehaviour sc in scripts)
            {
                Conditions.IConditionOwner owner = sc as Conditions.IConditionOwner ;
                if (owner == null) continue;
                 groupCondition.AddCondition(owner.GetCondition());
            } 
       }
	
	}
	

    public Conditions.IGroupCondition GetGroupCondition()
    {
        return groupCondition;
    }
}
