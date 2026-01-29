using UnityEngine;
using UnityEngine.Rendering;

public class UnitMovement : ActorMovement
{
    protected override void MoveForward()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
