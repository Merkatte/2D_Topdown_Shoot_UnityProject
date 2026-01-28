using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpPop : Popbase
{
    [SerializeField] private TextMeshProUGUI[] _optionTexts;
    
    private List<UpgradeOption> _options;
    private Action<UpgradeOption> _callback;
    
    public void Init(List<UpgradeOption> options, Action<UpgradeOption> onClick)
    {
        _options = options;
        _callback = onClick;
        for (int index = 0; index < _options.Count; index++)
        {
            _optionTexts[index].text = options[index].DisplayName;
        }
    }

    public void OnClickButton(int num)
    {
        _callback(_options[num]);
        ClosePop();
    }
}
