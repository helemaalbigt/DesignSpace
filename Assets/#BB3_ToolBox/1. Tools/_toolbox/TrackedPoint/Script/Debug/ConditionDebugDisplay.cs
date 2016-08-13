using UnityEngine;
using System.Collections;

public class ConditionDebugDisplay : MonoBehaviour {


    public MonoBehaviour target;
    public Conditions.ICondition condition;
    public Conditions.IGroupCondition groupCondition;
    public string value;
    public void Start() { 
    
        if(target==null){
           Destroy(this);return;
        }
        Conditions.IConditionOwner condOwner = target as Conditions.IConditionOwner; 
        if (condOwner != null)
            condition = condOwner.GetCondition();
        Conditions.IGroupConditionsOwner condGroupOwner = target as Conditions.IGroupConditionsOwner;
        if (condGroupOwner != null)
            groupCondition = condGroupOwner.GetGroupCondition();

       
    }
	void Update () {

        if (condition != null) value = "Condition:" + condition.IsComplete() + "  --  " + condition.GetDescriptiveName() ;
        if (groupCondition != null) value = "Group condition:" + groupCondition.GetPourcentComplete() + "  --  " + groupCondition.GetDescriptiveName();
	}
}
