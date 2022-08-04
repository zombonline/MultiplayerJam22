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

            collision.transform.position = catchHandPosition.position;
            Destroy(collision.gameObject, 1f);
            StartCoroutine(ReturnArm());
        }
    }


    IEnumerator ReturnArm()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<SessionManager>().tankardsCaught++;
        FindObjectOfType<TankardControls>().CatchArmSpriteCatching(false);
        FindObjectOfType<TankardControls>().tankardCaught = false;
    }
}
