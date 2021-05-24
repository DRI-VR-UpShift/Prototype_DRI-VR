using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_hitbox : MonoBehaviour
{
    private Renderer box_renderer;

    private Material m_standard;
    [SerializeField]
    private Material m_correct;
    [SerializeField]
    private Material m_wrong;

    private bool _hasResult = false;

    private UI_Results ui_result;

    void Start()
    {
        box_renderer = GetComponent<Renderer>();
        m_standard = box_renderer.material;

        ui_result = GameObject.FindObjectOfType<UI_Results>();
    }

    public void IsWrong(string explanation)
    {
        if (_hasResult) return;

        box_renderer.material = m_wrong;
        if (ui_result != null) ui_result.ShowResult(false, explanation);

        _hasResult = true;
    }

    public void IsCorrect(string explanation)
    {
        if (_hasResult) return;

        box_renderer.material = m_correct;
        if (ui_result != null) ui_result.ShowResult(true, explanation);

        _hasResult = true;
    }

    public void ResetResults()
    {
        if (box_renderer == null) return;

        box_renderer.material = m_standard;
        _hasResult = false;
    }
}
