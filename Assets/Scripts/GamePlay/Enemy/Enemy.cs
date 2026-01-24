using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyStatData _myStatData;
    public void Init(EnemyStatData enemyStatData)
    {
        _myStatData = enemyStatData;
    }
}
