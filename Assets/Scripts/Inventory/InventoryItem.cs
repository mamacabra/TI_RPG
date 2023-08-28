using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Combat;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string cardName;
    public Image itemImage;
    public ItemScriptableObject itemSO;
    public bool itemSlot;

    public Button button;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!itemSlot)
        {
            InventoryUIManager.instance?.ShowCardsPreview(itemSO);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!itemSlot)
        {
            InventoryUIManager.instance?.HideCardsPreview();
        }
    }
}
