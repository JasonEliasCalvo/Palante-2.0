using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotType;
    [SerializeField]private string _selectedValue;
    public static UnityAction<string> OnDropIcon;

    public string SelectedValue { get => _selectedValue; set => _selectedValue = value; }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableIcon icon = eventData.pointerDrag.GetComponent<DraggableIcon>();
        if (icon != null && icon.type == slotType)
        {
            icon.RectTransform.SetParent(transform);
            icon.RectTransform.position = transform.position;
            SelectedValue = icon.value;
        }
        else
        {
            icon.RectTransform.position = icon.OriginalPosition;
        }
    }

}
