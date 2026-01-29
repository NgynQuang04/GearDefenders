using System.Collections;
using UnityEngine;

public abstract class ActorAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] protected float attackInterval = 1f;
    [SerializeField] protected float attackRange = 2f;

    [Header("Visual Tilt")]
    [SerializeField] Transform visual; // ⭐ Transform con
    [SerializeField] float tiltAngle = 15f;
    [SerializeField] float tiltDuration = 0.1f;

    protected float timer;

    protected Actor actor;
    protected ActorTargeting targeting;

    Coroutine tiltRoutine;
    Quaternion visualOriginRot;

    protected virtual void Awake()
    {
        actor = GetComponent<Actor>();
        targeting = GetComponent<ActorTargeting>();
    }

    private void Update()
    {
        if (!actor.IsAlive) return;
        if(targeting.CurrentTarget == null) return;

        float distSqr = (targeting.CurrentTarget.position - transform.position).sqrMagnitude;
        if (distSqr > attackRange * attackRange)
            return;

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            PerformAttack(targeting.CurrentTarget);
            PlayAttackTilt(targeting.CurrentTarget);
            timer = attackInterval;
        }
    }

    protected virtual void PerformAttack(Transform target) { }

    void PlayAttackTilt(Transform target)
    {
        if (visual == null) return;

        if (tiltRoutine != null)
            StopCoroutine(tiltRoutine);

        tiltRoutine = StartCoroutine(TiltRoutine(target));
    }

    IEnumerator TiltRoutine(Transform target)
    {
        Vector3 dir = target.position - transform.position;
        float sign = Mathf.Sign(dir.x);

        Quaternion tilt =
            Quaternion.Euler(0, 0, -sign * tiltAngle);

        visual.localRotation = tilt;
        yield return new WaitForSeconds(tiltDuration);
        visual.localRotation = visualOriginRot;
    }
}


