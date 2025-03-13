using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    GenerateCards,
    StartDialogue,
    StartMoving,
    StartTypingGame
}

public class InteractableOptions : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    private MemoryGameController memoryGameController;

    [SerializeField] private InteractionType interactionType;
    [SerializeField] private int ID;
    [SerializeField] private MovableObject movableObject;
    [SerializeField] private bool possibleInteract = true;
    private bool isPlayerInTrigger = false;

    void Start()
    {
        memoryGameController = FindObjectOfType<MemoryGameController>();
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

    void Update()
    {
        if (possibleInteract)
        {
            if (Input.GetKeyDown(UIManager.instance.dialogueKey) && isPlayerInTrigger)
            {
                if (!UIManager.instance.IsDialogueActive())
                {
                    ExecuteInteraction();
                    UIManager.instance.ShowInteractablePanel(false);
                    PlayerOutTrigger();
                }
            }
        }
    }

    private void ExecuteInteraction()
    {
        switch (interactionType)
        {
            case InteractionType.GenerateCards:
                memoryGameController?.StartMemoryGame();
                break;

            case InteractionType.StartDialogue:
                dialogueSystem?.StartDialogue(ID);
                break;
            
            case InteractionType.StartMoving:
                movableObject.StartMoving();
                break;

            case InteractionType.StartTypingGame:
                GameManager.instance.TypingGameStart();
                    break;
        }
    }

    public void PlayerInTrigger() => isPlayerInTrigger = true; 
    public void PlayerOutTrigger() => isPlayerInTrigger = false; 
    public void StartInterract() => possibleInteract = true;
    public void StopInterract() => possibleInteract = false;
}
