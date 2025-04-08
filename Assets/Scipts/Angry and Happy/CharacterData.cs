using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Minigame/Character")]
public class CharacterData : ScriptableObject
{
    public Sprite characterImage;
    public GenderType gender;
    public EmotionType emotion; 
}
