using UnityEngine;

public abstract class ActorSpawner : MonoBehaviour
{
    [SerializeField] protected ActorPool pool;
    [SerializeField] protected float spawnInterval = 2f;

    protected float timer;

    protected virtual void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = spawnInterval;
        }
    }

    protected abstract void Spawn();
}
