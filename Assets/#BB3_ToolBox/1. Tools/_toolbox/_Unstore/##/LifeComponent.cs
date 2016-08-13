using UnityEngine;
using System.Collections;

public class LifeComponent : MonoBehaviour {
	
	
	
	public float initalLife=100;
	//InspectorOnly;
    public float _life;
	public bool hasBeenDeathOnce = false;
	
	public virtual float Life
	{
		get { return _life; }
		set
		{

			float old = _life;
			if (value < 0f) value = 0f;
			_life = value;
            if (onLifeChange != null)
            {

                onLifeChange(this, old, _life);
            }
			if (!hasBeenDeathOnce && _life <= 0)
			{
				hasBeenDeathOnce = true;
                if (onNoLifeAnymore != null)
                {

                    onNoLifeAnymore(this);
                }
			}
		}
	}
	
	public void AddLife(float relativeAmount)
	{
		Life += relativeAmount;
	}
	
	
	void Awake() { Life = initalLife; }
	public delegate void HasNoLifeAnymore(LifeComponent obj);
	public HasNoLifeAnymore onNoLifeAnymore;
	
	public delegate void LifeChange(LifeComponent obj, float oldLife, float newLife);
	public LifeChange onLifeChange;
	
	
	public void Reset()
	{
		Life = initalLife;
	}
	
	public float GetPourcentCompareToInital()
	{
		return Life / initalLife;
	}
	
	internal void AddPourcent(float pctLifeHeal)
	{
		if (Life < initalLife) {
			Life += initalLife * pctLifeHeal;
		}
	}


    internal void SetAsFull()
    {
        Reset();
    }
}
