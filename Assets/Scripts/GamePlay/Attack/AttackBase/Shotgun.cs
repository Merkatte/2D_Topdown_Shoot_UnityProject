using UnityEngine;

public class Shotgun : AttackBase
{
    private float spread = 30;
    private float randomSpeedRate = 60f;
    protected override void Attack(Vector2 startPosition, Vector2 direction)
    {
        for (int index = 0; index < curStatData.BulletNum_PerShot; index++)
        {
            BulletMove newBullet = _poolManager.GetObject();
            float randomAngle = Random.Range(-spread / 2f, spread / 2f);
            Vector2 randomDir = RotateVector(direction, randomAngle);
            float randomSpeed = Random.Range(curStatData.BulletSpeed - (curStatData.BulletSpeed / 100 * randomSpeedRate), curStatData.BulletSpeed);
            newBullet.Init(randomSpeed, randomDir, startPosition, curStatData.BulletDistance, _poolManager);
        }
    }
    
    private Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}
