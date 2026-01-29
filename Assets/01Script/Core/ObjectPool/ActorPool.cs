using UnityEngine;

public class ActorPool : PoolBase<Actor>
{
    public virtual Actor Spawn(Vector3 pos)
    {
        Actor actor = Get();
        actor.transform.position = pos;
        actor.Init(this);
        return actor;

    }

    
}
