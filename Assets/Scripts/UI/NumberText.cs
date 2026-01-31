using UnityEngine;
using System;
using TMPro;

public class NumberText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private string extraTextAfterNum;
    public NumUIType numUIType;

    public void SetText(int num)
    {
        numberText.text = num.ToString() + extraTextAfterNum;
    }
}
