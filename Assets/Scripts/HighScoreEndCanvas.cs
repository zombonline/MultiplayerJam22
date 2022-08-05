using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreEndCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tankardText, barrelText, glassText, totalScoreText, highScoreText;
    [SerializeField] RectTransform tankardPos, barrelPos, glassPos, totalScorePos, highScorePos, menuButtonPos;
    [SerializeField] Button menuButton;
    int tankardAmount, barrelAmount, glassAmount;

    float totalScore, totalScoreDisplay, highScore, highScoreDisplay;

    [SerializeField] float tankardValue = 10, barrelValue = 25, glassValue = 50;

    [SerializeField] float countUpTime = 0.1f;

    [SerializeField] AudioClip thudSfx;
    AudioSource tickSFX;

    private void Awake()
    {
        tickSFX = GetComponent<AudioSource>();
        StartCoroutine(MenuStart());

        totalScore =
            (FindObjectOfType<SessionManager>().tankardsCaught * tankardValue) +
            (FindObjectOfType<SessionManager>().barrelsCaught * barrelValue) +
            (FindObjectOfType<SessionManager>().glassesCleaned * glassValue);
        highScore = PlayerPrefs.GetFloat("HIGH SCORE");
        if(totalScore > highScore)
        {
            PlayerPrefs.SetFloat("HIGH SCORE", totalScore);
        }
    }


    IEnumerator MenuStart()
    {
        LeanTween.move(tankardText.gameObject, tankardPos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        tankardText.text = "Pints slid - " + tankardAmount.ToString("00");
        while (tankardAmount < FindObjectOfType<SessionManager>().tankardsCaught)
        {
            tankardAmount++;
            if (!tickSFX.isPlaying)
            {
                tickSFX.Play();
            }
            if (Input.anyKeyDown)
            {
                tankardAmount = FindObjectOfType<SessionManager>().tankardsCaught;
            }
            tankardText.text = "PINTS SLID - " + tankardAmount.ToString("00");
            yield return new WaitForSeconds(countUpTime / FindObjectOfType<SessionManager>().tankardsCaught);
        }
        tickSFX.Stop();
        LeanTween.move(barrelText.gameObject, barrelPos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        while (barrelAmount < FindObjectOfType<SessionManager>().barrelsCaught)
        {
            if (!tickSFX.isPlaying)
            {
                tickSFX.Play();
            }
            barrelAmount++;
            if (Input.anyKeyDown)
            {
                barrelAmount = FindObjectOfType<SessionManager>().barrelsCaught;
            }
            barrelText.text = "BARRELS PASSED - " + barrelAmount.ToString("00");
            yield return new WaitForSeconds(countUpTime / FindObjectOfType<SessionManager>().barrelsCaught);
        }
        tickSFX.Stop();
        LeanTween.move(glassText.gameObject, glassPos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        while (glassAmount < FindObjectOfType<SessionManager>().glassesCleaned)
        {
            if (!tickSFX.isPlaying)
            {
                tickSFX.Play();
            }
            glassAmount++;
            if(Input.anyKeyDown)
            {
                glassAmount = FindObjectOfType<SessionManager>().glassesCleaned;
            }
            glassText.text = "GLASSES CLEANED - " + glassAmount.ToString("00");
            yield return new WaitForSeconds(countUpTime / FindObjectOfType<SessionManager>().glassesCleaned);
        }
        tickSFX.Stop();
        LeanTween.move(totalScoreText.transform.parent.gameObject, totalScorePos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        while (totalScoreDisplay < totalScore)
        {
            if (!tickSFX.isPlaying)
            {
                tickSFX.Play();
            }
            totalScoreDisplay += 10;
            if (totalScoreDisplay > totalScore)
            {
                totalScoreDisplay = totalScore;
            }
            if (Input.anyKeyDown)
            {
                totalScoreDisplay = totalScore;
            }
            totalScoreText.text = totalScoreDisplay.ToString("0000000");
            yield return new WaitForSeconds(countUpTime / totalScore);
        }
        tickSFX.Stop();
        LeanTween.move(highScoreText.transform.parent.gameObject, highScorePos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        while (highScoreDisplay < highScore)
        {
            if (!tickSFX.isPlaying)
            {
                tickSFX.Play();
            }
            highScoreDisplay += 10;
            if(highScoreDisplay > highScore)
            {
                highScoreDisplay = highScore;
            }
            if (Input.anyKeyDown)
            {
                highScoreDisplay = highScore;
            }
            highScoreText.text = highScoreDisplay.ToString("0000000");
            yield return new WaitForSeconds(countUpTime / highScore);
        }
        tickSFX.Stop();
        LeanTween.move(menuButton.gameObject, menuButtonPos, 1f);
        yield return new WaitForSeconds(0.9f);
        AudioSource.PlayClipAtPoint(thudSfx, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
    }

}
