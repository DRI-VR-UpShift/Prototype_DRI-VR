using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem time;

    [Header("Start time")]
    [SerializeField]
    private int start_minute;
    [SerializeField]
    private int start_second;

    [Header("End time")]
    [SerializeField]
    private int end_minute;
    [SerializeField]
    private int end_second;

    [Header("The hitbox")]
    [SerializeField]
    private GameObject box;

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeSystem>();
        if(time == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }

        if(start_minute > end_minute ||
            start_minute == end_minute && start_second > end_second)
        {
            Debug.LogError("The hitbox " + name + " has a start time after end time");
            _canUseScript = false;
        }

        box.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canUseScript) return;

        if(Started && !Ended)
        {
            box.SetActive(true);
            return;
        }

        box.SetActive(false);
    }

    private bool Started
    {
        get
        {
            return start_minute < time.Minutes || start_minute == time.Minutes && start_second <= time.Seconds;
        }
    }

    private bool Ended
    {
        get
        {
            return end_minute < time.Minutes || end_minute == time.Minutes && end_second <= time.Seconds;
        }
    }
}
