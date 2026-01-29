using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
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
    
    List<LevelData> _levelData;

    private int _playerKillCount;
    private int _playerLevel = 0;
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

        RefineData();
        
        StartGame();
    }

    void RefineData()
    {
        _levelData = new List<LevelData>();
        List<LevelConfig> levelConfigs = dataManager.GetLevelRepo().Levels;

        for (int index = 0; index < levelConfigs.Count; index++)
        {
            LevelData newLevelData = new LevelData(
                levelConfigs[index].level, levelConfigs[index].requireKillCount);
            _levelData.Add(newLevelData);
        }
    }
    
    void SelectWeapon()
    {
        uiManager.OpenPopup<WeaponSelectPop>(PopType.WeaponSelectPopup).Init(OnSelectWeapon);
    }

    void StartGame()
    {
        unitManager.SetPlayer();
    }
    
    void PlayerLevelUp()
    {
        Time.timeScale = 0;
        ++_playerLevel;
        var options = statManager.GetRandomUpgradeOptions(3);
        
        uiManager.OpenPopup<LevelUpPop>(PopType.LevelUpPopup).Init(options, OnUpgradeSelected);
    }
    #endregion
    
    #region public
    public void GameOver()
    {
        uiManager.OpenPopup<GameOverPop>(PopType.GameOverPopup).Init(OnClickRetry);
        unitManager.GameOver();
    }

    public void CheckLevelUp()
    {
        _playerKillCount++;
        if (_playerLevel >= _levelData.Count) return;
        
        if(_levelData[_playerLevel].requireKillCount <= _playerKillCount)
            PlayerLevelUp();
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
        if(selectedOption.Category == UpgradeCategory.Player)
            statManager.UpgradePlayer(selectedOption);
        else statManager.UpgradeWeapon(selectedOption);
        
        unitManager.LevelUp(selectedOption.Category);
        Time.timeScale = 1;
    }
    #endregion
}
