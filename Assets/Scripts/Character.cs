using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]

public class Character : ScriptableObject
{
    [SerializeField] public KeyCode controlUp, controlDown, controlLeft, controlRight;
    [SerializeField] public Sprite[] slideMiniGameSprites, lobMinigameSprites;
    [SerializeField] public AudioClip[] noises;
}
