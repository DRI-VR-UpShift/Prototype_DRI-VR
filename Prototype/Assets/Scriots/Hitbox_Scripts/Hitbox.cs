using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem _timeSystem;
    public bool IsPlaying = false;

    [Header("The hitbox")]
    [SerializeField]
    private GameObject _box;

    [Header("Hitbox positions")]
    [SerializeField]
    private List<HitboxPosition> _positionList = new List<HitboxPosition>();

    private int _index = 0;
    private HitboxPosition _currentPositon = null;
    private HitboxPosition CurrentPosition
    {
        set
        {
            _currentPositon = value;
            _index++;
        }
    }
    private HitboxPosition _nextPosition = null;
    private HitboxPosition NextPosition
    {
        set
        {
            _nextPosition = value;
            timeToReach = HitboxPosition.SecondsBetween(_currentPositon, _nextPosition);
        }
    }

    private float timestep = 0;
    private float timeToReach = 0;

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
            _box.SetActive(false);
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
                _box.SetActive(false);
                _nextPosition = null;
            }
            else NextPosition = _positionList[_index];
        }
        else if (_currentPositon.TimePassed(_timeSystem.Now))
        {
            _box.SetActive(true);

            timestep += Time.deltaTime / timeToReach;
            transform.position = Vector3.Lerp(_currentPositon.Pos, _nextPosition.Pos, timestep);
        }
    }

    public void StartHitbox()
    {
        _currentPositon = null;
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

    public static int SecondsBetween(HitboxPosition startPos, HitboxPosition endPos)
    {
        return TimeStamp.TimeBetween(startPos.TimePosition, endPos.TimePosition);
    }
}