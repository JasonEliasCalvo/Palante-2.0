using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public string slotType;
    private string _selectedValue;

    public string SelectedValue { get => _selectedValue; set => _selectedValue = value; }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableIcon icon = eventData.pointerDrag.GetComponent<DraggableIcon>();
        if (icon != null && icon.type == slotType)
        {
            icon.RectTransform.position = transform.position;
            icon.RectTransform.SetParent(transform);
            //icon.RectTransform.position = Vector3.zero;
            SelectedValue = icon.value;
        }
        else
        {
            icon.RectTransform.position = icon.OriginalPosition;
        }
    }
}
