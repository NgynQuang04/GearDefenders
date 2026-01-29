using UnityEngine;

public class EnemyMelee : Actor
{
    private void Awake()
    {
        Team = ActorTeam.Enemy;
        Type = ActorType.Enemy;
    }
}
