using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Button btn_StartTime;

    [SerializeField]
    private GameObject timerObject;

    [SerializeField]
    private Text txt_Timer;

    // Variables needed for timer
    private bool _timerIsRunning = false;
    private float _endTime = 120;

    // Variables needed to count time, other scripts can only read them
    private float _currentSeconds = 0;
    public float Seconds
    {
        get { return _currentSeconds; }
    }
    private float _currentMinutes = 0;
    public float Minutes
    {
        get { return _currentMinutes; }
    }
    private float _countSeconds = 0;

    void Start()
    {
        if(_hasUI)
        {
            btn_StartTime.onClick.AddListener(StartTime);
            timerObject.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_timerIsRunning)
        {
            _countSeconds += Time.deltaTime;

            _currentSeconds = Mathf.FloorToInt(_countSeconds % 60);
            _currentMinutes = Mathf.FloorToInt(_countSeconds / 60);

            if (_hasUI) txt_Timer.text = string.Format("{0:00}:{1:00}", _currentMinutes, _currentSeconds);

            // Check if end time is reached
            if(_countSeconds >= _endTime)
            {
                _timerIsRunning = false;
                

                if (_hasUI)
                {
                    timerObject.gameObject.SetActive(false);
                    btn_StartTime.gameObject.SetActive(true);
                }
            }
        }
    }

    private void StartTime()
    {
        StartTime(30);
    }

    public void StartTime(float endTime)
    {
        if (_hasUI)
        {
            timerObject.gameObject.SetActive(true);
            btn_StartTime.gameObject.SetActive(false);
        }

        _countSeconds = 0;
        _endTime = endTime;
        _timerIsRunning = true;
    }
}
