using System;
using UnityEngine;

[Serializable]
public struct UpgradeOption
{
    public readonly UpgradeCategory Category;
    public readonly int StatType;
    public readonly string DisplayName;
    public readonly float Value;
    public readonly CalculateType ValueType;

    public UpgradeOption(UpgradeCategory category, int statType, string displayName, float value,
        CalculateType valueType)
    {
        Category = category;
        StatType = statType;
        DisplayName = displayName;
        Value = value;
        ValueType = valueType;
    }
    
    public string GetDisplayText()
    {
        if (ValueType == CalculateType.Percentage)
            return $"{DisplayName}\n+{Value:F1}%";
        else
            return $"{DisplayName}\n+{Value:F0}";
    }
}
