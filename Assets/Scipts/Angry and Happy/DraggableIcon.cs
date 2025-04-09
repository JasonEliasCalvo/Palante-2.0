using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum GenderType
{
    none,
    male,
    Female
}

public enum EmotionType
{
    none,
    Angry,
    Happy,
}

public class DraggableIcon : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    [SerializeField] private GameObject happyAndAngrypanel;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private Transform originalParent;

    public GenderType gender;
    public EmotionType emotion;
    public EnumType enumType;

    public RectTransform RectTransform { get => rectTransform; set => rectTransform = value; }
    public Vector3 OriginalPosition { get => originalPosition; set => originalPosition = value; }
    public Transform OriginalParent { get => originalParent; set => originalParent = value; }

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalPosition = rectTransform.position;
        OriginalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent != transform.parent)
        {
            RectTransform.position = OriginalPosition;
        }
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (transform.parent == OriginalParent)
        {
            RectTransform.position = OriginalPosition;
        }
    }
}
