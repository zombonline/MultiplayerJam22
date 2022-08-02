using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TankardControls : MonoBehaviour
{
    [SerializeField] float timeInLevel = 30f;
    [SerializeField] TextMeshProUGUI timerText;
    float levelTime;
    bool levelOver = false;


    [SerializeField] float tankardSpeed = 2f;

    [SerializeField] GameObject tankardPrefab;
    GameObject currentTankard;
    public bool tankardMoving;
    public bool tankardCaught = false;


    [SerializeField] Transform tankardSpawnPoint, tankardDestination;

    [SerializeField] Character[] characters;
    [Header("Throw Arm")]
    Character throwCharacter;
    [SerializeField] SpriteRenderer throwArmSprite;

    [Header("Catch Arm")]
    Character catchCharacter;
    [SerializeField] SpriteRenderer catchArmSprite;
    [SerializeField] GameObject catchZone;
    [SerializeField] float catchLength;
    [SerializeField] float catchArmCooldownLength;
    float catchArmCooldown;


    private void Start()
    {
        int randomNumber = Random.Range(0, 100);
        if(randomNumber > 50)
        {
            throwCharacter = characters[0];
            throwArmSprite.sprite = throwCharacter.slideMiniGameSprites[2];

            catchCharacter = characters[1];
            catchArmSprite.sprite = catchCharacter.slideMiniGameSprites[0];


        }
        else
        {
            throwCharacter = characters[1];
            throwArmSprite.sprite = throwCharacter.slideMiniGameSprites[2];
            catchCharacter = characters[0];
            catchArmSprite.sprite = catchCharacter.slideMiniGameSprites[0];

        }

        currentTankard = Instantiate(tankardPrefab, tankardSpawnPoint.position, Quaternion.identity);
        levelTime = timeInLevel;



    }

    private void Update()
    {
        levelTime -= Time.deltaTime;
        timerText.text = levelTime.ToString("00");
        if(levelTime <= 0 && !levelOver)
        {
            StartCoroutine(LevelOver());
        }
        if(levelOver)
        {
            return;
        }
        catchArmCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(throwCharacter.controlUp) && tankardMoving == false && currentTankard != null && !tankardCaught)
        {
            throwArmSprite.sprite = throwCharacter.slideMiniGameSprites[3];
            AudioSource.PlayClipAtPoint(throwCharacter.noises[Random.Range(0, throwCharacter.noises.Length)], Camera.main.transform.position);
            tankardMoving = true;
        }
        if (Input.GetKeyDown(catchCharacter.controlUp) && catchArmCooldown <= 0)
        {
            StartCoroutine(CatchArm());
        }

        ScaleTankard();



        if (!tankardMoving && currentTankard == null)
        {
            throwArmSprite.sprite = throwCharacter.slideMiniGameSprites[2];
            currentTankard = Instantiate(tankardPrefab, tankardSpawnPoint.position, Quaternion.identity);
        }
        if(tankardMoving)
        {
            currentTankard.transform.position = Vector2.MoveTowards(currentTankard.transform.position, tankardDestination.position, 1f * Time.deltaTime * tankardSpeed);
        }
        if(tankardMoving && currentTankard.transform.position == tankardDestination.position)
        {
            tankardMoving = false;
            Destroy(currentTankard);
        }
    }

    public void UpdateTankardLayer()
    {
        if (currentTankard.transform.position.y < catchZone.transform.position.y && !tankardCaught && catchZone.GetComponent<BoxCollider2D>().enabled == false)
        {
            currentTankard.GetComponentInChildren<SpriteRenderer>().sortingOrder = 5;
        }
    }

    void ScaleTankard()
    {
        if (tankardMoving)
        {
            var currentDifference = Vector2.Distance((Vector2)currentTankard.transform.position, (Vector2)tankardDestination.position);
            var totalDifference = Vector2.Distance((Vector2)tankardSpawnPoint.position, (Vector2)tankardDestination.position);
            var percentage = currentDifference / totalDifference;
            Debug.Log(currentDifference);
            Debug.Log(totalDifference);
            Debug.Log(currentDifference / totalDifference);
            var scaleModifier = Mathf.Abs(percentage - 1f);

            currentTankard.GetComponentInChildren<AudioSource>().transform.localScale = new Vector3(
                1f + (scaleModifier*5f),
                1f + (scaleModifier*5f),
                1f);
        }
    }

    IEnumerator LevelOver()
    {
        levelOver = true;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<SceneLoader>().LoadRandomGameScene();
    }

    IEnumerator CatchArm()
    {
        AudioSource.PlayClipAtPoint(catchCharacter.noises[Random.Range(0, catchCharacter.noises.Length)], Camera.main.transform.position);
        catchArmCooldown = catchArmCooldownLength;
        CatchArmSpriteCatching(true);
        catchZone.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(catchLength);
        catchZone.GetComponent<BoxCollider2D>().enabled = false;
        if(!tankardCaught)
        {
            CatchArmSpriteCatching(false);
        }
        
    }
    public void CatchArmSpriteCatching(bool state)
    {
        if(state == true)
        {
            catchArmSprite.sprite = catchCharacter.slideMiniGameSprites[1];
        }
        else
        {
            catchArmSprite.sprite = catchCharacter.slideMiniGameSprites[0];
        }
    }

}
    
