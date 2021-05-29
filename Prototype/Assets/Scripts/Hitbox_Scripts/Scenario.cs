using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;
using UnityEngine.Video;

public class Scenario : MonoBehaviour
{
    public string Name
    {
        get { return _name; }
    }
    [SerializeField] private string _name;

    public string Description
    {
        get { return _description; }
    }
    [SerializeField] private string _description;

    public Image Img
    {
        get { return _img; }
    }
    [SerializeField] private Image _img;

    public List<WeatherClip> WeahterClips
    {
        get { return listVideoClips; }
    }
    [SerializeField] List<WeatherClip> listVideoClips = new List<WeatherClip>();

    public VideoClip Clip
    {
        get { return _clip; }
    }
    [SerializeField] private VideoClip _clip;

    private Hitbox[] hitboxList;

    private float endtime = 0;
    public float Time
    {
        get { return endtime; }
    }

    public int Total
    {
        get { return hitboxList.Length; }
    }

    public int Correct
    {
        get
        {
            int correct = 0;
            foreach(Hitbox box in hitboxList)
            {
                if (box.HasBeenHit) correct++;
            }
            return correct;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hitboxList = GetComponentsInChildren<Hitbox>();

        if (_clip != null) endtime = (float)_clip.length;
    }

    public void StartScenario(Mode scenarioMode)
    {
        foreach (Hitbox item in hitboxList)
        {
            item.StartHitbox(scenarioMode);
        }
    }

    public bool SetWeatherVideo(WeatherClip clip)
    {
        _clip = clip.Clip;

        return (_clip != null);
    }
}

[System.Serializable]
public class WeatherClip
{
    [SerializeField] private string _weather;

    public Sprite Icon
    {
        get { return image; }
    }
    [SerializeField] private Sprite image;

    public VideoClip Clip
    {
        get { return _clip; }
    }
    [SerializeField] private VideoClip _clip;

    public bool IsWeather(string weather)
    {
        return (_weather.Equals(weather));
    }
}
