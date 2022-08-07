using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void PlayHighScoreButton()
    {
        FindObjectOfType<SessionManager>().highScoreMode = true;
        FindObjectOfType<SceneLoader>().LoadRandomGameScene();

            foreach (MusicPersist backroundTrack in FindObjectsOfType<MusicPersist>())
            {
                backroundTrack.FadeIn();
            }
        
    }
}
