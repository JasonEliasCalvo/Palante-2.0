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
    [SerializeField] private GameObject _continueIcon;

    [Header("UI Game Elements")]
    [SerializeField] private GameObject interactablePanel;
    [SerializeField] private GameObject typingPanel;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private GameObject syllableGamePanel;

    [Header("UI Settings")]
    public KeyCode dialogueKey = KeyCode.F;

    public void ShowContinueIcon(bool state) => _continueIcon.SetActive(state);


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
        if (!IsPanelActive())
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

    public bool IsPanelActive()
    {
        return dialoguePanel.activeSelf || syllableGamePanel.activeSelf || choicesPanel.activeSelf || typingPanel.activeSelf;
    }

    //public GameObject GetSyllableButtonPrefab() => syllableButtonPrefab;
    //public Transform GetSyllableContainer() => syllableContainer;
    //public TextMeshProUGUI GetSyllableText() => syllableText;
    public TextMeshProUGUI GetDialogueText() => dialogueText;
    public TextMeshProUGUI GetQuestionText() => choicesText;
    public Transform GetChoiceContainer() => choicesContainer;
    public GameObject GetChoiceButtonPrefab() => choiceButtonPrefab;
}
