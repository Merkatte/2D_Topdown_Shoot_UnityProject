using UnityEngine;
using System;
public class GameOverPop : Popbase
{
    private Action _onClickRetry;
    public void Init(Action callback)
    {
        _onClickRetry = callback;
    }
    public override void ClosePop()
    {
        _onClickRetry = null;
        base.ClosePop();
    }

    public void OnClickButton()
    {
        _onClickRetry?.Invoke();
        base.ClosePop();
    }
}
