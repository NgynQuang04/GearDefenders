using UnityEngine;

public class ProjectilePool : PoolBase<Projectile>
{
    public static ProjectilePool Instance { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public void Spawn(Vector3 pos, Vector3 dir, int damage)
    {
        Projectile p = Get();
        p.transform.position = pos;
        p.Init(dir, damage);
    }
}
