using UnityEngine;

public class GearDragHandler : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private LayerMask gearLayer;
    [SerializeField] private float snapRadius = 1f;

    private Camera cam;
    private BaseGear gear;

    private bool isDragging;
    private Vector3 dragOffset;

    private void Awake()
    {
        cam = Camera.main;
        gear = GetComponent<BaseGear>();
    }

    private void Update()
    {
        HandleInput();
    }

    /* ================= INPUT ================= */

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryPickThisGear();

        if (Input.GetMouseButton(0) && isDragging)
            Drag();

        if (Input.GetMouseButtonUp(0) && isDragging)
            Release();
    }

    /* ================= PICK ================= */

    void TryPickThisGear()
    {
        Vector3 mouseWorld = GetMouseWorld();

        RaycastHit2D hit = Physics2D.Raycast(
            mouseWorld,
            Vector2.zero,
            0f,
            gearLayer
        );

        // Không click trúng chính gear này → bỏ
        if (!hit || hit.collider.gameObject != gameObject)
            return;

        isDragging = true;
        dragOffset = transform.position - mouseWorld;

        // Nhấc khỏi cell nhưng CHƯA recalc
        if (gear.GetCell() != null)
            gear.GetCell().RemoveGear();
    }

    /* ================= DRAG ================= */

    void Drag()
    {
        transform.position = GetMouseWorld() + dragOffset;
    }

    /* ================= RELEASE ================= */

    void Release()
    {
        TrySnapToCell();
        isDragging = false;
    }

    /* ================= SNAP ================= */

    void TrySnapToCell()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            snapRadius
        );

        GridCell best = null;
        float minDist = float.MaxValue;

        foreach (var hit in hits)
        {
            GridCell cell = hit.GetComponent<GridCell>();
            if (cell == null || !cell.IsEmpty()) continue;

            float d = Vector3.Distance(
                transform.position,
                cell.transform.position
            );

            if (d < minDist)
            {
                minDist = d;
                best = cell;
            }
        }

        if (best != null)
        {
            best.PlaceGear(gear);
            //RotationManager.Instance.RecalculateAll();
        }
    }

    /* ================= UTIL ================= */

    Vector3 GetMouseWorld()
    {
        Vector3 p = Input.mousePosition;
        p.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(p);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (isDragging)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, snapRadius);
        }
    }
#endif
}
