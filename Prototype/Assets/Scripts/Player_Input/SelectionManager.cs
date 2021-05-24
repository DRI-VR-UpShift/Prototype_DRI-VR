using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private bool _canUseScript = true;
    private TimeSystem _timeSystem;

    [SerializeField]
    private LayerMask hitboxMask;

    private Hitbox highlighted_box;

    void Start()
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
        if (!_canUseScript) return;

        // Detect if we can select item
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, hitboxMask))
        {
            Transform selection = hit.transform;
            Hitbox box = selection.parent.GetComponent<Hitbox>();
            if (box != null)
            {
                highlighted_box = box;
            }
        }
        else if (highlighted_box != null)
        {
            highlighted_box = null;
        }

        // Select item
        if (Input.GetMouseButtonDown(0) && highlighted_box != null)
        {
            _timeSystem.Hit(highlighted_box);
        }
    }
}
