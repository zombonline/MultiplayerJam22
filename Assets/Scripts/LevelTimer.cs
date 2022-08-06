using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelTime = 15f;
    float timeRemaining;
    public bool timeUp;
    [SerializeField] AudioClip timeUpSFX;
    AudioSource tickingSFX;
    bool tickingSFXPlaying = false;
    private void Awake()
    {
        timeRemaining = levelTime;
        tickingSFX = GetComponent<AudioSource>();
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(timeRemaining < 6f && !tickingSFXPlaying)
        {
            tickingSFXPlaying = true;
            tickingSFX.Play();
        }
        if (timeRemaining > 0)
        {
            GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>().text = timeRemaining.ToString("00");
        }

        if (timeRemaining <= 0)
        {
            tickingSFX.Stop();
            timeUp = true;
        }
    }
}
