using UnityEngine;
using System;
public class Popbase : MonoBehaviour
{
    private Action _closeCallback;
    public virtual void OpenPop(Action closeCallback)
    {
        _closeCallback = closeCallback;
        gameObject.SetActive(true);
    }

    public virtual void ClosePop()
    {
        _closeCallback?.Invoke();
        _closeCallback = null;
    }
}
