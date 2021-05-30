using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_ChooseMode : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem time;

    [SerializeField]
    private Toggle toggle_video;
    [SerializeField]
    private float time_duration;

    [SerializeField]
    private Toggle toggle_feedback;

    [SerializeField]
    private GameObject parent_Scenarios;
    private Scenario[] scenarios;
    [SerializeField]
    private Dropdown dropdown_scenarios;

    private ModeStop modeStop = null;
    private ModeFeedback modeFeedback = null;

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeSystem>();
        if (time == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }

        scenarios = parent_Scenarios.GetComponentsInChildren<Scenario>();

        List<string> options = new List<string>();
        foreach (var option in scenarios)
        {
            options.Add(option.name); // Or whatever you want for a label
        }
        dropdown_scenarios.ClearOptions();
        dropdown_scenarios.AddOptions(options);
    }

    private void StartTimer(ModeStop mStop)
    {
        if (!_canUseScript) return;

        modeStop = mStop;

        if (toggle_feedback.isOn) modeFeedback = new ModeFeedbackDuring();
        else modeFeedback = new ModeFeedbackAfter();

        int index = dropdown_scenarios.value;
        Scenario thisScenario = scenarios[index];

        if (toggle_video.isOn) time.StartVideo(thisScenario, modeStop, modeFeedback);
        else time.StartTime(thisScenario, modeStop, modeFeedback);

        gameObject.SetActive(false);
    }

    public void StartPlayerStopMode()
    {
        ModeStop thisMode = new ModePlayerstop();
        StartTimer(thisMode);
    }

    public void StartSystemStopMode()
    {
        ModeStop thisMode = new ModeSystemstop();
        StartTimer(thisMode);
    }

    public void StartNonStopMode()
    {
        ModeStop thisMode = new ModeNonStop();
        StartTimer(thisMode);
    }

    public void RetryMode()
    {
        if(modeStop != null) StartTimer(modeStop);
    }
}
