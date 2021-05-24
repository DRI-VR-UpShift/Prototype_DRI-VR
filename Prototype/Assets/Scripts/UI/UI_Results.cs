using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Results : MonoBehaviour
{
    [SerializeField] private Text txt_Correct;
    [SerializeField] private Text txt_Total;

    [SerializeField]
    UI_ChooseMode chooseMenu;

    public void ShowResults(int correct, int total)
    {
        txt_Correct.text = "" + correct;
        txt_Total.text = "" + total;
    }

    public void Btn_Retry()
    {
        chooseMenu.RetryMode();
        gameObject.SetActive(false);
    }

    public void Btn_ChangeMode()
    {
        chooseMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Btn_MainMenu()
    {
        chooseMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
