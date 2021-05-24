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
    private GameObject parent_Scenarios;
    private Scenario[] scenarios;
    [SerializeField]
    private Dropdown dropdown_scenarios;

    private Mode currentMode = null;

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

    private void StartTimer(Mode thisMode)
    {
        if (!_canUseScript) return;

        currentMode = thisMode;

        int index = dropdown_scenarios.value;
        Scenario thisScenario = scenarios[index];

        if (toggle_video.isOn) time.StartVideo(thisMode, thisScenario);
        else time.StartTime(thisMode, thisScenario);

        gameObject.SetActive(false);
    }

    public void StartPlayerStopMode()
    {
        Mode thisMode = new ModePlayerstop();
        StartTimer(thisMode);
    }

    public void StartSystemStopMode()
    {
        Mode thisMode = new ModeSystemstop();
        StartTimer(thisMode);
    }

    public void StartFeedbackAfterMode()
    {
        Mode thisMode = new ModeFeedbackAfter();
        StartTimer(thisMode);
    }

    public void StartFeedbackDuringMode()
    {
        Mode thisMode = new ModeFeedbackDuring();
        StartTimer(thisMode);
    }

    public void RetryMode()
    {
        if(currentMode != null) StartTimer(currentMode);
    }
}
