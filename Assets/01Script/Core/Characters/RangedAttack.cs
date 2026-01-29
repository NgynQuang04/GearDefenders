using UnityEngine;

public class RangedAttack : ActorAttack
{
    [SerializeField] int damage = 10;
    [SerializeField] Transform firePoint;

    protected override void Awake()
    {
        base.Awake();
        if (firePoint == null)
            firePoint = transform;
    }

    protected override void PerformAttack(Transform target)
    {
        Vector3 dir = target.position - firePoint.position;
        ProjectilePool.Instance.Spawn(firePoint.position, dir, damage);
    }
}
