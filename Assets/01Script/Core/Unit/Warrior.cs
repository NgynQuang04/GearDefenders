using UnityEngine;

public class Warrior : Actor
{
    private void Awake()
    {
        Team = ActorTeam.Player;
        Type = ActorType.Unit;
    }
}
