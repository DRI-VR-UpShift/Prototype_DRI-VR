using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInput_Manager : MonoBehaviour
{
    protected bool _canUseScript;
    protected TimeSystem _timeSystem;

    [SerializeField]
    protected LayerMask hitboxMask;

    public ModeStop CurrentMode
    {
        set { currentMode = value; }
    }
    private ModeStop currentMode;

    // The hand
    private InputDevice targetDevice;
    private float HandRight = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);

    // The line painted by the hand
    [SerializeField] protected LineRenderer lineRenderer;
    [SerializeField] protected float lineWidth = 0.1f;
    [SerializeField] protected float lineMaxLength = 1f;

    private bool btnPressed = false;
    private Vector3 endPosition = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        _timeSystem = FindObjectOfType<TimeSystem>();
        if (_timeSystem == null)
        {
            Debug.LogError("The scene is missing a TimeSystem");
            _canUseScript = false;
        }

        // Start painting the line into the scene
        Vector3[] startLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        lineRenderer.SetPositions(startLinePositions);
        lineRenderer.enabled = true;
    }

    void Update()
    {
        if (_canUseScript || currentMode == null) return;

        if (_timeSystem.IsRunning)
        {
            OVRInput.Update();

            // update value HandRight every frae with new value from trigger
            HandRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

            if (HandRight > 0.7) btnPressed = true;
            else btnPressed = false;

            lineRenderer.enabled = true;

            endPosition = transform.position + (lineMaxLength * transform.forward);

            if(btnPressed)
            {
                if (currentMode is ModePlayerstop && !_timeSystem.IsTakingBreak)
                {
                    _timeSystem.StartBreak();
                }
                else if ((currentMode is ModePlayerstop || currentMode is ModeSystemstop) && _timeSystem.IsTakingBreak)
                {
                    SelectHitbox();
                }
                else if (currentMode is ModeNonStop)
                {
                    SelectHitbox();
                }
            }

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void SelectHitbox()
    {
        Hitbox box = null;

        // Detect if we can select item
        //Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, hitboxMask))
        {
            endPosition = hit.point;

            Transform selection = hit.transform;
            box = selection.parent.GetComponent<Hitbox>();
        }

        _timeSystem.Hit(box);
    }
}
