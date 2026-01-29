using UnityEngine;

public class EnemyMovement : ActorMovement
{
    protected override void MoveForward()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
