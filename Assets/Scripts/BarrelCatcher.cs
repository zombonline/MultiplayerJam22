using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCatcher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Barrel"))
        {
            Destroy(collision.gameObject);
            FindObjectOfType<SessionManager>().barrelsCaught++;
        }
    }
}