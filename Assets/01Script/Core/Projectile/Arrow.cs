using UnityEngine;

public class Arrow : Projectile
{
    [SerializeField] float arrowSpeed = 12f;

    protected override void Shoot()
    {
        rb.linearVelocity = direction * arrowSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out ActorHealth actorHealth))
        {
            actorHealth.TakeDamage(damage);
            ProjectilePool.Instance.Return(this);
        }
    }
}
