using System;
using UnityEngine;

[Serializable]
public struct UpgradeOption
{
    public readonly UpgradeCategory Category;
    public readonly int StatType;
    public readonly string DisplayName;
    public readonly float Value;
    public readonly CalculateType CalType;

    public UpgradeOption(UpgradeCategory category, int statType, string displayName, float value,
        CalculateType calType)
    {
        Category = category;
        StatType = statType;
        DisplayName = displayName;
        Value = value;
        CalType = calType;
    }
    
    public string GetDisplayText()
    {
        if (CalType == CalculateType.Percentage)
            return $"{DisplayName}\n+{Value:F1}%";
        return $"{DisplayName}\n+{Value:F1}";
    }
}
