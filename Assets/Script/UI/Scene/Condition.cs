using System;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    private float curValue;
    public float maxValue;      // 추후 변경
    private Image uiBar;
    public ConditionType thisType { get; private set; }

    private void Start()
    {
        if (Enum.TryParse(gameObject.name, out ConditionType typeName))
        {
            thisType = typeName;
        }
        else
        {
            thisType = ConditionType.None;
        }

        curValue = maxValue;
        uiBar = GetComponent<Image>();
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue += value;

        if (isFull())
        {
            curValue = maxValue;
        }
    }

    public void Substract(float value)
    {
        curValue -= value;

        if(isZero())
        {
            curValue = 0;
        }
    }

    public bool isFull()
    {
        return curValue >= maxValue;
    }

    public bool isZero()
    {
        return curValue <= 0;
    }
}