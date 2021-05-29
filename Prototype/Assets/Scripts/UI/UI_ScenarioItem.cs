using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ScenarioItem : MonoBehaviour
{
    private Scenario thisScenario;

    [SerializeField] private Text txt_title;
    [SerializeField] private Text txt_description;
    [SerializeField] private Image bg_image;

    public void SetScenario(Scenario scenario)
    {
        thisScenario = scenario;

        txt_title.text = thisScenario.Name;
        txt_description.text = thisScenario.Description;

        bg_image = thisScenario.Img;
    }

    public void ChooseScenario()
    {
        UI_Control ui_control = GameObject.FindObjectOfType<UI_Control>();
        if(ui_control != null)
        {
            ui_control.ChooseScenario(thisScenario);
        }
    }
}
