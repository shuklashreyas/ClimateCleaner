using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Ensure this namespace is included

public class LevelTxt : MonoBehaviour
{
    // Text components to display wave number and timer
    public Text waveText;  // Using Text from UnityEngine.UI
    public Text timerText;

    // Wave and timer variables
    private int currentWave = 1;
    private float countdownTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the text components
        UpdateWaveText();
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the countdown timer
        countdownTimer -= Time.deltaTime;
        if (countdownTimer <= 0f)
        {
            // Reset the timer and increment the wave number
            countdownTimer = 10f;
            currentWave++;
            UpdateWaveText();
        }
        UpdateTimerText();
    }

    // Method to update the wave text
    void UpdateWaveText()
    {
        waveText.text = "Wave:0" + currentWave;
    }

    // Method to update the timer text
    void UpdateTimerText()
    {
        timerText.text = "Time:0" + Mathf.Ceil(countdownTimer).ToString();
    }
}
