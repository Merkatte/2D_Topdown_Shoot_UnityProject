using UnityEngine;

public struct WaveData
{
    public readonly int WaveInterval;
    public readonly float EnemyStatMultiplier;
    public readonly int MaxEnemyIncrease;
    public readonly float SpawnRateMultiplier;

    public WaveData(int waveInterval, float enemyStatMultiplier, int maxEnemyIncrease, float spawnRateMultiplier)
    {
        WaveInterval = waveInterval;
        EnemyStatMultiplier = enemyStatMultiplier;
        MaxEnemyIncrease = maxEnemyIncrease;
        SpawnRateMultiplier = spawnRateMultiplier;
    }
}
