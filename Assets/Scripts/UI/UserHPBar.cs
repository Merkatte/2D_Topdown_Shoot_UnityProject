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

    public void SetHPSlider(float curHp, float maxHp)
    {
        var value = curHp >= maxHp ? 1 : curHp / maxHp;
        hpSlider.value = value;
    }

    public void setStaminaSlider(float curStamina, float maxStamina)
    {
        var value = curStamina >= maxStamina ? 1 : curStamina / maxStamina;
        staminaSlider.value = value;
    }

    private void OnDisable()
    {

    }
}
