using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public static RotationManager Instance { get; private set; }

    private readonly List<RotationSystem> systems = new();
    private readonly List<BaseGear> allGears = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterSystem(RotationSystem system)
    {
        if (!systems.Contains(system))
            systems.Add(system);
    }

    public void RegisterGear(BaseGear gear)
    {
        if (!allGears.Contains(gear))
            allGears.Add(gear);
    }

    public void RecalculateAll()
    {
        // 1. RESET POWER
        foreach (var gear in allGears)
            gear.ClearPower();

        // 2. BFS TỪ MỖI CORE
        foreach (var system in systems)
            system.Recalculate();

        // 3. THU THẬP DỮ LIỆU
        /*int generatorGearCount = 0;
        float speedMultiplier = 1f;

        foreach (var gear in allGears)
        {
            if (!gear.IsRotating) continue;

            if (gear is GeneratorGear)
                generatorGearCount++;

            if (gear is SpeedModifierGear speedGear)
                speedMultiplier *= speedGear.SpeedMultiplier;
        }*/

    }
}
