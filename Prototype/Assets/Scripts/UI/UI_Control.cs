using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem time;
    [SerializeField] private bool testVideo = true;

    [SerializeField] private GameObject screen_selectScenario;
    [SerializeField] private UI_SelectMode screen_selectMode;
    [SerializeField] private UI_Results screen_results;

    private Scenario thisScenario = null;
    private ModeStop modeStop = null;
    private ModeFeedback modeFeedback = null;

    void Start()
    {
        time = FindObjectOfType<TimeSystem>();
        if (time == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    // Settings

    public void ChooseScenario(Scenario newScenario)
    {
        thisScenario = newScenario;

        screen_selectMode.SetScenario(thisScenario);
        screen_selectMode.gameObject.SetActive(true);
    }
    public void SelectMode(ModeStop mStop, ModeFeedback mFeedback)
    {
        if (!_canUseScript) return;

        modeStop = mStop;
        modeFeedback = mFeedback;

        if (testVideo) time.StartVideo(thisScenario, mStop, mFeedback);
        else time.StartTime(thisScenario, mStop, mFeedback);

        gameObject.SetActive(false);
    }
    public void ShowResults(Scenario thisScenario)
    {
        screen_results.gameObject.SetActive(true);
        screen_results.ShowResults(thisScenario);
    }

    public void RetryMode()
    {
        if (thisScenario != null && modeStop != null && modeFeedback != null)
        {
            screen_results.gameObject.SetActive(false);
            SelectMode(modeStop, modeFeedback);
        }
    }

    public void ChangeMode()
    {
        screen_results.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        screen_results.gameObject.SetActive(false);
        screen_selectMode.gameObject.SetActive(false);
        screen_selectScenario.SetActive(false);
    }
}
