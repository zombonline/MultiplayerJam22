using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPersist : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] float fadeSpeed;
    float targetVolume = 1f;
    [SerializeField] float speed;
    float targetSpeed = 1f;

    bool coRoutineRunning = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();


        StartCoroutine(AdjustToTargetVolume());
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<SceneLoader>().Length;
        if (gameSessionCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        
        audioSource.pitch = speed;
        audioSource.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / speed);
    }

    public void FadeIn()
    {
        audioSource.volume = 0f;
        targetVolume = 1f;
        audioSource.Play();
        StartCoroutine(AdjustToTargetVolume());

    }
    public void FadeOut()
    {
        audioSource.volume = 1f;
        targetVolume = 0f;
        StartCoroutine(AdjustToTargetVolume());
    }

    public void SpeedUp(float newSpeed)
    {
        Debug.Log("Speeding up!");
        targetSpeed = targetSpeed += newSpeed;
        
        StartCoroutine(AdjustToTargetSpeed());
    }

    IEnumerator AdjustToTargetSpeed()
    {
        while (speed != targetSpeed)
        {
            if (speed < targetSpeed)
            {
                speed += 0.01f;
            }
            else if (speed > targetSpeed)
            {
                speed -= 0.01f;
            }
            yield return new WaitForSeconds(fadeSpeed / 100f);
        }
        
    }

    IEnumerator AdjustToTargetVolume()
    {
        while(audioSource.volume != targetVolume)
        {
            if (audioSource.volume < targetVolume)
            {
                audioSource.volume += 0.01f;
            }
            else if(audioSource.volume > targetVolume)
            {
                audioSource.volume -= 0.01f;
            }
            yield return new WaitForSeconds(fadeSpeed / 100f);
        }
        if(targetVolume == 0)
        {
            audioSource.Stop();
        }
    }


}
