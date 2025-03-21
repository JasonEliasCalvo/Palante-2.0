using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public enum InteractionType
{
    GenerateCards,
    StartDialogue,
    StartMoving,
    StartTypingGame,
    StartSyllableGame,
}

public class InteractableOptions : MonoBehaviour
{
    private DialogueSystem dialogueSystem;
    private MemoryGameController memoryGameController;

    [SerializeField] private InteractionType interactionType;
    public InteractionType InteractionType { get => interactionType; set => interactionType = value; }

    public int iD;

    public MovableObject movableObject;

    public static bool possibleInteract = true;
    private bool isPlayerInTrigger = false;

    [SerializeField] private bool justAnInterraction = false;

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
                if (!UIManager.instance.IsInterractionActive())
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
        if (justAnInterraction) { StopInterract(); }
        switch (InteractionType)
        {
            case InteractionType.GenerateCards:
                memoryGameController?.StartMemoryGame();
                break;

            case InteractionType.StartDialogue:
                dialogueSystem?.StartDialogue(iD);
                break;
            
            case InteractionType.StartMoving:
                movableObject.StartMoving();
                break;

            case InteractionType.StartTypingGame:
                GameManager.instance.TypingGameStart();
                break;

            case InteractionType.StartSyllableGame:
                GameManager.instance.SyllableGameStart();
                break;
        }
    }

    public void PlayerInTrigger() => isPlayerInTrigger = true; 
    public void PlayerOutTrigger() => isPlayerInTrigger = false; 
    public void StartInterract() => possibleInteract = true;
    public void StopInterract() => possibleInteract = false;
    public void ShowInteractionPanel(bool state) => UIManager.instance.ShowInteractablePanel(state);
}
