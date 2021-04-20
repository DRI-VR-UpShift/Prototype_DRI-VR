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
    private Button btn_StartTime;

    [SerializeField]
    private Text txt_Timer;

    // Variables needed for timer
    private bool timerIsRunning = false;
    private float _timeSpeed = 1.0f;
    private float _currentTimeRemaining = 1.0f;

    // Variables needed to count time, other scripts can only read them
    private int currentSeconds = 0;
    public int Seconds
    {
        get { return currentSeconds; }
    }
    private int currentMinutes = 0;
    public int Minutes
    {
        get { return currentMinutes; }
    }

    void Start()
    {
        btn_StartTime.onClick.AddListener(StartTime);
        if (txt_Timer != null)  txt_Timer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if (_currentTimeRemaining <= 0)
            {
                _currentTimeRemaining = _timeSpeed;

                currentSeconds++;

                // If counted an hour, start new hour
                if (currentSeconds >= 60)
                {
                    currentMinutes++;
                    currentSeconds = 0;
                }

                // Print to screen
                if (txt_Timer != null)
                {
                    txt_Timer.text = currentMinutes.ToString("00") + ":" + currentSeconds.ToString("00");
                }
            }
            else
            {
                _currentTimeRemaining -= Time.deltaTime;
            }
        }
    }

    private void StartTime()
    {
        if(txt_Timer != null) txt_Timer.gameObject.SetActive(true);
        timerIsRunning = true;
    }
}
