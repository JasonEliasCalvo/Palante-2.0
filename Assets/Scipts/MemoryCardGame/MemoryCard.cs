using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MemoryCard : MonoBehaviour
{
    public CardData cardData;
    public GameObject frontSide;
    public GameObject backSide;

    private bool isFlipped;
    [SerializeField] private bool isMatched;
    [SerializeField] private Vector3 HideRotation = new (0, 0, -90);
    [SerializeField] private Vector3 FlipRotation = new (180, 0, -90);

    [SerializeField] private float duration = 1f;
    public static UnityAction<MemoryCard> OnCardFlipped;

    public void SetupCard(CardData data)
    {
        cardData = data;
        frontSide.GetComponent<SpriteRenderer>().sprite = data.cardFront;
        HideCard();
    }

    private void OnMouseDown()
    {
        FlipCard();
    }

    public void FlipCard()
    {
        if (isFlipped || isMatched || MemoryGameController.isCheckingMatch) return;

        isFlipped = true;
        transform.DORotate(FlipRotation, duration, RotateMode.Fast);

        OnCardFlipped?.Invoke(this);
    }

    public void HideCard()
    {
        if (isMatched) return;

        transform.DORotate(HideRotation, duration, RotateMode.Fast);
        isFlipped = false;
    }

    public void MarkAsMatched()
    {
        isMatched = true;
    }

    public bool IsMatched() { return isMatched; }
}
