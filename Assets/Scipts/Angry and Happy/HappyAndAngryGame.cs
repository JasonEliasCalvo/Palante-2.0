using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HappyAndAngryGame : MonoBehaviour
{
    public List<CharacterData> characters;
    public Image characterImage;
    public DropSlot slotGender;
    public DropSlot slotEmocion;

    private CharacterData currentCharacter;
    private int characterIndex;

    public TextMeshProUGUI mensajeResultado;
    private bool isCheckingMatch;


    void Start()
    {
        StartCharacter();
        GameManager.instance.InitialGameEnd();
    }

    private void Update()
    {
        CheckSlots();
    }

    public void StartCharacter()
    {
        mensajeResultado.text = "La Respuesta es.....";
        slotGender.gender = GenderType.none;
        slotEmocion.emotion = EmotionType.none;
        characterIndex = 0;
        currentCharacter = characters[characterIndex];
        characterImage.sprite = currentCharacter.characterImage;
    }

    private void CheckSlots()
    {
        if (slotEmocion.emotion != EmotionType.none && slotGender.gender != GenderType.none && !isCheckingMatch) 
        {
            isCheckingMatch = true;
            Debug.Log("Chekeo");
            CheckAnswer();
        }
    }



    public void CheckAnswer()
    {
        if (slotGender.gender == currentCharacter.gender && slotEmocion.emotion == currentCharacter.emotion && characterIndex < characters.Count -1)
        {
            NextCharacter();
        }
        else if (slotGender.gender == currentCharacter.gender && slotEmocion.emotion == currentCharacter.emotion)
        {
            Win();
        }
        else
        {
            GameOver();
        }
    }

    public void NextCharacter()
    {
        characterIndex++;
        slotGender.gender = GenderType.none;
        slotEmocion.emotion = EmotionType.none;
        currentCharacter = characters[characterIndex];
        characterImage.sprite = currentCharacter.characterImage;
        isCheckingMatch = false;
        Debug.Log("Sigue");
    }

    private void GameOver()
    {
        GameManager.instance.InitialGameStart();
        Debug.Log("¡Perdiste!");
    }

    private void Win()
    {
        GameManager.instance.InitialGameStart();
        Debug.Log("¡Ganaste!");
    }

}
