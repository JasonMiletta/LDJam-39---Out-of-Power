using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public float currentChargeAmount = 100.0f;
    public Text timeUIText;
    public Text energyValueUIText;
    public Slider chargeSlider;
    public GameObject HUD;
    public GameObject mainMenuScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;

    private float currentTimeValue;

	// Use this for initialization
	void Start () {
        currentTimeValue = 0;
        Time.timeScale = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimeValue += Time.deltaTime;
        
        if (timeUIText != null)
        {
            timeUIText.text = FormatTime(currentTimeValue);
        }

        pauseCheck();
        chargeLoop();
    }

    #region Button Functions

    public void startGame()
    {
        mainMenuScreen.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1.0f;
       
    }

    public void resetGame()
    {
        resumeGame();
        currentChargeAmount = 100.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startGame();
    }

    public void quitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #endregion

    #region Gameplay Loops

    private void chargeLoop()
    {
        if (currentChargeAmount <= 0)
        {
            //LOSE!
            currentChargeAmount = 0;
            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else if (Input.GetButton("Fire1"))
        {
            updateEnergyValue(-(Time.deltaTime * 50));
        }
        else
        {
            updateEnergyValue(-(Time.deltaTime * 10));
        }

        chargeSlider.value = currentChargeAmount;
        energyValueUIText.text = currentChargeAmount.ToString("0");
    }

    private void pauseCheck()
    {
        if (Input.GetButtonDown("Cancel") && HUD.activeSelf)
        {
            if (!pauseScreen.activeSelf && !gameOverScreen.activeSelf && !mainMenuScreen.activeSelf)
            {
                pauseGame();
            } else
            {
                resumeGame();
            }
        }
    }

    #endregion

    public void updateEnergyValue(float value)
    {
        currentChargeAmount += value;
        if(currentChargeAmount > 100)
        {
            currentChargeAmount = 100;
        } else if(currentChargeAmount < 0)
        {
            currentChargeAmount = 0;
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


    private void pauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    private void resumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
