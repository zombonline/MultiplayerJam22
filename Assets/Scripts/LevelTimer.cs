using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] float levelTime = 15f;
    float timeRemaining;
    public bool timeUp;

    private void Awake()
    {
        timeRemaining = levelTime;
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining > 0)
        {
            GameObject.Find("Timer Text").GetComponent<TextMeshProUGUI>().text = timeRemaining.ToString("00");
        }

        if (timeRemaining < 0)
        {
            timeUp = true;
        }
    }
}
