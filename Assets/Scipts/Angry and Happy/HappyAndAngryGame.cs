using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyAndAngryGame : MonoBehaviour
{
    public List<CharacterData> characters;
    public Image characterImage;
    public DropSlot slotGender;
    public DropSlot slotEmocion;

    private CharacterData currentCharacter;

    public Text mensajeResultado;
    private bool isCheckingMatch;


    void Start()
    {
        mensajeResultado.text = "La Respuesta es.....";
        NextCharacter();
    }

    private void OnEnable()
    {
       /// DropSlot.OnDropIcon += CheckAnswer;
    }

    private void OnDisable()
    {
        //DropSlot.OnDropIcon -= CheckAnswer;
    }

    public void NextCharacter()
    {
        mensajeResultado.text = "";
        slotGender.SelectedValue = "";
        slotEmocion.SelectedValue = "";

        currentCharacter = characters[0];
        characterImage.sprite = currentCharacter.characterImage;
    }

    private void CheckSlots()
    {
        if (isCheckingMatch) return;

        if (slotEmocion != null && slotGender)
        {
            isCheckingMatch = true;
            CheckAnswer();
        }
    }

    public void CheckAnswer()
    {
        if (slotGender.SelectedValue != currentCharacter.gender || slotEmocion.SelectedValue != currentCharacter.emotion)
        {

        }
        if (slotGender.SelectedValue == currentCharacter.gender || slotEmocion.SelectedValue == currentCharacter.emotion)
        {

            CheckGameOver();
        }

        isCheckingMatch = false;
    }

    private void CheckGameOver()
    {
        
    }

    private void Win()
    {
        GameManager.instance.InitialGameStart();
        Debug.Log("🎉 ¡Ganaste!");
    }

}
