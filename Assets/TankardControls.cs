using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankardControls : MonoBehaviour
{
    [SerializeField] float tankardSpeed = 2f;

    [SerializeField] GameObject tankardPrefab;
    GameObject currentTankard;
    public bool tankardMoving;
    public bool tankardCaught = false;


    [SerializeField] Transform tankardSpawnPoint, tankardDestination;
    [Header("Throw Arm")]
    [SerializeField] SpriteRenderer throwArmSprite;
    [SerializeField] public Sprite throwArmWaiting, throwArmThrowing;


    [Header("Catch Arm")]
    [SerializeField] SpriteRenderer catchArmSprite;
    [SerializeField] GameObject catchZone;
    [SerializeField] float catchLength;
    [SerializeField] Sprite catchArmWaiting, catchArmCatching;
    [SerializeField] float catchArmCooldownLength;
    float catchArmCooldown;


    private void Start()
    {
        currentTankard = Instantiate(tankardPrefab, tankardSpawnPoint.position, Quaternion.identity);
    }

    private void Update()
    {
        catchArmCooldown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.W) && tankardMoving == false && currentTankard != null && !tankardCaught)
        {
            throwArmSprite.sprite = throwArmThrowing;
            tankardMoving = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && catchArmCooldown <= 0)
        {
            StartCoroutine(CatchArm());

        }




        if (!tankardMoving && currentTankard == null)
        {
            throwArmSprite.sprite = throwArmWaiting;
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
    IEnumerator CatchArm()
    {
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
            catchArmSprite.sprite = catchArmCatching;
        }
        else
        {
            catchArmSprite.sprite = catchArmWaiting;
        }
    }

}
