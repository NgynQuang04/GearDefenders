using UnityEngine;

public class Archer : Actor
{
    private void Awake()
    {
        Team = ActorTeam.Player;
        Type = ActorType.Unit;
    }
}
