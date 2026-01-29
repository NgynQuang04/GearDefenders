using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private Dictionary<Vector2Int, GridCell> gridCells = new();

    // Expose readonly để system khác duyệt
    public IEnumerable<GridCell> AllCells => gridCells.Values;

    // Đăng ký một ô lưới vào quản lý
    public void RegisterCell(GridCell cell)
    { 
        Vector2Int position = new Vector2Int(cell.GridPosition.x, cell.GridPosition.y);

        if(gridCells.ContainsKey(position))
        {
            Debug.LogError("GridManager: Cell already exists at " + position);
            return;
        }

        gridCells.Add(position, cell);
    }

    // Lấy ô lưới tại vị trí cụ thể
    public GridCell GetCellAtPosition(GridCoord gridPosition)
    {
        Vector2Int position = new Vector2Int(gridPosition.x, gridPosition.y);

        GridCell cell;
        if(gridCells.TryGetValue(position, out cell))
        {
            return cell;
        }
        return null;
    }

    // Lấy các ô kề 4 hướng
    public List<GridCell> GetAdjacentCells4(GridCell centerCell)
    {
        List<GridCell> neighbors = new List<GridCell>();

        GridCoord gridPosition = centerCell.GridPosition;

        AddNeighborIfExists(neighbors, new GridCoord(gridPosition.x + 1, gridPosition.y)); // Right
        AddNeighborIfExists(neighbors, new GridCoord(gridPosition.x - 1, gridPosition.y)); // Left
        AddNeighborIfExists(neighbors, new GridCoord(gridPosition.x, gridPosition.y + 1)); // Up
        AddNeighborIfExists(neighbors, new GridCoord(gridPosition.x, gridPosition.y - 1)); // Down  

        return neighbors;
    }

    private void AddNeighborIfExists(List<GridCell> neighborList, GridCoord gridPostion)
    {
        GridCell gridCell = GetCellAtPosition(gridPostion);

        if (gridCell != null)
        {
            neighborList.Add(gridCell);
        }
    }
}
