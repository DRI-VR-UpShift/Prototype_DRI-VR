using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private Renderer box_renderer;

    private Material m_standard;
    [SerializeField] private Material m_correct;
    [SerializeField] private Material m_wrong;

    private bool _hasResult = false;

    private UI_Feedback feedback;

    private ModeFeedback modeFeedback;

    void Start()
    {
        if(box_renderer != null)
            m_standard = box_renderer.material;

        feedback = GameObject.FindObjectOfType<UI_Feedback>();
    }

    public void IsWrong(string explanation)
    {
        Scores.AddWrong();
        if (_hasResult || modeFeedback is ModeFeedbackAfter) return;

        if (box_renderer != null)
            box_renderer.material = m_wrong;

        if (feedback != null) feedback.ShowResult(false, explanation);

        _hasResult = true;
    }

    public void IsCorrect(string explanation)
    {
        Scores.AddCorrect();
        if (_hasResult || modeFeedback is ModeFeedbackAfter) return;

        if (box_renderer != null)
            box_renderer.material = m_correct;

        if (feedback != null) feedback.ShowResult(true, explanation);

        _hasResult = true;
    }

    public void ResetResults(ModeFeedback mFeedback)
    {
        if (box_renderer == null) return;

        modeFeedback = mFeedback;

        if (box_renderer != null)
            box_renderer.material = m_standard;

        _hasResult = false;
    }
}
