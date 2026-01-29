using UnityEngine;

public abstract class Projectile : MonoBehaviour, IPoolable
{
    protected Rigidbody2D rb;
    protected int damage;
    protected Vector2 direction;

    [SerializeField] protected float lifeTime;
    float timer;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
            ProjectilePool.Instance.Return(this);
    }

    public virtual void Init(Vector3 dir, int dmg)
    {
        direction = dir;
        damage = dmg;
        Shoot();
    }

    protected virtual void Shoot() { }

    public void OnSpawned()
    {
        timer = lifeTime;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnDespawned()
    {
        rb.linearVelocity = Vector2.zero;
    }

}
