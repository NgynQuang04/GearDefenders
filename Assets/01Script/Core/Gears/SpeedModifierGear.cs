using UnityEngine;

public class SpeedModifierGear : BaseGear
{
    [Header("Speed Modifier")]
    [SerializeField] private float speedMultiplier = 1f;

    public float SpeedMultiplier => speedMultiplier;

    private void Update()
    {
        if (!IsRotating) return;

        transform.Rotate(0, 0, CurrentSpeed * 100f * Time.deltaTime);
    }


    protected override void OnPowerChanged()
    {
        UpdateSpeed();
        Debug.Log("Current speed SpeedGear = " + CurrentSpeed);
    }
}
