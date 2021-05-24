using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Scenario : MonoBehaviour
{
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
}
