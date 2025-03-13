using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField] private List<CardData> cardDataList;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform boardParent;
    [SerializeField] private int columns = 4;
    [SerializeField] private float spacing = 1f;

    private List<MemoryCard> cards = new List<MemoryCard>();
    private Vector3 StartRotation = new Vector3(0, 0, -90);
    private MemoryCard firstCard, secondCard;
    public static bool isCheckingMatch = false;
    [SerializeField] private GameObject cam;

    public void StartMemoryGame()
    {
        GenerateCards();
        cam.SetActive(true);
    }

    public void GenerateCards()
    {
        List<CardData> allCards = new List<CardData>(cardDataList);
        allCards.AddRange(cardDataList);
        Shuffle(allCards);

        for (int i = 0; i < allCards.Count; i++)
        {
            int row = i / columns;
            int col = i % columns;

            Vector3 position = boardParent.position + new Vector3(col * spacing, 0, row * spacing);
            GameObject newCard = Instantiate(cardPrefab, position, Quaternion.Euler(StartRotation), boardParent);
            MemoryCard memoryCard = newCard.GetComponent<MemoryCard>();
            memoryCard.SetupCard(allCards[i]);
            cards.Add(memoryCard);
        }
    }

    private void Shuffle(List<CardData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            CardData temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    private void OnEnable()
    {
        MemoryCard.OnCardFlipped += CheckMatch;
    }

    private void OnDisable()
    {
        MemoryCard.OnCardFlipped -= CheckMatch;
    }

    private void CheckMatch(MemoryCard card)
    {
        if (isCheckingMatch) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            isCheckingMatch = true;
            StartCoroutine(CompareCards());
        }
    }

    private IEnumerator CompareCards()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.cardData.cardName == secondCard.cardData.cardName)
        {
            firstCard.MarkAsMatched();
            secondCard.MarkAsMatched();
            CheckGameOver();
        }
        else
        {
            firstCard.HideCard();
            secondCard.HideCard();
        }

        firstCard = null;
        secondCard = null;
        isCheckingMatch = false;
    }

    private void CheckGameOver()
    {
        if (cards.All(card => card.IsMatched()))
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        cam.gameObject.SetActive(false);
        Debug.Log("🎉 ¡Ganaste! Todas las cartas han sido emparejadas.");
    }
}
