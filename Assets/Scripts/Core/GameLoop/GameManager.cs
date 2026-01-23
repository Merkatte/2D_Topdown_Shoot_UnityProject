using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PoolManager  poolManager;
    [SerializeField] private StatManager statManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private InputManager inputManager;

    [Header("WeaponType")] 
    [SerializeField] private WeaponType weaponType;
    
    #region Init
    public void Init()
    {
        InitiateManagers();

        StartGame();
    }
    #endregion
    
    #region private
    //Can be Delayed
    //If there`s something reason to change this method to async
    //Please use Unitask and change the Init method.
    void InitiateManagers()
    {
        //Core Init
        dataManager.Init();
        poolManager.Init();
        inputManager.Init();
        
        //GameData Init
        statManager.Init(dataManager, weaponType);
        unitManager.Init(inputManager, statManager);
    }

    void StartGame()
    {
        unitManager.SetPlayer();
    }
    #endregion
    
    #region public
    #endregion
}
