using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandle : MonoBehaviour
{
    PlayerInput playerInput;

    GameObject currentHeldObject;
    List<GameObject> objectsInReach;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Interactable") || collision.CompareTag("Player") && gameObject != transform.parent.gameObject)
        {
            objectsInReach.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") || collision.CompareTag("Player") && gameObject != transform.parent.gameObject)
        {
            objectsInReach.Remove(collision.gameObject);
        }
    }



    public void Handle()
    {
        if(currentHeldObject == null && objectsInReach.Count != 0)
        {
            currentHeldObject = objectsInReach[0].gameObject;
        }
        else if(currentHeldObject != null)
        {
            //stop player movement
            //enable aiming cursor
            //
        }
    }
    
}
