using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreEndCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tankardText, barrelText, glassText, totalScoreText, highScoreText, newText;
    [SerializeField] RectTransform tankardPos, barrelPos, glassPos, totalScorePos, highScorePos, menuButtonPos;
    [SerializeField] Button menuButton;
    int tankardAmount, barrelAmount, glassAmount;

    float totalScore, totalScoreDisplay, highScore, highScoreDisplay;

    [SerializeField] float tankardValue = 10, barrelValue = 25, glassValue = 50;

    [SerializeField] float countUpTime = 0.1f;

    [SerializeField] AudioClip thudSFX, waheySFX;
    AudioSource tickSFX;

    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] float sRank, aRank, bRank, cRank, fRank, zRank;

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

        if(totalScore >= sRank)
        {
            rankText.text = "S";
        }
        else if(totalScore >= aRank)
        {
            rankText.text = "A";
        }
        else if (totalScore >= bRank)
        {
            rankText.text = "B";
        }
        else if (totalScore >= cRank)
        {
            rankText.text = "C";
        }
        else if (totalScore >= fRank)
        {
            rankText.text = "F";
        }
        else if (totalScore >= zRank)
        {
            rankText.text = "Z";
        }


    }



    IEnumerator MenuStart()
    {

        yield return new WaitForSeconds(2.5f);
        LeanTween.move(tankardText.gameObject, tankardPos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
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
        LeanTween.move(barrelText.gameObject, barrelPos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
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
        LeanTween.move(glassText.gameObject, glassPos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
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
            glassText.text = "TANKARDS CLEANED - " + glassAmount.ToString("00");
            yield return new WaitForSeconds(countUpTime / FindObjectOfType<SessionManager>().glassesCleaned);
        }
        tickSFX.Stop();
        LeanTween.move(totalScoreText.transform.parent.gameObject, totalScorePos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
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

        //rank appears
        yield return new WaitForSeconds(0.4f);
        rankText.enabled = true;
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
        yield return new WaitForSeconds(0.4f);
        
        //high score appears
        LeanTween.move(highScoreText.transform.parent.gameObject, highScorePos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
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

        if(totalScore == highScore)
        {
            yield return new WaitForSeconds(0.15f);
            AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
            yield return new WaitForSeconds(0.15f);

            AudioSource.PlayClipAtPoint(waheySFX, Camera.main.transform.position, 1f);
            newText.enabled = true;
            yield return new WaitForSeconds(0.65f);


        }

        //menu button appears
        yield return new WaitForSeconds(0.4f);
        LeanTween.move(menuButton.gameObject, menuButtonPos, 0.5f);
        yield return new WaitForSeconds(0.4f);
        AudioSource.PlayClipAtPoint(thudSFX, Camera.main.transform.position, 1f);
    }

    public void LoadMenu()
    {
        Destroy(FindObjectOfType<SessionManager>());
        FindObjectOfType<SceneLoader>().LoadScene("Menu");
    }

}
