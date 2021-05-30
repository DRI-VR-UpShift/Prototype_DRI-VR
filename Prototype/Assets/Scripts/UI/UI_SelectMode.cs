using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;

public class UI_SelectMode : MonoBehaviour
{
    private Scenario thisScenario;

    private ModeStop mStopping = null;
    private ModeFeedback mFeedback = null;
    private bool hasWeather = false;

    [SerializeField] private UI_Control ui_control;

    [Header("Stop mode")]
    [SerializeField] private Transform parent_stopmodetoggles;
    [SerializeField] private SVGImage bg_btn_stopSystem;
    [SerializeField] private SVGImage bg_btn_stopPlayer;
    [SerializeField] private SVGImage bg_btn_stopNone;

    [Header("Feedback mode")]
    [SerializeField] private Transform parent_feedbacktoggles;
    [SerializeField] private SVGImage bg_btn_feedbackduring;
    [SerializeField] private SVGImage bg_btn_feedbackafter;

    [Header("Weather mode")]
    [SerializeField] private GameObject prefab_btn_weatherVideo;
    [SerializeField] private Transform parent_weathertoggles;

    [Header("Start btn")]
    [SerializeField] private Button btn_startScenario;

    [Header("Btn colors")]
    [SerializeField] private Color normal_color;
    [SerializeField] private Color selected_color;

    public void SetScenario(Scenario scenario)
    {
        thisScenario = scenario;

        mStopping = null;
        mFeedback = null;

        // Clear weather buttons
        foreach(Transform child in parent_weathertoggles)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Create weather buttons
        foreach(WeatherClip clip in thisScenario.WeahterClips)
        {
            GameObject btn = Instantiate(prefab_btn_weatherVideo, parent_weathertoggles);
            UI_weatherBtn toggle = btn.GetComponent<UI_weatherBtn>();
            toggle.SetWeatherClip(this, clip);
        }

        btn_startScenario.onClick.AddListener(SetMode);
        btn_startScenario.gameObject.SetActive(false);
    }

    private void ResetButtons(Transform parent)
    {
        SVGImage[] backgrounds = parent.GetComponentsInChildren<SVGImage>();

        foreach (SVGImage item in backgrounds)
        {
            item.color = normal_color;
        }
    }

    // Functions to set the stopping mode
    public void SetModeStopSystem()
    {
        ResetButtons(parent_stopmodetoggles);

        mStopping = new ModeSystemstop();
        bg_btn_stopSystem.color = selected_color;

        UpdateStartButton();
    }

    public void SetModeStopPlayer()
    {
        ResetButtons(parent_stopmodetoggles);

        mStopping = new ModePlayerstop();
        bg_btn_stopPlayer.color = selected_color;

        UpdateStartButton();
    }

    public void SetModeNonStop()
    {
        ResetButtons(parent_stopmodetoggles);

        mStopping = new ModeNonStop();
        bg_btn_stopNone.color = selected_color;

        UpdateStartButton();
    }

    // Functions to set the feedback mode
    public void SetModeFeedbackDuring()
    {
        ResetButtons(parent_feedbacktoggles);

        mFeedback = new ModeFeedbackDuring();
        bg_btn_feedbackduring.color = selected_color;

        UpdateStartButton();
    }
    public void SetModeFeedbackAfter()
    {
        ResetButtons(parent_feedbacktoggles);

        mFeedback = new ModeFeedbackAfter();
        bg_btn_feedbackafter.color = selected_color;

        UpdateStartButton();
    }

    // Function to set the weather mode
    public void SetWeather(SVGImage bg_btn, WeatherClip clip)
    {
        ResetButtons(parent_weathertoggles);
        
        hasWeather = thisScenario.SetWeatherVideo(clip);

        if(hasWeather)
        {
            bg_btn.color = selected_color;
        }

        UpdateStartButton();
    }

    // Check if scenario can start
    public void UpdateStartButton()
    {
        if(mStopping != null && mFeedback != null && hasWeather)
        {
            btn_startScenario.gameObject.SetActive(true);
        }
        else
        {
            btn_startScenario.gameObject.SetActive(false);
        }
    }

    public void SetMode()
    {
        ui_control.SelectMode(mStopping, mFeedback);
    }
}
