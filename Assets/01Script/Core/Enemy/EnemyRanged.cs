using UnityEngine;

public class EnemyRanged : Actor
{
    private void Awake()
    {
        Team = ActorTeam.Enemy;
        Type = ActorType.Enemy;
    }
}
