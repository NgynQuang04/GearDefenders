using UnityEngine;

public class GridCell : MonoBehaviour
{

    public GridCoord GridPosition { get; private set; }

  
    public BaseGear CurrentGear { get; private set; }
    public CoreGear CurrentCore { get; private set; }

    // Khởi tạo vị trí cho ô (chỉ gọi 1 lần khi setup grid)
    public void Initialize(GridCoord gridPosition)
    {
        GridPosition = gridPosition;
    }


    public bool IsEmpty()
    {
        return CurrentGear == null && CurrentCore == null;
    }

    // Đặt một gear vào ô này
    public void PlaceGear(BaseGear gear)
    {
        CurrentGear = gear;
        gear.AttachToCell(this);

        RotationManager.Instance.RegisterGear(gear);
        RotationManager.Instance.RecalculateAll();
    }

    public void PlaceCore(CoreGear core, GridManager gridManager)
    {
        CurrentCore = core;
        core.AttachToCell(this, gridManager);
    }
    // Gỡ gear khỏi ô (ô trở về trạng thái trống)
    public void RemoveGear()
    {
        CurrentGear = null;
    }
}
