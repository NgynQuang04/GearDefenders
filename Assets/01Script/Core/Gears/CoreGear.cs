using UnityEngine;


public class CoreGear : MonoBehaviour, IPowerSource
{
    private GridCell currentCell;
    private RotationSystem rotationSystem;

    /* ================= LIFECYCLE ================= */

    private void Awake()
    {
        rotationSystem = GetComponent<RotationSystem>();
        if (rotationSystem == null)
            Debug.LogError("❌ CoreGear thiếu RotationSystem");
    }

    private void Start()
    {
        // Đăng ký hệ quay
        RotationManager.Instance.RegisterSystem(rotationSystem);
    }

    public RotationSystem GetRotationSystem()
    {
        return rotationSystem;
    }


    /* ================= GRID ================= */

    public void AttachToCell(GridCell cell, GridManager gridManager)
    {
        currentCell = cell;
        transform.position = cell.transform.position;

        rotationSystem.Init(gridManager, this);
    }

    public GridCell GetCell()
    {
        return currentCell;
    }

    /* ================= DEBUG ================= */

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.35f);
    }
#endif
}
