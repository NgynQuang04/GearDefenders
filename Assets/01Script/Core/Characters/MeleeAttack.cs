using UnityEngine;

public class MeleeAttack : ActorAttack
{
    [SerializeField] int damage = 10;
    [SerializeField] float range = 1.5f;

    protected override void PerformAttack(Transform target)
    {
        Vector3 dir = target.transform.position - transform.position;
        if (dir.sqrMagnitude > range * range)
        {
            return;
        }

        if(target.TryGetComponent(out ActorHealth actorHealth))
        {
            actorHealth.TakeDamage(damage);
        }
    }
}
