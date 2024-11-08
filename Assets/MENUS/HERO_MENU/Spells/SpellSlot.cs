using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public Ability ability;

    private Image draggedIcon;
    private Canvas rootCanvas;

    private void Awake()
    {
        // Locate the root canvas (assumes the script is part of a UI hierarchy under a Canvas)
        rootCanvas = GetComponentInParent<Canvas>();
        if (rootCanvas == null)
        {
            Debug.LogError("No root canvas found in parent hierarchy.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag called");

        if (rootCanvas == null) return;

        // Create a new image for dragging
        draggedIcon = new GameObject("DraggedIcon").AddComponent<Image>();
        draggedIcon.sprite = icon.sprite;
        draggedIcon.transform.SetParent(rootCanvas.transform, false); // Set to root canvas
        //draggedIcon.rectTransform.sizeDelta = icon.rectTransform.sizeDelta; // Match size of the original icon
        draggedIcon.rectTransform.sizeDelta = new Vector2(60, 60);

        // Set position to the mouse position
        UpdateDraggedIconPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag called");

        // Update the dragged icon position
        if (draggedIcon != null)
        {
            UpdateDraggedIconPosition(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag called");

        // Destroy the dragged icon when dragging ends
        if (draggedIcon != null)
        {
            //Destroy(draggedIcon.gameObject);
        }
    }

    private void UpdateDraggedIconPosition(PointerEventData eventData)
    {
        if (rootCanvas == null) return;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootCanvas.transform as RectTransform,
            eventData.position,
            rootCanvas.worldCamera,
            out Vector2 localPoint);

        draggedIcon.rectTransform.localPosition = localPoint;
    }

    internal void Init(Ability ability)
    {
        Debug.Log("Init method called");
        this.ability = ability;
        icon.sprite = ability.Icon;
    }
}
