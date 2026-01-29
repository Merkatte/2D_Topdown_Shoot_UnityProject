using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "WaveRepo", menuName = "Scriptable Objects/WaveRepo")]
public class WaveRepo : ScriptableObject
{
    public int WaveInterval;
    [FormerlySerializedAs("EnemyStatMultipolier")] public float EnemyStatMultiplier;
    public int MaxEnemyIncrease;
    public float SpawnRateMultiplier;
}
