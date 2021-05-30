using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private Text txt_correct;
    [SerializeField] private Text txt_wrong;

    [SerializeField] private GameObject status_poor;
    [SerializeField] private GameObject status_medium;
    [SerializeField] private GameObject status_good;
    [SerializeField] private GameObject status_perfect;

    [SerializeField] private Slider sl_process;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        txt_correct.text = "CORRECT: " + Scores.Correct;
        txt_wrong.text = "WRONG: " + Scores.Correct;

        float percentage = Scores.Correct / Scores.Total;
        sl_process.value = percentage;
        SetStatus(percentage * 100);
    }

    private void SetStatus(float percentage)
    {
        status_poor.SetActive(false);
        status_medium.SetActive(false);
        status_good.SetActive(false);
        status_perfect.SetActive(false);

        if (percentage < 25) status_poor.SetActive(true);
        else if (percentage < 50) status_medium.SetActive(true);
        else if (percentage < 90) status_good.SetActive(true);
        else status_perfect.SetActive(true);
    }
}
