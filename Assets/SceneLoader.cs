using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    Animator animator;
    [SerializeField] List<int> gameScenes;
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

    public void LoadScene(int scene)
    {
        var sceneBuildIndex = SceneManager.GetSceneByBuildIndex(scene).buildIndex;
        StartCoroutine(TransitionScene(sceneBuildIndex));
    }

    public void LoadRandomGameScene()
    {
        if (currentGameScenes.Count == 0)
        {
            Debug.Log("Repopulating game scene list");
            foreach (int scene in gameScenes)
            {
                FindObjectOfType<SessionManager>().highScoreModeLoops++;
                if (FindObjectOfType<SessionManager>().highScoreModeLoops >= FindObjectOfType<SessionManager>().maxHighScoreModeLoops)
                {
                    LoadScene(0);
                }
                currentGameScenes.Add(scene);
            }


            var randomValue = Random.Range(0, currentGameScenes.Count);

            var sceneBuildIndex = currentGameScenes[randomValue];
            Debug.Log(sceneBuildIndex);
            currentGameScenes.Remove(currentGameScenes[randomValue]);
            StartCoroutine(TransitionScene(sceneBuildIndex));
        }

        else
        {


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
        animator.CrossFade("SceneOut", 0, 0);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(scene);
        yield return new WaitForSeconds(0.75f);
        animator.CrossFade("SceneIn", 0, 0);
    }


}
