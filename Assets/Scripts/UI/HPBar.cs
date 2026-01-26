using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private RectTransform rectTransform;
    
    private GameObject _targetObject;
    private bool _isInit = false;
    public void Init(GameObject targetObject)
    {
        _targetObject = targetObject;
    }

    public void SetHPSlider(float hp)
    {
        if (hp <= 1f) gameObject.SetActive(true);
        hpSlider.value = hp;
    }

    private void LateUpdate()
    {
        rectTransform.position = Camera.main.WorldToScreenPoint(_targetObject.transform.position);
    }

    private void OnDisable()
    {
        _targetObject = null;
    }
}
