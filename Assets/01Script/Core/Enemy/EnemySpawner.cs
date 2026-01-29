using UnityEngine;

public class EnemySpawner : ActorSpawner
{
    [SerializeField] Transform spawnPoint;

    protected override void Spawn()
    {
        pool.Spawn(spawnPoint.position);
    }
}
