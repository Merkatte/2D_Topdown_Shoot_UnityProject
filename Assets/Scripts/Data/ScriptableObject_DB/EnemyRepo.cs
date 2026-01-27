using UnityEngine;

[CreateAssetMenu(fileName = "EnemyRepo", menuName = "Scriptable Objects/EnemyRepo")]
public class EnemyRepo : ScriptableObject
{
    public float Health;
    public int FireRate;
    public float Damage;
    public float Armor;
    public float MoveSpeed;
    public float Acceleration;
    public float Deceleration;
}
