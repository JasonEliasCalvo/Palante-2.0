using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SyllableGame : MonoBehaviour
{
    [SerializeField] private List<WordData> wordList;
    [SerializeField] private AllSyllableData syllableDatas;
    [SerializeField] private int maxChoices = 4;
    [SerializeField] private int maxCorrectAnswers = 10;
    [SerializeField] private GameObject syllableButtonPrefab;
    [SerializeField] private TextMeshProUGUI syllableText;
    [SerializeField] private Transform syllableContainer;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource incorrectSound;

    public bool randomOrder = true;

    private int currentWordIndex = 0;
    private int correctAnswers = 0;
    private int lives = 3;

    private List<WordData> remainingWords;

    private void Start()
    {
        GameManager.instance.eventSyllableGameStart += StartGame;
        livesText.text = "Vidas: " + lives;
    }

    private void StartGame()
    {
        UIManager.instance.ShowSyllableGamePanel(true);
        GameManager.instance.GameEnd();

        remainingWords = new List<WordData>(wordList);

        if (randomOrder)
        {
            remainingWords = remainingWords.OrderBy(x => Random.value).ToList();
        }

        ShowNextWord();
    }

    private void ShowNextWord()
    {
        if (correctAnswers >= maxCorrectAnswers || currentWordIndex >= remainingWords.Count)
        {
            SyllableGameWin();
            return;
        }

        if (lives <= 0)
        {
            UIManager.instance.ShowSyllableGamePanel(false);
            remainingWords = new List<WordData>(wordList);
            GameManager.instance.GameStart();
            GameManager.instance.SyllableGameEnd();
            return;
        }

        WordData currentWord = remainingWords[currentWordIndex];
        syllableText.text = currentWord.fullWord.Replace(currentWord.correctSyllable, "...");
        GenerateChoices(currentWord);
    }

    private void GenerateChoices(WordData word)
    {
        Transform container = syllableContainer;
        foreach (Transform child in container) Destroy(child.gameObject);

        List<string> choices = new List<string>(word.syllables);

        while (choices.Count < maxChoices)
        {
            string fakeSyllable = syllableDatas.allSyllables[Random.Range(0, syllableDatas.allSyllables.Count)];

            if (!choices.Contains(fakeSyllable) && !word.fullWord.Contains(fakeSyllable))
            {
                choices.Add(fakeSyllable);
            }
        }

        choices = choices.OrderBy(x => Random.value).ToList();

        foreach (var choice in choices)
        {
            GameObject button = Instantiate(syllableButtonPrefab, container);
            button.GetComponentInChildren<TextMeshProUGUI>().text = choice;
            button.GetComponent<Button>().onClick.AddListener(() => CheckAnswer(choice, word.correctSyllable));
        }
    }

    private void CheckAnswer(string chosen, string correct)
    {
        if (chosen == correct)
        {
            correctAnswers++;
            correctSound.Play();
        }
        else
        {
            lives--;
            UpdateLivesUI();
            incorrectSound.Play();
        }

        currentWordIndex++;
        ShowNextWord();
    }
    private void UpdateLivesUI()
    {
        livesText.text = "Vidas: " + lives;
    }

    private void SyllableGameWin()
    {
        Debug.Log("Ganaste");
        UIManager.instance.ShowSyllableGamePanel(false);
        GameManager.instance.GameStart();
        remainingWords = new List<WordData>(wordList);
        GameManager.instance.SyllableGameEnd();
    }
}
