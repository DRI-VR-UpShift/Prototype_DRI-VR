using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    private bool _canUseScript;
    private TimeSystem _timeSystem;

    [SerializeField]
    private LayerMask hitboxMask;
    
    public Mode CurrentMode
    {
        set { currentMode = value; }
    }
    private Mode currentMode;

    private void Start()
    {
        _timeSystem = FindObjectOfType<TimeSystem>();
        if (_timeSystem == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }
    }

    void Update()
    {
        if (_canUseScript || currentMode == null) return;

        if (_timeSystem.IsRunning && Input.GetMouseButtonDown(0))
        {
            if (currentMode is ModePlayerstop && !_timeSystem.IsTakingBreak)
            {
                Debug.Log("Taking break");
                _timeSystem.StartBreak();
            }
            else if((currentMode is ModePlayerstop || currentMode is ModeSystemstop) && _timeSystem.IsTakingBreak)
            {
                Debug.Log("Select box");
                _timeSystem.StopBreak();
                SelectHitbox();
            }
            else if (currentMode is ModeFeedbackAfter || currentMode is ModeFeedbackDuring)
            {
                SelectHitbox();
            }
        }
    }

    public void SelectHitbox()
    {
        // Detect if we can select item
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hitboxMask))
        {
            Transform selection = hit.transform;
            Hitbox box = selection.parent.GetComponent<Hitbox>();
            if (box != null)
            {
                box.SelectBox();
            }
        }
    }
}
