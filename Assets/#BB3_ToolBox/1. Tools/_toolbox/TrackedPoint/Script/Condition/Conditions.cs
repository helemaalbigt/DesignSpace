using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



public class Conditions : MonoBehaviour
{
    public delegate bool IsConditionComplete();

    public interface IConditionOwner
    {
        ICondition GetCondition();
    }
    public interface IGroupConditionsOwner
    {
        IGroupCondition GetGroupCondition();
    }
    public interface ICondition
    {


        bool IsComplete();

        void SetConditionAccess(IsConditionComplete accessor);
        IsConditionComplete GetConditionAccess();

        string GetDescriptiveName();
        void SetDescriptiveName(string name);

    }
   

    public delegate float IsGroupConditionComplete();
    public interface IGroupCondition
    {
        bool IsComplete();
        float GetPourcentComplete();

        void SetConditionAccess(IsGroupConditionComplete accessor);
        IsGroupConditionComplete GetConditionAccess();
        void AddCondition(Conditions.ICondition condition);
        ICondition[] GetConditions();

        string GetDescriptiveName();
        void SetDescriptiveName(string name);
    }


    /**
     TODO add a static acccessor with hashtable in aim to have quick access 
     * on Condition, GroupCondition
     */

}






[System.Serializable]
public class Condition :  Conditions.ICondition
{

    public string _descriptiveName = "No descriptive name";
    private Conditions.IsConditionComplete conditionAccessor;
    public bool lastValueComputed;

    public bool IsComplete()
    {
        if (conditionAccessor == null) lastValueComputed = false;
        else lastValueComputed = conditionAccessor();
        return lastValueComputed;
    }

    public void SetConditionAccess(Conditions.IsConditionComplete accessor)
    {
        conditionAccessor = accessor;
    }

    public Conditions.IsConditionComplete GetConditionAccess()
    {
        return conditionAccessor;
    }

    public string GetDescriptiveName()
    {
        return _descriptiveName;
    }

    public void SetDescriptiveName( string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        _descriptiveName = name;
    }
}

public abstract class GroupCondition : Conditions.IGroupCondition
{

    public string descriptiveName = "No descriptive name";
    private Conditions.IsGroupConditionComplete conditionAccessor;
    public float lastValueComputed;

    public bool IsComplete()
    {
        return GetPourcentComplete()>=1f;
    }

    public float GetPourcentComplete()
    {
        if (conditionAccessor == null) lastValueComputed = 0f;
        else lastValueComputed = Mathf.Clamp(conditionAccessor(), 0f, 1f);
        return lastValueComputed;
    }

    public void SetConditionAccess(Conditions.IsGroupConditionComplete accessor)
    {
        conditionAccessor = accessor;
    }

    public Conditions.IsGroupConditionComplete GetConditionAccess()
    {
        return conditionAccessor;
    }

    public string GetDescriptiveName()
    {
        return descriptiveName;
    }

    public void SetDescriptiveName(string name)
    {
        if (string.IsNullOrEmpty(name)) return;
        descriptiveName = name;
    }


    public abstract Conditions.ICondition[] GetConditions();
    public abstract void AddCondition(Conditions.ICondition condition);
    
}
[System.Serializable]
public class GroupCondition_List: GroupCondition 
{
    public int conditionCount;
    public List<Conditions.ICondition> conditions = new List<Conditions.ICondition>();

    public GroupCondition_List() {
        SetConditionAccess(PourcentComplete);
    }
    public override void AddCondition( Conditions.ICondition condition)
    {
        if (condition == null) return;
        conditionCount++;
        conditions.Add(condition);
    }

    public override Conditions.ICondition[] GetConditions()
    {
        return conditions.ToArray();
    }
    public float PourcentComplete()
    {
        if (conditions == null) return 0f;
        int count = conditions.Count;
        if (count<=0) return 0f;

        int i=0;
        foreach (Conditions.ICondition condition in conditions)
        {
            if (condition != null && condition.IsComplete()) i++;
            
        }
        return (float)i / (float)count;
    }

}


[System.Serializable]
public class BetweenRange 
{
    public enum RangeType {Before, Between, After, Out }
    public RangeType rangeType= RangeType.Between;

    public double rangeA;
    public double rangeB;

    public BetweenRange() { }

    public BetweenRange(float min, float max, RangeType rangeType= RangeType.Between) {
        rangeA = min;
        rangeB = max;
        this.rangeType = rangeType;
    }

    public bool IsInRange(double value)
    {
        CheckAndInverseRange();
        switch (rangeType)
        {
            case RangeType.Before:
                return value.CompareTo(rangeA) < 0d;
            case RangeType.Between:
                return value.CompareTo(rangeA) > 0d && value.CompareTo(rangeB) < 0d;
            case RangeType.After:
                return value.CompareTo(rangeB) > 0d;
            case RangeType.Out:
                return value.CompareTo(rangeA) < 0d || value.CompareTo(rangeB) > 0d;
        }
        return false;
    }

    private void CheckAndInverseRange()
    {
        if (rangeA>rangeB)
        {
            double tmp = rangeA;
            rangeA = rangeB;
            rangeB = tmp;
        }
    }

}
