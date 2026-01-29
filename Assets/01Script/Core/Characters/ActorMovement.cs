using UnityEngine;

public class ActorMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 2.5f;
    [SerializeField] protected float stopDistance = 1.5f;

    [SerializeField] ModeMove modeMove = ModeMove.Forward;

    Actor actor;
    ActorTargeting targeting;

    private void Awake()
    {
        actor = GetComponent<Actor>();
        targeting = GetComponent<ActorTargeting>();
    }

    private void Update()
    {
        if (!actor.IsAlive) return;

        UpdateMoveMode();

        switch (modeMove)
        {
            case ModeMove.Forward:
                MoveForward();
                break;

            case ModeMove.ToTarget:
                MoveToTarget();
                break;
        }
    }

    void UpdateMoveMode()
    {
        if (targeting.CurrentTarget != null)
            modeMove = ModeMove.ToTarget;
        else
            modeMove = ModeMove.Forward;
    }

    protected virtual void MoveForward()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void MoveToTarget()
    {
        var target = targeting.CurrentTarget;
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;

        if (dir.sqrMagnitude <= stopDistance * stopDistance)
            return;

        transform.position += dir.normalized * speed * Time.deltaTime;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
#endif
}

public enum ModeMove
{
    Forward,
    ToTarget
}
