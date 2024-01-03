using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class healthManager : MonoBehaviour
{
    public float health, maxHealth, timer;

    [Header("UI")]
    public Slider sliderR;
    public Slider sliderL;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        sliderR.maxValue = maxHealth;
        sliderL.maxValue = maxHealth;
        SetHealthBar();
    }
    private void Update()
    {
        health -= Time.deltaTime;
        SetHealthBar();
        timer +=Time.deltaTime;
        SetTimer();

        if(health <= 0)
        {
            //Game Over
        }
    }

    public void SetHealthBar()
    {
        sliderR.value = sliderL.value = health;
    }

    private void SetTimer()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // Format the timer string as "00:00"
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void IncreaseHealth(float amount)
    {
        health = Mathf.Clamp(amount, 0, maxHealth);
    }

}

