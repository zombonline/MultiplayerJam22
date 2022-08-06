using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    Animator animator;
    [SerializeField] List<int> gameScenes;
    [SerializeField] string washInstructions, lobInstructions, slideInstructions, endInstructions;

    [SerializeField] TextMeshProUGUI instructionsText;
    List<int> currentGameScenes;
    private void Awake()
    {
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

        animator = GetComponent<Animator>();

        currentGameScenes = new List<int>();
        foreach (int scene in gameScenes)
        {
            currentGameScenes.Add(scene);
        }
    }

    public void LoadScene(string scene)
    {
        var sceneBuildIndex = SceneManager.GetSceneByName(scene).buildIndex;
        StartCoroutine(TransitionScene(sceneBuildIndex));
    }

    public void LoadRandomGameScene()
    {
        if (currentGameScenes.Count == 0)
        {
            MusicPersist musicTrack = GameObject.FindGameObjectWithTag("Music").GetComponent<MusicPersist>();
            musicTrack.SpeedUp(0.25f);

            FindObjectOfType<SessionManager>().highScoreModeLoops++;
            if (FindObjectOfType<SessionManager>().highScoreModeLoops >= FindObjectOfType<SessionManager>().maxHighScoreModeLoops)
            {
                foreach(MusicPersist backroundTrack in FindObjectsOfType<MusicPersist>())
                {
                    backroundTrack.FadeOut();
                }

                StartCoroutine(TransitionEndScene());
            
            }
            else
            {
                Debug.Log("Repopulating game scene list");
                foreach (int scene in gameScenes)
                {
                    currentGameScenes.Add(scene);
                }


                var randomValue = Random.Range(0, currentGameScenes.Count);

                var sceneBuildIndex = currentGameScenes[randomValue];
                Debug.Log(sceneBuildIndex);
                currentGameScenes.Remove(currentGameScenes[randomValue]);
                StartCoroutine(TransitionScene(sceneBuildIndex));
            }
        }

        else
        {
            if(currentGameScenes.Count == gameScenes.Count && FindObjectOfType<SessionManager>().highScoreModeLoops == 0)
            {
                foreach(MusicPersist backroundTrack in FindObjectsOfType<MusicPersist>())
                {
                    backroundTrack.FadeIn();
                }
            }

            var randomValue = Random.Range(0, currentGameScenes.Count);

            var sceneBuildIndex = currentGameScenes[randomValue];
            Debug.Log(sceneBuildIndex);
            currentGameScenes.Remove(currentGameScenes[randomValue]);
            StartCoroutine(TransitionScene(sceneBuildIndex));
        }

    }

    public void LoadNextScene()
    {
        var sceneBuildIndex = (SceneManager.GetActiveScene().buildIndex) + 1;
        StartCoroutine(TransitionScene(sceneBuildIndex));
    }

    public void ReloadScene()
    {
        var sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        TransitionScene(sceneBuildIndex);
    }

    IEnumerator TransitionScene(int scene)
    {
        if(scene == 1)
        {
            instructionsText.text = slideInstructions;
        }
        if(scene == 2)
        {
            instructionsText.text = lobInstructions;
        }
        if(scene == 3)
        {
            instructionsText.text = washInstructions;
        }
        animator.CrossFade("SceneOut", 0, 0);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.75f);
        animator.CrossFade("SceneIn", 0, 0);
    }
    IEnumerator TransitionEndScene()
    {
        instructionsText.text = endInstructions;
        animator.CrossFade("SceneOut", 0, 0);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("HighScoreEndScene");
        yield return new WaitForSeconds(0.75f);
        animator.CrossFade("SceneIn", 0, 0);
    }

}
