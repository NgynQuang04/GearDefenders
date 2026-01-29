using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public ActorTeam Team { get; protected set; }
    public ActorType Type { get; protected set; }

    public bool IsAlive { get; protected set; }

    protected ActorPool pool;

    public void Init(ActorPool pool)
    {
        this.pool = pool;
    }

    protected virtual void OnEnable()
    {
        IsAlive = true;
    }

    public virtual void Kill()
    {
        IsAlive = false;
        pool.Return(this);
    }

}

public enum ActorTeam
{
    Player,
    Enemy
}

public enum ActorType
{
    Unit,
    Enemy,
    Wall,
    Boss
}
