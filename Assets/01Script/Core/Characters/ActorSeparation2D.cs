using UnityEngine;

public class ActorSeparation2D : MonoBehaviour
{
    [SerializeField] float separationRadius = 0.25f;
    [SerializeField] float separationForce = 1f;
    [SerializeField] LayerMask actorLayer;

    void Update()
    {
        Separate();
    }

    void Separate()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            separationRadius,
            actorLayer
        );

        Vector2 pushDir = Vector2.zero;
        int count = 0;

        foreach (var hit in hits)
        {
            if (hit.transform == transform) continue;

            Vector2 dir = (Vector2)(transform.position - hit.transform.position);
            float dist = dir.magnitude;

            if (dist < 0.001f) continue;

            pushDir += dir.normalized / dist;
            count++;
        }

        if (count > 0)
        {
            pushDir /= count;
            transform.position += (Vector3)(pushDir * separationForce * Time.deltaTime);
        }
    }
}
