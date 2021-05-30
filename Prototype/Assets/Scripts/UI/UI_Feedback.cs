using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Feedback : MonoBehaviour
{
    public ModeFeedback CurrentMode
    {
        set { currentMode = value; }
    }
    private ModeFeedback currentMode = null;

    [SerializeField] private GameObject correct_field;
    [SerializeField] private Text txt_correct;
    [SerializeField] private GameObject wrong_field;
    [SerializeField] private Text txt_wrong;

    private GameObject currentMessage;

    private float time = 0;
    private float duration = 5;
    private bool isShowingMsg = false;

    // Start is called before the first frame update
    void Start()
    {
        correct_field.SetActive(false);
        wrong_field.gameObject.SetActive(false);
    }

    public void ShowResult(bool isCorrect, string explanation)
    {
        if (currentMode is ModeFeedbackDuring)
        {
            Debug.Log("Result " + isCorrect + " " + explanation);
            if (isShowingMsg && isCorrect) return;

            if (currentMessage != null) currentMessage.SetActive(false);

            if (isCorrect)
            {
                currentMessage = correct_field;
                txt_correct.text = explanation;
            }
            else
            {
                currentMessage = wrong_field;
                txt_wrong.text = explanation;
            }

            currentMessage.SetActive(true);

            time = 0;
            isShowingMsg = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isShowingMsg)
        {
            time += Time.deltaTime;

            if(time >= duration)
            {
                currentMessage.SetActive(false);
                isShowingMsg = false;
            }
        }
    }
}
