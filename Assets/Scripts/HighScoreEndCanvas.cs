using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreEndCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tankardText, barrelText;

    private void Awake()
    {
        tankardText.text = "Pints slid - " + FindObjectOfType<SessionManager>().tankardsCaught.ToString("00");
        barrelText.text = "Barrels passed - " + FindObjectOfType<SessionManager>().barrelsCaught.ToString("00");
    }


}
