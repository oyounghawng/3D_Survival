using System;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionType
{
    None = 0,
    HP = 10,
    Hunger,
    Water,
    Stamina
}

public enum CalType
{
    Add,
    Substract
}

public class UI_Conditions : MonoBehaviour
{
    public Dictionary<ConditionType, Condition> conditionDict = new Dictionary<ConditionType, Condition>();

    // 추후 수정 예정
    private void Awake()
    {
        foreach(ConditionType condition in Enum.GetValues(typeof(ConditionType))) 
        {
            conditionDict.Add(condition, Util.FindChild<Condition>(gameObject, condition.ToString()));
        }
    }

    public Condition Get(ConditionType conditionType)
    {
        if(conditionDict.TryGetValue(conditionType, out Condition curCondition))
        {
            return curCondition;
        }

        return null;
    }

    private void Update()
    {
        Passive(ConditionType.Stamina, 1f, CalType.Add);
        Passive(ConditionType.Hunger, 0.5f, CalType.Substract);
        Passive(ConditionType.Water, 0.5f, CalType.Substract);
    }

    public void Passive(ConditionType conditionType, float value, CalType calType)
    {
        Condition condition = Get(conditionType);

        if(condition == null) 
        {
            return;
        }

        value *= Time.deltaTime;
        switch(calType)
        {
            case CalType.Add:
                condition.Add(value); 
                break;
            case CalType.Substract:
                condition.Substract(value); 
                break;
        }
    }

}
