using UnityEngine;

public class UnitSpawner : ActorSpawner
{
    [SerializeField] Transform spawnPoint;

    protected override void Spawn()
    {
        pool.Spawn(spawnPoint.position);
    }
}
