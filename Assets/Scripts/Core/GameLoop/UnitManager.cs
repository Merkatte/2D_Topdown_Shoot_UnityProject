using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Dictionary<int, Enemy> enemiesDict;

    [SerializeField] private float noSpawnArea;
    
    private Dictionary<int, int> enemiesHPDict;
    private float playerHP;
    
    private InputManager _inputManager;
    private StatManager _statManager;
    
    #region Init

    public void Init(InputManager inputManager, StatManager statManager)
    {
        _inputManager = inputManager;
        _statManager = statManager;
    }

    #endregion

    #region private

    #endregion

    #region public

    public void SetPlayer()
    {
        player.Init(_inputManager.GetPlayerInput(), _statManager.GetPlayerOriginStatData(), _statManager.GetBulletOriginData());
    }

    #endregion

    #region event

    void OnPlayerHit(float damage)
    {
        
    }

    void OnEnemyHit(int instnaceID, float damage)
    {
        
    }

#endregion
}
