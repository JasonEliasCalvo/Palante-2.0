using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Dialogue Elements")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI choicesText;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private GameObject choiceButtonPrefab;

    [Header("UI Game Elements")]
    [SerializeField] private GameObject interactablePanel;
    [SerializeField] private GameObject typingPanel;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private GameObject syllableGamePanel;
    [SerializeField] private GameObject syllableButtonPrefab;
    [SerializeField] private TextMeshProUGUI syllableText;
    [SerializeField] private Transform syllableContainer;

    [Header("UI Settings")]
    public KeyCode dialogueKey = KeyCode.F;

    //[Serializable]
    //public class DialogueUI
    //{
    //    public GameObject interactablePanel;
    //    public GameObject dialoguePanel;
    //    public GameObject choicesPanel;

    //    public TextMeshProUGUI dialogueText;
    //    public TextMeshProUGUI choicesText;
    //    public Transform choicesContainer;
    //    public GameObject choiceButtonPrefab;
    //}

    //[Header("Dialogue Elements")]
    //[SerializeField] private DialogueUI dialogueUI;

    //public DialogueUI GetDialogueUI() => dialogueUI;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        instance.ShowDialoguePanel(false);
        instance.ShowChoicesPanel(false);
    }

    public void ShowInteractablePanel(bool state)
    {
        if (!IsInterractionActive())
        {
            interactablePanel.SetActive(state);
        }
        else
        {
            interactablePanel.SetActive(false);
        }
    }

    public void ShowDialoguePanel(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    public void ShowChoicesPanel(bool state)
    {
        choicesPanel.SetActive(state);
    }

    public void ShowTimerPanel(bool state)
    {
        timerPanel.SetActive(state);
    }

    public void ShowTypingPanel(bool state)
    {
        typingPanel.SetActive(state);
    }

    public void ShowSyllableGamePanel(bool state) => syllableGamePanel.SetActive(state);

    public bool IsInterractionActive()
    {
        return dialoguePanel.activeSelf || syllableGamePanel.activeSelf || choicesPanel.activeSelf || typingPanel.activeSelf;
    }

    public GameObject GetSyllableButtonPrefab() => syllableButtonPrefab;
    public Transform GetSyllableContainer() => syllableContainer;
    public TextMeshProUGUI GetSyllableText() => syllableText;
    public TextMeshProUGUI GetDialogueText() => dialogueText;
    public TextMeshProUGUI GetQuestionText() => choicesText;
    public Transform GetChoiceContainer() => choicesContainer;
    public GameObject GetChoiceButtonPrefab() => choiceButtonPrefab;
}
