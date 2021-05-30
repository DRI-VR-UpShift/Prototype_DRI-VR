using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn_Hitbox : MonoBehaviour
{
    protected bool _canUseScript;
    protected TimeSystem _timeSystem;
    private Transform _player;

    public Hitbox Box
    {
        set { thisBox = value; }
    }
    private Hitbox thisBox;

    public ModeStop CurrentMode
    {
        set { currentMode = value; }
    }
    private ModeStop currentMode;

    // Start is called before the first frame update
    void Start()
    {
        _timeSystem = FindObjectOfType<TimeSystem>();
        if (_timeSystem == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }

        _player = GameObject.FindGameObjectWithTag("player").transform;
        if(_player == null)
        {
            Debug.LogError("Missing player tag in the scene");
            _canUseScript = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player);
    }

    public void HitThisBox()
    {

    }
}
