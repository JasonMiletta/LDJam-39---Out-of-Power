using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public string currentTime;

    private float currentTimeValue;
    private Text timeUIText;

	// Use this for initialization
	void Start () {
        currentTimeValue = 0;
        timeUIText = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        currentTimeValue += Time.deltaTime;

        currentTime = string.Format("{00:00:00}", currentTimeValue);
        if (timeUIText != null)
        {
            timeUIText.text = FormatTime(currentTimeValue);
        }
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }
}
