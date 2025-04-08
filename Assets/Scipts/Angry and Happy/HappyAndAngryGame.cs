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

    public TextMeshProUGUI mensajeResultado;
    private bool isCheckingMatch;


    void Start()
    {
        NextCharacter();
        GameManager.instance.InitialGameEnd();
    }

    private void Update()
    {
        CheckSlots();
    }

    public void NextCharacter()
    {
        mensajeResultado.text = "La Respuesta es.....";
        slotGender.gender = GenderType.none;
        slotEmocion.emotion = EmotionType.none;

        currentCharacter = characters[0];
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
        if (slotGender.gender == currentCharacter.gender && slotEmocion.emotion == currentCharacter.emotion)
        {
            Win();
        }
        else
        {
            GameOver();
        }
        isCheckingMatch = false;
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
