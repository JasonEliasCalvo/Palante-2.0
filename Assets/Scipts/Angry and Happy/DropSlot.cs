using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[Serializable]
public class EnumType
{
    public bool isGender;
    public bool isEmotion;
}

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GenderType gender;
    public EmotionType emotion;

    public EnumType enumType;

    public void OnDrop(PointerEventData eventData)
    {
        DraggableIcon icon = eventData.pointerDrag.GetComponent<DraggableIcon>();

        if (icon != null && enumType.isGender == icon.enumType.isGender)
        {
            icon.RectTransform.SetParent(transform);
            icon.RectTransform.position = transform.position;
            gender = icon.gender;
        }

        if (icon != null && enumType.isEmotion == icon.enumType.isEmotion)
        {
            icon.RectTransform.SetParent(transform);
            icon.RectTransform.position = transform.position;
            emotion = icon.emotion;
        }

        else
        {
            icon.RectTransform.position = icon.OriginalPosition;
        }
    }

}
