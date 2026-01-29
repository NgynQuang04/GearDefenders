using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ActorTargeting : MonoBehaviour
{
    [SerializeField] float dectectRadius = 8f;

    [SerializeField] TargetPriorityProfile priorityProfile;

    public Transform CurrentTarget {  get; private set; }

    Actor owner;

    // ===== DEBUG =====
    List<Actor> debugCandidates = new();
    Actor debugChosen;

    private void Awake()
    {
        owner = GetComponent<Actor>();
    }

    void OnEnable()
    {
        ClearTarget(); // ⭐ CỰC KỲ QUAN TRỌNG
    }


    private void Update()
    {
        if (CurrentTarget != null) return;

        FindTarget();
    }

    void FindTarget()
    {
        debugCandidates.Clear();
        debugChosen = null;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, dectectRadius);

        foreach (Collider2D hit in hits)
        {
            if (!hit.TryGetComponent(out Actor actor)) continue;
            if (!actor.IsAlive) continue;
            if (actor.Team == owner.Team) continue;

            debugCandidates.Add(actor);
        }

        if (debugCandidates.Count == 0)
            return;

        debugChosen = ChooseByPriority(debugCandidates);

        if (debugChosen != null)
            CurrentTarget = debugChosen.transform;

    }

    Actor ChooseByPriority(List<Actor> actors)
    {
        // Ưu tiên theo profile
        foreach (var type in priorityProfile.priorityOrder)
        {
            Actor best = null;
            float minDist = float.MaxValue;

            foreach (var actor in actors)
            {
                if(actor.Type != type) continue;

                float d = (actor.transform.position - transform.position).sqrMagnitude;
                if (d < minDist)
                {
                    best = actor;
                    minDist = d;
                }
            }
            if (best != null) 
                return best;
        }
        return null;
    }

    public void ClearTarget()
    {
        CurrentTarget = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 1. Detect radius
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, dectectRadius);

        // 2. Candidate actors
        Gizmos.color = Color.yellow;
        foreach (var a in debugCandidates)
        {
            if (a == null) continue;
            Gizmos.DrawLine(transform.position, a.transform.position);
            Gizmos.DrawWireSphere(a.transform.position, 0.2f);
        }

        // 3. Chosen target
        if (debugChosen != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, debugChosen.transform.position);
            Gizmos.DrawWireSphere(debugChosen.transform.position, 0.35f);
        }
    }
#endif
}
