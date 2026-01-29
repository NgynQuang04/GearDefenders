using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGear : MonoBehaviour
{
    protected GridCell currentCell;

    public float CurrentSpeed { get; private set; }

    // Các nguồn quay (core)
    protected readonly HashSet<IPowerSource> powerSources = new();


    public bool IsRotating => powerSources.Count > 0;


    


    /* ================= CELL ================= */

    public void AttachToCell(GridCell cell)
    {
        currentCell = cell;
        transform.position = cell.transform.position;
    }

    public GridCell GetCell() => currentCell;

    /* ================= POWER ================= */

    public void ClearPower()
    {
        powerSources.Clear();
        OnPowerChanged();
    }

    public void AddPower(IPowerSource source)
    {
        if (powerSources.Add(source))
            OnPowerChanged();
    }

    public void UpdateSpeed()
    {
        if (!IsRotating)
        {
            CurrentSpeed = 0f;
            return;
        }

        float sum = 0f;
        foreach (var src in powerSources)
        {
            if (src is CoreGear core)
                sum += core.GetRotationSystem().AverageSpeed;
        }

        CurrentSpeed = sum ;
    }


    protected abstract void OnPowerChanged();

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (IsRotating)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.25f);
        }
    }
#endif

}
