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

    [SerializeField] int buttonPresses = 0;
    [SerializeField] int buttonPressesNeeded = 50;

    bool glassBroken = false;

    private void Awake()
    {
        AssignCharacters();
        GetNewGlass();
    }

    void GetNewGlass()
    {
        currentGlass = Instantiate(glassPrefab, dirtyPoint.position, Quaternion.identity).GetComponent<SpriteRenderer>();
        currentState = GlassState.Dirty;
        
    }

    private void Update()
    {
        MoveArms();
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
        if (currentState == GlassState.Dirty)
        {
            if (Input.GetKeyDown(washCharacter.controlLeft))
            {
                if (washLeftPressed)
                {
                    glassBroken = true;
                    StartCoroutine(BreakGlass());
                }
                else
                {
                washCharacterLeftArm.transform.position = new Vector3(washCharacterLeftArm.position.x, washCharacterLeftArm.position.y + 1, washCharacterLeftArm.position.z);
                washCharacterRightArm.transform.position = new Vector3(washCharacterRightArm.position.x, washCharacterRightArm.position.y - 1, washCharacterRightArm.position.z);

                washLeftPressed = true;
                buttonPresses++;
                }
            }
            if (Input.GetKeyDown(washCharacter.controlRight) && washLeftPressed)
            {
                if(!washLeftPressed)
                {
                    glassBroken = true;
                    StartCoroutine(BreakGlass());
                }
                {
                    washCharacterLeftArm.transform.position = new Vector3(washCharacterLeftArm.position.x, washCharacterLeftArm.position.y - 1, washCharacterLeftArm.position.z);
                    washCharacterRightArm.transform.position = new Vector3(washCharacterRightArm.position.x, washCharacterRightArm.position.y + 1, washCharacterRightArm.position.z);

                    washLeftPressed = false;
                    buttonPresses++;
                }

            }
            if(buttonPresses >= buttonPressesNeeded)
            {
                currentState = GlassState.Wet;
                LeanTween.move(currentGlass.gameObject, wetPoint.position, 0.2f);
                currentGlass.sprite = glassWet;
                buttonPresses = 0;
            }
        }
        if(currentState == GlassState.Wet)
        {
            if (Input.GetKeyDown(dryCharacter.controlLeft))
            {
                if (dryLeftPressed)
                {
                    glassBroken = true;
                    StartCoroutine(BreakGlass());
                }
                else
                {
                    dryCharacterLeftArm.transform.position = new Vector3(dryCharacterLeftArm.position.x, dryCharacterLeftArm.position.y + 1, dryCharacterLeftArm.position.z);
                    dryCharacterRightArm.transform.position = new Vector3(dryCharacterRightArm.position.x, dryCharacterRightArm.position.y - 1, dryCharacterRightArm.position.z);

                    dryLeftPressed = true;
                    buttonPresses++;
                }
            }
            if (Input.GetKeyDown(dryCharacter.controlRight) && dryLeftPressed)
            {
                if (!dryLeftPressed)
                {
                    glassBroken = true;
                    StartCoroutine(BreakGlass());
                }
                else
                {
                    dryCharacterLeftArm.transform.position = new Vector3(dryCharacterLeftArm.position.x, dryCharacterLeftArm.position.y - 1, dryCharacterLeftArm.position.z);
                    dryCharacterRightArm.transform.position = new Vector3(dryCharacterRightArm.position.x, dryCharacterRightArm.position.y + 1, dryCharacterRightArm.position.z);

                    dryLeftPressed = false;
                    buttonPresses++;
                }
            }
            if (buttonPresses >= buttonPressesNeeded)
            {
                currentState = GlassState.Clean;
                LeanTween.move(currentGlass.gameObject, cleanPoint.position, 0.2f);
                if (FindObjectOfType<SessionManager>())
                {
                    FindObjectOfType<SessionManager>().glassesCleaned++;
                }
                currentGlass.sprite = glassClean;
                buttonPresses = 0;
            }
        }
    }
    IEnumerator BreakGlass()
    {
        yield return new WaitForSeconds(1f);
        Destroy(currentGlass);
        glassBroken = false;
        GetNewGlass();
    }
}
