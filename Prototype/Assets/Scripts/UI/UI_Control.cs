using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    [SerializeField] private GameObject screen_main;
    [SerializeField] private GameObject screen_quit;
    [SerializeField] private GameObject screen_settings;
    [SerializeField] private GameObject screen_selectScenario;
    [SerializeField] private UI_SelectMode screen_selectMode;
    [SerializeField] private GameObject screen_results;
    [SerializeField] private GameObject screen_scenarioUI;

    private Scenario thisScenario = null;

    #region Quit App
    public void QuitApp()
    {
        Application.Quit();
    }
    #endregion

    // Settings

    #region Select Scenario
    public void ChooseScenario(Scenario newScenario)
    {
        thisScenario = newScenario;

        screen_selectMode.SetScenario(thisScenario);
        screen_selectMode.gameObject.SetActive(true);
    }
    #endregion

    #region Select Mode
    public void SelectMode(Scenario thisScenario, ModeStop stopping, ModeFeedback feedback)
    {

    }
    #endregion
}
