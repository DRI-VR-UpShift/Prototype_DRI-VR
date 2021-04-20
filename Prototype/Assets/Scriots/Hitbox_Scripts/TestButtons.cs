using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TestButtons : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem time;

    [SerializeField]
    private float timeduration;

    [SerializeField]
    private VideoClip clip;

    [SerializeField]
    private GameObject _buttonList;

    private bool _isRunningTimer = false;

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

    // Update is called once per frame
    void Update()
    {
        if (!_canUseScript) return;

        if(_isRunningTimer && !time.IsRunning)
        {
            _buttonList.SetActive(true);
            _isRunningTimer = false;
        }
    }

    public void StartTime()
    {
        if (!_canUseScript) return;

        time.StartTime(timeduration);
        _buttonList.SetActive(false);
        _isRunningTimer = true;
    }

    public void StartVideo()
    {
        if (!_canUseScript) return;

        time.StartVideo(clip);
        _buttonList.SetActive(false);
        _isRunningTimer = true;
    }
}
