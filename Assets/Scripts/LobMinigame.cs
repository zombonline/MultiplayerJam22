using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobMinigame : MonoBehaviour
{
    bool levelOver = false;

    [SerializeField] SpriteRenderer throwingSprite, catchingSprite;
    [SerializeField] Rigidbody2D[] throwObjects;
    [SerializeField] float throwForce;
    Character throwCharacter, catchCharacter;
    [SerializeField] Character[] characters;
    [SerializeField] Transform throwAim;
    [SerializeField] float throwAimMaxRotate = 60f;
    [SerializeField] float throwAimSpeed;

    [SerializeField] float catchCharacterSpeed;
    [SerializeField] float catchCharacterXBoundary;

    [SerializeField] Transform barrelIndicator;
    [SerializeField] float barrelIndicatorYStartPos;

    [SerializeField] Animator catchAnimator;
    bool moving = false;
    private void Start()
    {
        int randomValue = Random.Range(0, 100);
        if(randomValue > 50f)
        {
            throwCharacter = characters[0];
            catchCharacter = characters[1];
        }
        else
        {
            throwCharacter = characters[1];
            catchCharacter = characters[0];
        }
    }

    private void Update()
    {
        float rZ = Mathf.SmoothStep(-throwAimMaxRotate, throwAimMaxRotate, Mathf.PingPong(Time.time * throwAimSpeed, 1));
        throwAim.rotation = Quaternion.Euler(0, 0, rZ);


        if (!FindObjectOfType<LevelTimer>().timeUp)
        {
            if (Input.GetKeyDown(throwCharacter.controlUp) && !GameObject.FindGameObjectWithTag("Barrel"))
            {
                Rigidbody2D newThrownObject = Instantiate(throwObjects[Random.Range(0, throwObjects.Length)], throwAim.position, Quaternion.identity);
                newThrownObject.transform.rotation = throwAim.transform.rotation;

                var multiplier = FindObjectOfType<SessionManager>().highScoreModeLoops * 0.75f;

                newThrownObject.AddForce(newThrownObject.transform.up * (throwForce + multiplier), ForceMode2D.Impulse);
                Destroy(newThrownObject.gameObject, 7f);
            }

            if (Input.GetKey(catchCharacter.controlLeft) && catchingSprite.transform.position.x > -catchCharacterXBoundary)
            {
                if(!moving)
                {
                    moving = true;
                    catchAnimator.CrossFade("lobcatchmove", 0f, 0);
                }
                catchingSprite.transform.parent.position -= new Vector3(catchCharacterSpeed * Time.deltaTime, 0f, 0f);
            }
            else if (Input.GetKey(catchCharacter.controlRight) && catchingSprite.transform.position.x < catchCharacterXBoundary)
            {
                if (!moving)
                {
                    moving = true;
                    catchAnimator.CrossFade("lobcatchmove", 0f, 0);
                }
                catchingSprite.transform.parent.position += new Vector3(catchCharacterSpeed * Time.deltaTime, 0f, 0f);
            }
            else
            {
                if (moving)
                {
                    catchAnimator.CrossFade("lobcatchidle", 0, 0);
                    moving = false;
                }
            }
        }
        else if(!levelOver)
        {
            StartCoroutine(LevelOver());
        }

        BarrelIndicator();
    }

    void BarrelIndicator()
    {
        if(GameObject.FindGameObjectsWithTag("Barrel").Length > 0)
        {
            if (GameObject.FindGameObjectWithTag("Barrel").transform.position.y > barrelIndicatorYStartPos)
            {
                barrelIndicator.gameObject.SetActive(true);
                barrelIndicator.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Barrel").transform.position.x, barrelIndicator.transform.position.y, barrelIndicator.transform.position.z);

                var scale = barrelIndicatorYStartPos / GameObject.FindGameObjectWithTag("Barrel").transform.position.y;

                barrelIndicator.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                barrelIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            barrelIndicator.gameObject.SetActive(false);
        }
    }

    IEnumerator LevelOver()
    {
        levelOver = true;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<SceneLoader>().LoadRandomGameScene();
    }

}
