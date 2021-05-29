using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectScenario : MonoBehaviour
{
    [SerializeField] private Transform parent_Scenarios;
    private Scenario[] scenarios;

    [SerializeField] private Transform parent_MenuScenarios;
    [SerializeField] private GameObject prefab_menuItem_Scenario;

    // Start is called before the first frame update
    void Start()
    {
        scenarios = parent_Scenarios.GetComponentsInChildren<Scenario>();

        foreach(Scenario item in scenarios)
        {
            GameObject menuItem = Instantiate(prefab_menuItem_Scenario, parent_MenuScenarios);
            UI_ScenarioItem scenarioItem = menuItem.GetComponent<UI_ScenarioItem>();
            scenarioItem.SetScenario(item);
        }
    }
}
