using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    private Material m_standard;
    [SerializeField] private Renderer box_renderer;
    [SerializeField] private Material m_correct;
    [SerializeField] private Material m_wrong;

    [Header("Feedback UI")] 
    [SerializeField] private GameObject canvas_explanation;

    [SerializeField] private GameObject ui_correct;
    [SerializeField] private Text txt_correct;

    [SerializeField] private GameObject ui_wrong;
    [SerializeField] private Text txt_wrong;

    private bool _hasResult = false;

    private ModeFeedback modeFeedback;

    private float time = 0;
    private float duration = 5;
    private bool isShowingMsg = false;

    void Start()
    {
        if(box_renderer != null)
            m_standard = box_renderer.material;

        canvas_explanation.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShowingMsg)
        {
            time += Time.deltaTime;

            if (time >= duration)
            {
                canvas_explanation.SetActive(false);
                isShowingMsg = false;
            }
        }
    }

    public void IsWrong(string explanation)
    {
        if (_hasResult) return;

        Scores.AddWrong();

        if (modeFeedback is ModeFeedbackAfter) return;

        if (box_renderer != null)
            box_renderer.material = m_wrong;

        ui_correct.SetActive(false);

        ui_wrong.SetActive(true);
        txt_wrong.text = explanation;

        ShowUI();

        _hasResult = true;
    }

    public void IsCorrect(string explanation)
    {
        if (_hasResult) return;

        Scores.AddCorrect();

        if (modeFeedback is ModeFeedbackAfter) return;

        if (box_renderer != null)
            box_renderer.material = m_correct;

        ui_wrong.SetActive(false);

        ui_correct.SetActive(true);
        txt_correct.text = explanation;

        ShowUI();

        _hasResult = true;
    }

    private void ShowUI()
    {
        canvas_explanation.SetActive(true);

        time = 0;
        isShowingMsg = true;
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
