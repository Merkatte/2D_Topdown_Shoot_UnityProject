using UnityEngine;

public interface IPoolManager
{
    public BulletMove GetObject();
    public void ReturnObject(BulletMove obj);
}
