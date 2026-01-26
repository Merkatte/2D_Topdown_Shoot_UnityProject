using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    
    private PoolManager _poolManager;
    Dictionary<int, HPBar> enemyHPBars =  new Dictionary<int, HPBar>();

    public void Init(PoolManager poolManager)
    {
        _poolManager = poolManager;
    }

    public void SetHP(int instanceID, GameObject targetObject)
    {
        if (enemyHPBars == null) enemyHPBars = new Dictionary<int, HPBar>();
        HPBar hpBar = _poolManager.GetHPBar();
        hpBar.transform.parent = canvas.transform;
        
        hpBar.Init(targetObject);
        enemyHPBars.Add(instanceID, hpBar);
    }
    
    public void ChangeHP(float val, int instanceID)
    {
        if (!enemyHPBars.ContainsKey(instanceID)) return;
        
        enemyHPBars[instanceID].SetHPSlider(val);
    }

    public void ReleaseHP(int instanceID)
    {
        _poolManager.ReturnHPBar(enemyHPBars[instanceID]);
        enemyHPBars.Remove(instanceID);
    }
}
