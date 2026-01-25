using UnityEngine;

public interface IPoolManager
{
    public BulletMove GetBulletMove();
    public Enemy GetEnemy();
    public void ReturnBullet(BulletMove obj);
    public void ReturnEnemy(Enemy obj);
}
