using UnityEngine;

public class GeneratorGear : BaseGear
{
    [Header("Spawn")]
    //[SerializeField] private float baseCycleTime = 4f;
    [SerializeField] private GameObject unitPrefab;

    protected override void OnPowerChanged()
    {

        UpdateSpeed();
        Debug.Log("Current speed GeneratorGear = " + CurrentSpeed);
    }

    private void Update()
    {
        if (!IsRotating) return;


        transform.Rotate(0, 0, CurrentSpeed * 100f * Time.deltaTime);
    }


    void SpawnUnit()
    {
        //Instantiate(unitPrefab, transform.position, Quaternion.identity);
        Debug.Log($"{name} spawn unit");
    }
}
