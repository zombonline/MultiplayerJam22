using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlassState
{
    Dirty,
    Wet,
    Clean
}

public class WashMinigame : MonoBehaviour
{
    Character dryCharacter, washCharacter;
    [SerializeField] Character[] characters;
    SpriteRenderer currentGlass;
    [SerializeField] Sprite glassDirty, glassWet, glassClean;
    [SerializeField] GlassState currentState;
    [SerializeField] GameObject glassPrefab;
    [SerializeField] Transform dirtyPoint, wetPoint, cleanPoint;
    [SerializeField] Transform dryCharacterLeftArm, dryCharacterRightArm, washCharacterLeftArm, washCharacterRightArm;
    bool dryLeftPressed = false, washLeftPressed = false;
    bool levelOver = false;

    [SerializeField] int buttonPresses = 0;
    [SerializeField] int buttonPressesNeeded = 50;

    bool glassBroken = false;
    [SerializeField] ParticleSystem glassBrokenParticles;

    Vector3 dryLeftOriginalPos, dryRightOriginalPos, washLeftOriginalPos, washRightOriginalPos;

    private void Awake()
    {
        AssignCharacters();
        GetNewGlass();
        dryLeftOriginalPos = dryCharacterLeftArm.position;
        dryRightOriginalPos = dryCharacterRightArm.position;
        washLeftOriginalPos = washCharacterLeftArm.position;
        washRightOriginalPos = washCharacterRightArm.position;

        buttonPressesNeeded += (FindObjectOfType<SessionManager>().highScoreModeLoops * 10);

        washCharacterLeftArm.GetComponent<SpriteRenderer>().sprite = washCharacter.washMinigameSprites[0];
        washCharacterRightArm.GetComponent<SpriteRenderer>().sprite = washCharacter.washMinigameSprites[1];
        dryCharacterLeftArm.GetComponent<SpriteRenderer>().sprite = dryCharacter.washMinigameSprites[2];
        dryCharacterRightArm.GetComponent<SpriteRenderer>().sprite = dryCharacter.washMinigameSprites[3];
    }

    void GetNewGlass()
    {
        currentGlass = Instantiate(glassPrefab, dirtyPoint.position, Quaternion.identity).GetComponent<SpriteRenderer>();
        currentState = GlassState.Dirty;
        
    }

    private void Update()
    {
        if (FindObjectOfType<LevelTimer>().timeUp && !levelOver)
        {
            StartCoroutine(LevelOver());
        }
        if (!levelOver)
        {
            MoveArms();
        }
    }

    void AssignCharacters()
    {
        int randomValue = Random.Range(0, 100);
        if(randomValue > 50f)
        {
            dryCharacter = characters[0];
            washCharacter = characters[1];
        }
        else
        {
            dryCharacter = characters[1];
            washCharacter = characters[0];
        }
    }

    void MoveArms()
    {
        if (!glassBroken)
        {
            if (currentState == GlassState.Dirty)
            {
                if (Input.GetKeyDown(washCharacter.controlLeft))
                {
                    if (washLeftPressed && buttonPresses > 0)
                    {
                        glassBroken = true;
                        StartCoroutine(BreakGlass());
                        washCharacterLeftArm.transform.position = new Vector3(washLeftOriginalPos.x, washLeftOriginalPos.y, washCharacterLeftArm.position.z);
                        washCharacterRightArm.transform.position = new Vector3(washRightOriginalPos.x, washRightOriginalPos.y, washCharacterRightArm.position.z);
                    }
                    else
                    {
                        washCharacterLeftArm.transform.position = new Vector3(washLeftOriginalPos.x, washLeftOriginalPos.y + 1, washCharacterLeftArm.position.z);
                        washCharacterRightArm.transform.position = new Vector3(washRightOriginalPos.x, washRightOriginalPos.y - 1, washCharacterRightArm.position.z);

                        washLeftPressed = true;
                        buttonPresses++;
                    }
                }
                if (Input.GetKeyDown(washCharacter.controlRight))
                {
                    if (!washLeftPressed && buttonPresses > 0)
                    {
                        glassBroken = true;
                        StartCoroutine(BreakGlass());
                        washCharacterLeftArm.transform.position = new Vector3(washLeftOriginalPos.x, washLeftOriginalPos.y, washCharacterLeftArm.position.z);
                        washCharacterRightArm.transform.position = new Vector3(washRightOriginalPos.x, washRightOriginalPos.y, washCharacterRightArm.position.z);
                    }
                    else
                    {
                        washCharacterLeftArm.transform.position = new Vector3(washLeftOriginalPos.x, washLeftOriginalPos.y - 1, washCharacterLeftArm.position.z);
                        washCharacterRightArm.transform.position = new Vector3(washRightOriginalPos.x, washRightOriginalPos.y + 1, washCharacterRightArm.position.z);

                        washLeftPressed = false;
                        buttonPresses++;
                    }

                }
                if (buttonPresses >= buttonPressesNeeded)
                {
                    currentState = GlassState.Wet;
                    LeanTween.move(currentGlass.gameObject, wetPoint.position, 0.2f);
                    currentGlass.sprite = glassWet;
                    buttonPresses = 0;
                }
            }
            if (currentState == GlassState.Wet)
            {
                if (Input.GetKeyDown(dryCharacter.controlLeft))
                {
                    if (dryLeftPressed && buttonPresses > 0)
                    {
                        glassBroken = true;
                        StartCoroutine(BreakGlass());
                        dryCharacterLeftArm.transform.position = new Vector3(dryLeftOriginalPos.x, dryLeftOriginalPos.y, dryCharacterLeftArm.position.z);
                        dryCharacterRightArm.transform.position = new Vector3(dryRightOriginalPos.x, dryRightOriginalPos.y, dryCharacterRightArm.position.z);
                    }
                    else
                    {
                        dryCharacterLeftArm.transform.position = new Vector3(dryLeftOriginalPos.x, dryLeftOriginalPos.y + 1, dryCharacterLeftArm.position.z);
                        dryCharacterRightArm.transform.position = new Vector3(dryRightOriginalPos.x, dryRightOriginalPos.y - 1, dryCharacterRightArm.position.z);

                        dryLeftPressed = true;
                        buttonPresses++;
                    }
                }
                if (Input.GetKeyDown(dryCharacter.controlRight))
                {
                    if (!dryLeftPressed && buttonPresses > 0)
                    {
                        glassBroken = true;
                        StartCoroutine(BreakGlass());
                        dryCharacterLeftArm.transform.position = new Vector3(dryLeftOriginalPos.x, dryLeftOriginalPos.y, dryCharacterLeftArm.position.z);
                        dryCharacterRightArm.transform.position = new Vector3(dryRightOriginalPos.x, dryRightOriginalPos.y, dryCharacterRightArm.position.z);
                    }
                    else
                    {
                        dryCharacterLeftArm.transform.position = new Vector3(dryLeftOriginalPos.x, dryLeftOriginalPos.y - 1, dryCharacterLeftArm.position.z);
                        dryCharacterRightArm.transform.position = new Vector3(dryRightOriginalPos.x, dryRightOriginalPos.y + 1, dryCharacterRightArm.position.z);

                        dryLeftPressed = false;
                        buttonPresses++;
                    }
                }
                if (buttonPresses >= buttonPressesNeeded)
                {
                    currentState = GlassState.Clean;
                    LeanTween.move(currentGlass.gameObject, cleanPoint.position, 0.8f);
                    if (FindObjectOfType<SessionManager>())
                    {
                        FindObjectOfType<SessionManager>().glassesCleaned++;
                    }

                    currentGlass.sprite = glassClean;
                    GetNewGlass();
                    buttonPresses = 0;
                }
            }
        }
    }

    IEnumerator LevelOver()
    {
        levelOver = true;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<SceneLoader>().LoadRandomGameScene();
    }

    IEnumerator BreakGlass()
    {
        currentGlass.enabled = false;
        if (currentState == GlassState.Dirty)
        {
            glassBrokenParticles.transform.position = dirtyPoint.position;
            glassBrokenParticles.Play();
        }
        else if(currentState == GlassState.Dirty)
        {
            glassBrokenParticles.transform.position = wetPoint.position;
            glassBrokenParticles.Play();
        }
        yield return new WaitForSeconds(1f);
        buttonPresses = 0;
        Destroy(currentGlass.gameObject);
        glassBroken = false;
        GetNewGlass();
    }
}
