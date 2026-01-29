using UnityEngine;

public class RotationDebugTestBootstrap : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GridManager gridManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject gridCellPrefab;
    [SerializeField] private GameObject corePrefab;
    [SerializeField] private GameObject speedGearPrefab;
    [SerializeField] private GameObject spawnerGearPrefab;

    [Header("Grid Size")]
    [SerializeField] private int width = 6;
    [SerializeField] private int height = 4;

    private void Start()
    {
        BuildGrid();
        SpawnTestLayout();

        // ⚠️ CHỐT: rebuild toàn hệ đúng 1 lần
        RotationManager.Instance.RecalculateAll();
    }

    /* ================= GRID ================= */

    private void BuildGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = Instantiate(
                    gridCellPrefab,
                    new Vector3(x, y, 0),
                    Quaternion.identity
                );

                GridCell cell = obj.GetComponent<GridCell>();
                cell.Initialize(new GridCoord(x, y));
                gridManager.RegisterCell(cell);
            }
        }
    }

    /* ================= TEST SETUP ================= */

    private void SpawnTestLayout()
    {
        // CORE 1
        SpawnCore(1, 1);

        // CORE 2
        SpawnCore(4, 1);

        // GEARS (ban đầu tách hệ)
        SpawnSpeedGear(1, 2);
        SpawnSpawnerGear(0, 2);

        SpawnSpeedGear(4, 2);
        SpawnSpawnerGear(5, 2);

        // GEAR NỐI HỆ (bật để test merge)
        // SpawnSpeedGear(2, 2);
        // SpawnSpeedGear(3, 2);
    }

    /* ================= SPAWN HELPERS ================= */

    private void SpawnCore(int x, int y)
    {
        GridCell cell = gridManager.GetCellAtPosition(new GridCoord(x, y));
        if (cell == null) return;

        CoreGear core = Instantiate(
            corePrefab,
            cell.transform.position,
            Quaternion.identity
        ).GetComponent<CoreGear>();

        cell.PlaceCore(core, gridManager);
        core.AttachToCell(cell,gridManager);
        
    }

    private void SpawnSpeedGear(int x, int y)
    {
        SpawnGear(speedGearPrefab, x, y);
    }

    private void SpawnSpawnerGear(int x, int y)
    {
        SpawnGear(spawnerGearPrefab, x, y);
    }

    private void SpawnGear(GameObject prefab, int x, int y)
    {
        GridCell cell = gridManager.GetCellAtPosition(new GridCoord(x, y));
        if (cell == null || !cell.IsEmpty()) return;

        BaseGear gear = Instantiate(
            prefab,
            cell.transform.position,
            Quaternion.identity
        ).GetComponent<BaseGear>();

        cell.PlaceGear(gear);
        //RotationManager.Instance.RegisterGear(gear);
    }
}
