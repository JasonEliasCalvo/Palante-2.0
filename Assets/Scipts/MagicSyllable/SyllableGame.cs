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
    public bool randomOrder = true;

    private int currentWordIndex = 0;
    private int correctAnswers = 0;
    private int lives = 3;

    private List<WordData> remainingWords;

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private AudioSource correctSound;
    [SerializeField] private AudioSource incorrectSound;

    private void Start()
    {
        GameManager.instance.eventSyllableGameStart += StartGame;
        livesText.text = "Vidas: " + lives;
    }

    private void StartGame()
    {
        UIManager.instance.ShowSyllableGamePanel(true);

        remainingWords = new List<WordData>(wordList);

        if (randomOrder)
        {
            remainingWords = remainingWords.OrderBy(x => Random.value).ToList();
        }

        ShowNextWord();
    }

    private void ShowNextWord()
    {
        if (correctAnswers >= 10 || currentWordIndex >= remainingWords.Count)
        {
            SyllableGameWin();
            return;
        }

        if (lives <= 0)
        {
            UIManager.instance.ShowSyllableGamePanel(false);
            GameManager.instance.SyllableGameEnd();
            return;
        }

        WordData currentWord = remainingWords[currentWordIndex];
        UIManager.instance.GetSyllableText().text = currentWord.fullWord.Replace(currentWord.correctSyllable, "...");
        GenerateChoices(currentWord);
    }

    private void GenerateChoices(WordData word)
    {
        Transform container = UIManager.instance.GetSyllableContainer();
        foreach (Transform child in container) Destroy(child.gameObject);

        List<string> choices = new List<string>(word.syllables);

        while (choices.Count < 4)
        {
            string fakeSyllable = syllableDatas.allSyllables[Random.Range(0, syllableDatas.allSyllables.Count)];

            if (!choices.Contains(fakeSyllable) && !word.fullWord.Contains(fakeSyllable))
            {
                choices.Add(fakeSyllable);
            }
        }

        foreach (var choice in choices)
        {
            GameObject button = Instantiate(UIManager.instance.GetSyllableButtonPrefab(), container);
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
        GameManager.instance.SyllableGameEnd();
    }
}
