using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchZone : MonoBehaviour
{
    [SerializeField] Transform catchHandPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Tankard"))
        {
            FindObjectOfType<TankardControls>().tankardMoving = false;
            FindObjectOfType<TankardControls>().tankardCaught = true;
            FindObjectOfType<TankardControls>().fingerSprite.enabled = true;


            collision.transform.position = catchHandPosition.position;
            Destroy(collision.gameObject, 0.25f);
            StartCoroutine(ReturnArm());
        }
    }


    IEnumerator ReturnArm()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<SessionManager>().tankardsCaught++;
        FindObjectOfType<TankardControls>().fingerSprite.enabled = false;
        FindObjectOfType<TankardControls>().CatchArmSpriteCatching(false);
        FindObjectOfType<TankardControls>().tankardCaught = false;
    }
}
