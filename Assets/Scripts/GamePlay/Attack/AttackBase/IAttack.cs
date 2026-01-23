using System;
using UnityEngine;
public interface IAttack
{
    public void Init(BulletStatData data);
    public void OrderAttack(Vector2 position, Vector2 direction);
    public void Upgrade(BulletStatData data);
}
