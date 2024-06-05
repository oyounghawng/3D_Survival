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

}
