using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TimeSystem : MonoBehaviour
{
    #region Global TimeSystem
    private static TimeSystem _instance;
    public static TimeSystem Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
    }
    #endregion

    [SerializeField]
    private bool _hasUI = true;

    [SerializeField]
    private GameObject timerObject;

    [SerializeField]
    private Text txt_Timer;

    [SerializeField]
    private VideoPlayer _vPlayer;

    [SerializeField]
    private Transform parent_hitboxes;
    private Hitbox[] hitboxList;

    // Variables needed for timer
    private bool _timerIsRunning = false;
    private bool _videoIsRunning = false;
    public bool IsRunning
    {
        get { return (_timerIsRunning || _videoIsRunning); }
    }
    private float _endTime = 120;

    // Variables needed to count time, other scripts can only read them
    private float _countSeconds = 0;
    public float CurrentSeconds
    {
        get { return _countSeconds; }
    }

    private TimeStamp _currentTime = new TimeStamp(0, 0);
    public TimeStamp Now
    {
        get { return _currentTime; }
    }

    void Start()
    {
        if(_hasUI)
        {
            timerObject.gameObject.SetActive(false);
        }

        hitboxList = parent_hitboxes.GetComponentsInChildren<Hitbox>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_timerIsRunning || _videoIsRunning)
        {
            if (_videoIsRunning) _countSeconds = (float)_vPlayer.time;
            if(_timerIsRunning) _countSeconds += Time.deltaTime;

            _currentTime = new TimeStamp(_countSeconds);

            if (_hasUI) txt_Timer.text = _currentTime.ToString();

            // Check if end time is reached
            if(_countSeconds >= _endTime)
            {
                _timerIsRunning = false;
                _videoIsRunning = false;

                if (_hasUI)
                {
                    timerObject.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartTime(float endTime)
    {
        StartTimer(endTime);
        _timerIsRunning = true;

        StartHitboxes();
    }

    public void StartVideo(VideoClip clip)
    {
        _vPlayer.clip = clip;
        _vPlayer.Play();

        StartTimer((float)clip.length);
        _videoIsRunning = true;

        StartHitboxes();
    }

    public void StartHitboxes()
    {
        foreach(Hitbox item in hitboxList)
        {
            item.StartHitbox();
        }
    }

    private void StartTimer(float endTime)
    {
        if (_hasUI)
        {
            timerObject.gameObject.SetActive(true);
        }

        _countSeconds = 0;
        _endTime = endTime;
    }
}

[System.Serializable]
public class TimeStamp
{
    [SerializeField]
    private int _minutes;
    public int Min
    {
        get { return _minutes; }
    }

    [SerializeField]
    private int _seconds;
    public int Sec
    {
        get { return _seconds; }
    }

    public TimeStamp() { }

    public TimeStamp(int min, int sec)
    {
        _minutes = min;
        _seconds = sec;
    }

    public TimeStamp(float seconds)
    {
        _minutes = Mathf.FloorToInt(seconds / 60);
        _seconds = Mathf.FloorToInt(seconds % 60);
    }

    public override string ToString()
    {
        return string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    public bool IsBefore(TimeStamp time)
    {
        return _minutes < time.Min || _minutes == time.Min && _seconds <= time.Sec;
    }

    public int GetTimeInSeconds()
    {
        return (Min * 60) + Sec;
    }

    public static int TimeBetween(TimeStamp start, TimeStamp end)
    {
        int startSeconds = start.GetTimeInSeconds();
        int endSeconds = end.GetTimeInSeconds();

        return endSeconds - startSeconds;
    }
}
