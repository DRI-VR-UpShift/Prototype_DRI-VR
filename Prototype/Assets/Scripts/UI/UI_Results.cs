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

    public void ShowResults(Scenario scenario)
    {
        int totalShouldHit = 0;
        int totalHasHit = 0;

        int totalShouldNotHit = 0;
        int totalNotHit = 0;

        foreach (Hitbox box in scenario.listHitbox)
        {
            if (box.ShouldHit)
            {
                if (box.HasBeenHit) totalHasHit++;
                totalShouldHit++;
            }
            else
            {
                if (!box.HasBeenHit) totalNotHit++;
                totalShouldNotHit++;
            }
        }

        txt_correct.text = ("Should have hit: " + totalHasHit + " / " + totalShouldHit);
        txt_wrong.text = ("Should not have hit: " + totalNotHit + " / " + totalShouldNotHit);

        float percentage = (totalHasHit + totalNotHit) / (totalShouldNotHit + totalShouldHit);
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
