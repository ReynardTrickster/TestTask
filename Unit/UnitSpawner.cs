using UnityEngine;

public class UnitSpawner : MonoBehaviour {
    public Unit[] Units {get; private set;}

    [SerializeField]
    GameObject unitPrefab;

    const int minUnitsCount = 1;
    const int maxUnitsCount = 5;

    const float spawnDelay = 2.0f;
    const float spawnCooldown = 1.0f;

    int unitsCountLimit;
    int unitsCount;

    void Start () {
        unitsCountLimit = Random.Range(minUnitsCount, maxUnitsCount);
        Units = new Unit[unitsCountLimit];
        InvokeRepeating("Spawn", spawnDelay, spawnCooldown);
        Spawn();
    }
	
    void Spawn()
    {
        GameObject unit = Instantiate(unitPrefab, transform);
        unit.transform.localPosition += new Vector3(0, 0.2f, 0);

        Units[unitsCount] = unit.GetComponent<Unit>();

        unitsCount += 1;
        if (unitsCount >= unitsCountLimit)
            CancelInvoke("Spawn");
    }

    public void ChangeUnitsBehavior(Unit.Behavior behavior)
    {
        if (Units.Length > 0)
            foreach (Unit unit in Units)
            {
                unit.ChangeBehavior(behavior);
            }
    }
}
