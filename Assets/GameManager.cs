using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public string currentTime;
    public float currentChargeAmount = 100.0f;
    public Slider chargeSlider;
    public GameObject gameOverScreen;

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

        chargeLoop();
    }

    public void resetGame()
    {
        Time.timeScale = 1.0f;
        currentChargeAmount = 100.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void chargeLoop()
    {
        if (currentChargeAmount <= 0)
        {
            //LOSE!
            if(gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
            }
            Time.timeScale = 0.0f;
        }
        else if (Input.GetButton("Fire1"))
        {
            currentChargeAmount -= Time.deltaTime * 50;
        }
        else
        {
            currentChargeAmount -= Time.deltaTime * 10;
        }

        chargeSlider.value = currentChargeAmount;
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
