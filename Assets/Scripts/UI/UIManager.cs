 using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas popUpCanvas;
    [SerializeField] private Canvas statCanvas;
    
    [Header("Background")]
    [SerializeField] private GameObject backGround;
    
    [Header("Popup")]
    [SerializeField] private List<Popbase> popups;
    
    [Header("Basic UI")]
    [SerializeField] private UserHPBar userHPBar;
    
    
    private PoolManager _poolManager;
    Dictionary<int, HPBar> _enemyHPBars =  new Dictionary<int, HPBar>();
    List<Popbase> _activePops = new List<Popbase>();

    public void Init(PoolManager poolManager)
    {
        _poolManager = poolManager;
    }

    public void SetHP(int instanceID, GameObject targetObject)
    {
        if (_enemyHPBars == null) _enemyHPBars = new Dictionary<int, HPBar>();
        HPBar hpBar = _poolManager.GetHPBar();
        hpBar.transform.parent = statCanvas.transform;
        
        hpBar.Init(targetObject);
        _enemyHPBars.Add(instanceID, hpBar);
    }
    
    public void ChangeHP(float val, int instanceID)
    {
        if (!_enemyHPBars.ContainsKey(instanceID)) return;
        
        _enemyHPBars[instanceID].SetHPSlider(val);
    }

    public void ReleaseHP(int instanceID)
    {
        _poolManager.ReturnHPBar(_enemyHPBars[instanceID]);
        _enemyHPBars.Remove(instanceID);
    }

    public T OpenPopup<T>(PopType popType) where T : Popbase
    {
        backGround.SetActive(true);
        _activePops.Add(popups[(int)popType]);
        popups[(int)popType].OpenPop(ClosePopup);

        return popups[(int)popType] as T;
    }

    public void ClosePopup()
    {   
        _activePops.Last().gameObject.SetActive(false);
        _activePops.Remove(_activePops.Last());

        if(_activePops.Count == 0) backGround.SetActive(false);
    }

    public void CloseAllPopups()
    {
        foreach (var popup in _activePops)
        {
            popup.gameObject.SetActive(false);
        }
        _activePops.Clear();
        backGround.SetActive(false);
    }

    public void ConnectToPlayer(Player player)
    {
        player.OnHealthChanged += userHPBar.SetHPSlider;
        player.OnStaminaChanged += userHPBar.setStaminaSlider;
        userHPBar.gameObject.SetActive(true);
    }
}
