using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public bool highScoreMode = false;
    [SerializeField] public int maxHighScoreModeLoops;
    public int highScoreModeLoops;


    private void Awake()
    {
        //Do not destroy on load
        int gameSessionCount = FindObjectsOfType<SessionManager>().Length;
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

}
