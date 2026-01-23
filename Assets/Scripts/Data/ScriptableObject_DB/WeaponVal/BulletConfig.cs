using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "BulletConfig", menuName = "Scriptable Objects/BulletConfig")]
public class BulletConfig : ScriptableObject
{
    public float Damage;
    [FormerlySerializedAs("Speed")] public float BulletSpeed;
    public float KnockBack;
    public float BulletDistance;
    public int FireRate;
    public int BulletNum_PerShot;
}
