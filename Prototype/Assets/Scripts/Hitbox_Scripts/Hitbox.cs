using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem _timeSystem;
    [HideInInspector] public bool IsPlaying = false;

    [Header("The hitbox")]
    [SerializeField] private Result _box;

    public bool ShouldHit
    {
        get { return hit_this_box; }
    }
    [SerializeField] private bool hit_this_box = true;

    [SerializeField] private string hitbox_feedback = "There is something to be said about hitting this box";

    [Header("Stop the video at time")]
    [SerializeField]
    private HitboxPosition stopVideoAt = new HitboxPosition();

    [Header("Hitbox positions")]
    [SerializeField]
    private List<HitboxPosition> _positionList = new List<HitboxPosition>();

    private int _index = 0;

    private HitboxPosition CurrentPosition
    {
        set
        {
            _currentPositon = value;
            _index++;
        }
    }
    private HitboxPosition _currentPositon = null;
    
    private HitboxPosition NextPosition
    {
        set
        {
            _nextPosition = value;

            // Reset time stuff
            timeToReach = HitboxPosition.SecondsBetween(_currentPositon, _nextPosition);
            timestep = 0;
        }
    }
    private HitboxPosition _nextPosition = null;

    private float timestep = 0;
    private float timeToReach = 0;

    public bool HasBeenHit
    {
        get { return hasbeenselected; }
    }
    private bool hasbeenselected = false;

    private Mode currentMode = null;

    public bool TakenBreak
    {
        get { return hastakenbreak; }
    }
    private bool hastakenbreak = false;

    public bool HasResult
    {
        get { return hasResult; }
    }
    private bool hasResult = false;

    // Start is called before the first frame update
    void Start()
    {
        _timeSystem = FindObjectOfType<TimeSystem>();
        if(_timeSystem == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }

        if(_box == null)
        {
            Debug.LogError(name + " has no box attached to it");
            _canUseScript = false;
        }
        else
        {
            _box.gameObject.SetActive(false);
        }

        if(_positionList.Count <= 0)
        {
            Debug.LogError(name + " has no positions attached to it");
            _canUseScript = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canUseScript || !_timeSystem.IsRunning) return;

        if(currentMode is ModeSystemstop)
        {
            CheckSystemStop();
        }
        else
        {
            SetHitboxAtPosition();
        }
    }

    private void CheckSystemStop()
    {
        if (stopVideoAt != null && stopVideoAt.TimePassed(_timeSystem.Now))
        {
            if (!hastakenbreak)
            {
                _timeSystem.StartBreak();
                transform.position = stopVideoAt.Pos;
                _box.gameObject.SetActive(true);
                hastakenbreak = true;
            }
            else if (!_timeSystem.IsTakingBreak)
            {
                MissedBox();
                _box.gameObject.SetActive(false);
            }
        }
    }

    private void SetHitboxAtPosition()
    {
        if (_timeSystem.IsTakingBreak) return;

        if (_currentPositon == null)
        {
            _index = 0;

            CurrentPosition = _positionList[_index];
            NextPosition = _positionList[_index];

            transform.position = _currentPositon.Pos;
        }
        else if (_nextPosition == null) return;
        else if (_nextPosition.TimePassed(_timeSystem.Now))
        {
            CurrentPosition = _nextPosition;

            if (_index >= _positionList.Count)
            {
                if (!hasbeenselected) MissedBox();
                _box.gameObject.SetActive(false);
                _nextPosition = null;
            }
            else NextPosition = _positionList[_index];
        }
        else if (_currentPositon.TimePassed(_timeSystem.Now))
        {
            _box.gameObject.SetActive(true);

            timestep += _timeSystem.TimeStep / timeToReach;
            transform.position = Vector3.Lerp(_currentPositon.Pos, _nextPosition.Pos, timestep);
        }
    }

    public void StartHitbox(Mode thisMode)
    {
        currentMode = thisMode;
        _currentPositon = null;
        hasbeenselected = false;
        hastakenbreak = false;
        hasResult = false;
        _box.ResetResults();
    }

    public void HitThisBox()
    {
        if (hit_this_box) _box.IsCorrect(hitbox_feedback);
        else _box.IsWrong(hitbox_feedback);

        hasbeenselected = true;
        hasResult = true;
    }

    public void MissedBox()
    {
        Debug.Log("Missed " + hitbox_feedback);
        if (hit_this_box) _box.IsWrong(hitbox_feedback);
        else _box.IsCorrect(hitbox_feedback);

        hasbeenselected = false;
        hasResult = true;
    }
}

[System.Serializable]
public class HitboxPosition
{
    [SerializeField]
    private TimeStamp _time;
    public TimeStamp TimePosition
    {
        get { return _time; }
    }

    [SerializeField]
    private Vector3 _position;
    public Vector3 Pos
    {
        get { return _position; }
    }

    public bool TimePassed(TimeStamp time)
    {
        return _time.IsBefore(time);
    }

    public static float SecondsBetween(HitboxPosition startPos, HitboxPosition endPos)
    {
        return TimeStamp.TimeBetween(startPos.TimePosition, endPos.TimePosition);
    }
}