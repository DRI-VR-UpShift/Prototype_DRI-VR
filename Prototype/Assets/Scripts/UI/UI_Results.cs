using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Results : MonoBehaviour
{
    [SerializeField] private Text txt_correct;
    [SerializeField] private Text txt_wrong;

    [SerializeField] UI_ChooseMode chooseMenu;
    [SerializeField] UI_Control ui_control;

    [SerializeField] Image img_Score;

    public void ShowResults()
    {
        txt_correct.text = "CORRECT: " + Scores.Correct;
        txt_wrong.text = "WRONG: " + Scores.Correct;

        float percentage = (score.totalHasHit + score.totalNotHit) / (score.totalShouldNotHit + score.totalShouldHit);
        img_Score.fillAmount = percentage;
    }

    public void Btn_Retry()
    {
        if (chooseMenu != null)
        {
            chooseMenu.RetryMode();
            gameObject.SetActive(false);
        }
        else if(ui_control != null) ui_control.RetryMode();
    }

    public void Btn_ChangeMode()
    {
        if (chooseMenu != null)
        {
            chooseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (ui_control != null) ui_control.ChangeMode();
    }

    public void Btn_MainMenu()
    {
        if (chooseMenu != null)
        {
            chooseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (ui_control != null) ui_control.MainMenu();
    }
}
