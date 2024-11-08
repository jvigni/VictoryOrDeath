using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image icon;
    public Ability ability;

    private Canvas rootCanvas;
    private Vector2 originalPosition;
    private Vector2 dragOffset;
    private RectTransform rectTransform;

    private void Awake()
    {
        // Locate the root canvas (assumes the script is part of a UI hierarchy under a Canvas)
        rootCanvas = GetComponentInParent<Canvas>();
        if (rootCanvas == null)
        {
            Debug.LogError("No root canvas found in parent hierarchy.");
        }

        // Cache the RectTransform component for position adjustments
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag called");

        // Save the original position to return after dragging
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;

            // Calculate the offset between the mouse position and the center of the slot
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rootCanvas.transform as RectTransform,
                eventData.position,
                rootCanvas.worldCamera,
                out mousePosition
            );
            dragOffset = rectTransform.anchoredPosition - mousePosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag called");

        // Move the SpellSlot with the cursor, adjusting for the initial offset
        if (rectTransform != null && rootCanvas != null)
        {
            Vector2 mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rootCanvas.transform as RectTransform,
                eventData.position,
                rootCanvas.worldCamera,
                out mousePosition
            );
            rectTransform.anchoredPosition = mousePosition + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag called");

        // Return to original position
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    internal void Init(Ability ability)
    {
        Debug.Log("Init method called");
        this.ability = ability;
        icon.sprite = ability.Icon;
    }
}
