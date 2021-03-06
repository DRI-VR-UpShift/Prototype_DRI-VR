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

    [SerializeField] private bool _hasUI = true;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private Text txt_Timer;
    [SerializeField] private VideoPlayer _vPlayer;

    [SerializeField] private Input_Manager input;
    [SerializeField] private VRInput_Manager vr_hand;
    [SerializeField] private LineRenderer hand_renderer;
    [SerializeField] private LaserPointer pointer;
    [SerializeField] private LineRenderer pointer_renderer;

    private Scenario currentScenario;
    [SerializeField] private UI_Feedback ui_feedback;

    // Variables needed for timer
    public bool IsRunning
    {
        get { return (_timerIsRunning || _videoIsRunning); }
    }
    private bool _timerIsRunning = false;
    private bool _videoIsRunning = false;
    
    public bool IsTakingBreak
    {
        get { return _takebreak; }
    }
    private bool _takebreak = false;

    private List<Hitbox> hitboxlist_takingbreak = new List<Hitbox>();

    // To stop at this time
    private float _endTime = 120;

    // Variables needed to count time, other scripts can only read them
    public float CurrentSeconds
    {
        get { return _countSeconds; }
    }
    private float _countSeconds = 0;

    public TimeStamp Now
    {
        get { return _currentTime; }
    }
    private TimeStamp _currentTime = new TimeStamp(0, 0);

    public float TimeStep
    {
        get { return currentStep; }
    }
    private float currentStep = 0;

    [SerializeField] private UI_Control ui_control;
    [SerializeField] private UI_InGame ui_ingame;

    void Start()
    {
        if(_hasUI)
        {
            timerObject.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((_timerIsRunning || _videoIsRunning) && !_takebreak)
        {
            float lastcoundseconds = _countSeconds;

            if (_videoIsRunning) _countSeconds = (float)_vPlayer.time;
            if(_timerIsRunning) _countSeconds += Time.deltaTime;

            currentStep = _countSeconds - lastcoundseconds;

            _currentTime = new TimeStamp(_countSeconds);

            if (_hasUI) txt_Timer.text = _currentTime.ToString();

            // Check if end time is reached
            if(_countSeconds >= _endTime || _videoIsRunning && _countSeconds >= (_endTime - 1))
            {
                Debug.Log("Reached end video");

                _timerIsRunning = false;
                _videoIsRunning = false;

                if (_hasUI)
                {
                    timerObject.gameObject.SetActive(false);
                }

                if (ui_control != null)
                {
                    ui_control.gameObject.SetActive(true);
                    ui_control.ShowResults(currentScenario);
                }

                if (ui_ingame != null)
                {
                    ui_ingame.gameObject.SetActive(false);
                }

                if(vr_hand != null)
                {
                    vr_hand.enabled = false;
                    hand_renderer.enabled = false;
                    pointer.enabled = true;
                    pointer_renderer.enabled = true;
                }
            }
        }
    }

    public void StartTime(Scenario thisScenario, ModeStop modeStop, ModeFeedback modeFeedback)
    {
        StartTimer(60);
        _timerIsRunning = true;

        ResetScenario(thisScenario, modeStop, modeFeedback);
    }

    public void StartVideo(Scenario thisScenario, ModeStop modeStop, ModeFeedback modeFeedback)
    {
        _vPlayer.clip = thisScenario.Clip;
        _vPlayer.Play();

        StartTimer(thisScenario.Time);
        _videoIsRunning = true;

        ResetScenario(thisScenario, modeStop, modeFeedback);
    }

    public void ResetScenario(Scenario scenario, ModeStop modeStop, ModeFeedback modeFeedback)
    {
        if (input != null) input.CurrentMode = modeStop;
        else if (vr_hand != null)
        {
            vr_hand.enabled = true;
            hand_renderer.enabled = true;
            vr_hand.CurrentMode = modeStop;
            pointer.enabled = false;
            pointer_renderer.enabled = false;
        }

        currentScenario = scenario;
        currentScenario.StartScenario(modeStop, modeFeedback);

        ui_feedback.CurrentMode = modeFeedback;
        if (modeFeedback is ModeFeedbackDuring) ui_ingame.gameObject.SetActive(true);

        _countSeconds = 0;
    }

    public void StartBreak()
    {
        _takebreak = true;

        hitboxlist_takingbreak = new List<Hitbox>();

        Hitbox[] list = GameObject.FindObjectsOfType<Hitbox>();
        foreach(Hitbox item in list)
        {
            if (!item.TakenBreak)
            {
                Debug.Log(item.name + " is taking break");
                hitboxlist_takingbreak.Add(item);
            }
        }

        if (_videoIsRunning) _vPlayer.Pause();
    }

    public void Hit(Hitbox hitbox)
    {
        if(IsTakingBreak)
        {
            foreach(Hitbox box in hitboxlist_takingbreak)
            {
                Debug.Log("In the break " + box.name);
                if (box == hitbox) hitbox.HitThisBox();
                else box.MissedBox();
            }

            _takebreak = false;
            if (_videoIsRunning) _vPlayer.Play();
        }
        else
        {
            if (hitbox != null) hitbox.HitThisBox();
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
    private float _seconds;
    public float Sec
    {
        get { return _seconds; }
    }

    public TimeStamp GetLater()
    {
        return new TimeStamp(_minutes, _seconds++);
    }

    public TimeStamp() { }

    public TimeStamp(int min, float sec)
    {
        _minutes = min;
        _seconds = sec;
    }

    public TimeStamp(float seconds)
    {
        _minutes = Mathf.FloorToInt(seconds / 60);
        _seconds = seconds % 60;
    }

    public override string ToString()
    {
        float hundredths = (_seconds * 100) % 100;
        return string.Format("{0:00}:{1:00}:{2:00}", _minutes, _seconds, hundredths);
    }

    public bool IsBefore(TimeStamp time)
    {
        return _minutes < time.Min || _minutes == time.Min && _seconds <= time.Sec;
    }

    public float GetTimeInSeconds()
    {
        return (Min * 60) + Sec;
    }

    public static float TimeBetween(TimeStamp start, TimeStamp end)
    {
        float startSeconds = start.GetTimeInSeconds();
        float endSeconds = end.GetTimeInSeconds();

        return endSeconds - startSeconds;
    }
}
