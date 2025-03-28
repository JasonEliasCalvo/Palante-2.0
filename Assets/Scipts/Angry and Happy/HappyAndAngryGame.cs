using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappyAndAngryGame : MonoBehaviour
{
    public List<CharacterData> characters;
    public Image characterImage;
    public DropSlot slotGenero;
    public DropSlot slotEmocion;

    private CharacterData currentCharacter;

    public Text mensajeResultado;
    void Start()
    {
        mensajeResultado.text = "La Respuesta es.....";
    }

    public void NextCharacter()
    {
        mensajeResultado.text = "";
        slotGenero.SelectedValue = "";
        slotEmocion.SelectedValue = "";

        currentCharacter = characters[0];
        characterImage.sprite = currentCharacter.characterImage;
    }

    public void CheckAnswer()
    {
        if (slotGenero.SelectedValue == "" || slotEmocion.SelectedValue == "")
        {
            return;
        }
    }
}
