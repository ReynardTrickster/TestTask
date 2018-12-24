using UnityEngine;
using UnityEngine.UI;

public class ToggleHandler : MonoBehaviour
{
    Toggle toggle;
    UnitSpawner spawner;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
        spawner = GameObject.FindGameObjectWithTag("UnitSpawner").GetComponent<UnitSpawner>();

    }

    void ToggleValueChanged(Toggle change)
    {
        if (toggle.isOn)
            spawner.ChangeUnitsBehavior(Unit.Behavior.Competition);
        else
            spawner.ChangeUnitsBehavior(Unit.Behavior.Journey);
    }
}