using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        time = FindObjectOfType<TimeSystem>();
        if(time == null)
        {
            Debug.Log("The scene is missing a TimeSystem");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
