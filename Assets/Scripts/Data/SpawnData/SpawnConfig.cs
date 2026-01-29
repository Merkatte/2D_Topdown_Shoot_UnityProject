using UnityEngine;

public struct SpawnData
{
    public readonly Vector2 MinSpawnPoint;
    public readonly Vector2 MaxSpawnPoint;
    public readonly float NoSpawnArea;
    public readonly int SpawnDelay;
    public readonly int MaxSpawnDelay;
    public readonly int MaxSpawnCount;
    public readonly int SpawnRatePerOnce;

    public SpawnData(Vector2 minSpawnPoint, Vector2 maxSpawnPoint, float noSpawnArea, int spawnDelay, int maxSpawnDelay, int maxSpawnCount, int spawnRatePerOnce)
    {
        MinSpawnPoint = minSpawnPoint;
        MaxSpawnPoint = maxSpawnPoint;
        NoSpawnArea = noSpawnArea;
        SpawnDelay = spawnDelay;
        MaxSpawnDelay = maxSpawnDelay;
        MaxSpawnCount = maxSpawnCount;
        SpawnRatePerOnce = spawnRatePerOnce;
    }
}
