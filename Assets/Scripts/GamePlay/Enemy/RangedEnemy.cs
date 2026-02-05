using UnityEngine;

public class RangedEnemy : Enemy
{
    protected override void Move()
    {
        if (Vector2.Distance(_myTargetObject.transform.position, transform.position) <= 10f)
        {
            Attack();
        }
        base.Move();
    }

    void Attack()
    {
        
    }

}
