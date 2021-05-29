using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VectorGraphics;

public class UI_weatherBtn : MonoBehaviour
{
    private WeatherClip thisClip;

    [SerializeField] private SVGImage image;
    private UI_SelectMode ui_select;

    public void SetWeatherClip(UI_SelectMode select,  WeatherClip clip)
    {
        thisClip = clip;
        ui_select = select;
        image.sprite = clip.Icon;
    }

    public void SetThisWeather()
    {
        if (ui_select != null) ui_select.SetWeather(image, thisClip);
    }
}
