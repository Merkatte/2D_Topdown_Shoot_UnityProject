using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PoolManager  poolManager;
    [SerializeField] private StatManager statManager;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UnitManager unitManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIManager uiManager;

    [Header("WeaponType")] 
    [SerializeField] private WeaponType weaponType;
    
    #region Init
    public void Init()
    {
        InitiateManagers();
    }
    #endregion
    
    #region private
    void InitiateManagers()
    {
        //Core Init
        dataManager.Init();
        poolManager.Init();
        inputManager.Init();
        uiManager.Init(poolManager);
        
        SelectWeapon();
    }

    void InitlateManager()
    {
        //GameData Init
        statManager.Init(dataManager, poolManager, weaponType);
        unitManager.Init(this, inputManager, statManager, poolManager, uiManager);

        StartGame();
    }

    void SelectWeapon()
    {
        uiManager.OpenPopup<WeaponSelectPop>(PopType.WeaponSelectPopup).Init(OnSelectWeapon);
    }

    void StartGame()
    {
        unitManager.SetPlayer();
    }
    #endregion
    
    #region public
    public void GameOver()
    {
        uiManager.OpenPopup<GameOverPop>(PopType.GameOverPopup).Init(OnClickRetry);
        unitManager.GameOver();
    }

    public void PlayerLevelUp()
    {
        Time.timeScale = 0;

        var options = statManager.GetRandomUpgradeOptions(3);
        
        uiManager.OpenPopup<LevelUpPop>(PopType.LevelUpPopup).Init(options, OnUpgradeSelected);
    }
    #endregion
    
    #region ButtonEvent

    void OnClickRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnSelectWeapon(WeaponType selectedWeapon)
    {
        weaponType = selectedWeapon;
        InitlateManager();
    }

    void OnUpgradeSelected(UpgradeOption selectedOption)
    {
        
    }
    #endregion
}
