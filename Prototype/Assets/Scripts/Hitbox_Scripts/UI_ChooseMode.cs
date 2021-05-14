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
    private GameObject parent_canvas;
    [SerializeField]
    private Slider slider_video;
    [SerializeField]
    private VideoClip clip;
    [SerializeField]
    private float time_duration;

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeSystem>();
        if (time == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }
    }

    private void StartTimer(Mode thisMode)
    {
        if (!_canUseScript) return;

        if (slider_video.value < 1) time.StartVideo(clip, thisMode);
        else time.StartTime(time_duration, thisMode);

        parent_canvas.SetActive(false);
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
}
