using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRepo", menuName = "Scriptable Objects/SpawnRepo")]
public class SpawnRepo : ScriptableObject
{
    public Vector2 MinSpawnPoint;
    public Vector2 MaxSpawnPoint;
    public float NoSpawnArea;
    public int SpawnDelay;
    public int MaxSpawnDelay;
    public int MaxSpawnCount;
    public int SpawnRatePerOnce;
}
