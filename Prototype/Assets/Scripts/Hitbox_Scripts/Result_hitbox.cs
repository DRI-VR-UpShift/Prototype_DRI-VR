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

    void Start()
    {
        box_renderer = GetComponent<Renderer>();
        m_standard = box_renderer.material;
    }

    public void IsHit()
    {
        if (_hasResult) return;

        box_renderer.material = m_correct;
        _hasResult = true;
    }

    public void IsNotHit()
    {
        if (_hasResult) return;

        box_renderer.material = m_wrong;
        _hasResult = true;
    }

    public void Reset()
    {
        box_renderer.material = m_standard;
        _hasResult = false;
    }
}
