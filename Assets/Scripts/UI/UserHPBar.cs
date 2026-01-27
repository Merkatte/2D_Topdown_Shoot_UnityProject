using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UserHPBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private RectTransform rectTransform;
    
    private bool _isInit = false;
    public void Init()
    {

    }

    public void SetHPSlider(float hp)
    {
        hpSlider.value = hp;
    }

    public void setStaminaSlider(float stamina)
    {
        staminaSlider.value = stamina;
    }

    private void OnDisable()
    {

    }
}
