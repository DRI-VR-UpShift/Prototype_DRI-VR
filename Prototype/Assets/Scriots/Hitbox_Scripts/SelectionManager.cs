using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask hitboxMask;

    private Hitbox highlighted_box;
    private Hitbox selected_box;

    void Update()
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
                box.HighlightBox();
                highlighted_box = box;
            }
        }
        else if (highlighted_box != null)
        {
            highlighted_box.ResetBox();
            highlighted_box = null;
        }

        // Select item
        if (Input.GetMouseButtonDown(0))
        {
            if(highlighted_box != null)
            {
                selected_box = highlighted_box;
                selected_box.SelectBox();
                highlighted_box = null;
            }
            else if(selected_box != null)
            {
                selected_box = null;
            }
        }
    }
}
