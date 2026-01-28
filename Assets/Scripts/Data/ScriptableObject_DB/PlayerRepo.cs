using UnityEngine;

[CreateAssetMenu(fileName = "PlayerRepo", menuName = "Scriptable Objects/PlayerRepo")]
public class PlayerRepo : ScriptableObject
{
    [Header("Health Stat")]
    public float Health;
    public float Stamina;
    public float StaminaRecovery;

    [Header("Armor Stat")]
    public float Armor;
    
    [Header("Movement Stat")]
    public float MoveSpeed;
    public float Acceleration;
    public float Deceleration;
    
    [Header("Dash Stat")]
    public float DashSpeed;
    public float DashDuration;
    public float RequestDashStamina;
}
