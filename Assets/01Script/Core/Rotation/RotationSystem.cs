using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private CoreGear core;

    public float AverageSpeed { get; private set; } = 1f;

    private readonly List<BaseGear> connectedGears = new();

    // DEBUG: lưu các cạnh BFS
    private readonly List<(Vector3 from, Vector3 to)> debugEdges = new();

    public void Init(GridManager grid, CoreGear core)
    {
        gridManager = grid;
        this.core = core;
    }

    public void Recalculate()
    {
        if (gridManager == null || core == null)
        {
            Debug.LogError("❌ RotationSystem chưa Init");
            return;
        }

        connectedGears.Clear();
        debugEdges.Clear();

        GridCell startCell = core.GetCell();
        if (startCell == null) return;

        Queue<BaseGear> queue = new();
        HashSet<BaseGear> visited = new();

        foreach (GridCell cell in gridManager.GetAdjacentCells4(startCell))
        {
            //if (cell.IsEmpty()) continue;

            BaseGear gear = cell.CurrentGear;
            if(gear == null) continue;

            gear.AddPower(core);

            connectedGears.Add(gear);

            queue.Enqueue(gear);   
            visited.Add(gear);

            debugEdges.Add((core.transform.position, gear.transform.position));
        }

        while (queue.Count > 0)
        {
            BaseGear currentGear = queue.Dequeue();

            GridCell currentCell = currentGear.GetCell();
            if (currentCell == null)
                continue; //  GEAR ĐANG BỊ KÉO / CHƯA SNAP

            foreach (GridCell neighbor in gridManager.GetAdjacentCells4(currentCell))
            {
                //if (neighbor.IsEmpty()) continue;

                BaseGear gear = neighbor.CurrentGear;
                if(gear == null) continue;

                if (visited.Contains(gear)) continue;

                gear.AddPower(core);

                connectedGears.Add(gear);

                visited.Add(gear);
                queue.Enqueue(gear);

                debugEdges.Add((currentGear.transform.position, gear.transform.position));
            }
        }

        CalculateSystemSpeed();
    }

    private void CalculateSystemSpeed()
    {
        int generatorCount = 0;
        float speedMultiplier = 1f;

        foreach (var gear in connectedGears)
        {
            if (gear is GeneratorGear)
                generatorCount++;

            if (gear is SpeedModifierGear speedGear)
                speedMultiplier *= speedGear.SpeedMultiplier;
        }

        Debug.Log("Count = " + generatorCount);
        float slowFactor = 1f + generatorCount * 0.3f;
        AverageSpeed = speedMultiplier / slowFactor;
        AverageSpeed = Mathf.Max(0.02f, AverageSpeed);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (var edge in debugEdges)
        {
            Gizmos.DrawLine(edge.from, edge.to);
        }
    }
#endif
}
