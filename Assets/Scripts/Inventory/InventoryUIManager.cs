using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using Combat;


public class InventoryUIManager : MonoBehaviour
{
    enum Characters
    {
        CHARACTER1,
        CHARACTER2,
        CHARACTER3
    }

    [Header("Characters")]
    [SerializeField] Button[] characterButtons;
    [SerializeField] Characters currentCharacter = Characters.CHARACTER1;
    [SerializeField] GameObject[] charactersModels;
    [SerializeField] Button[] characterSlots;
    [SerializeField] Button currentSlot;
    [SerializeField] Image[] characterSlotsImage;


    [Header("Screens")]
    [SerializeField] GameObject itemView;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject characterSlot;


    [Header("Deck")]
    [SerializeField] GameObject deckView;
    [SerializeField] Toggle deckToggle;


    [Header("Chest")]
    [SerializeField] List<ItemScriptableObject> chest;
    [SerializeField] List<ItemScriptableObject> char1;
    [SerializeField] List<ItemScriptableObject> char2;
    [SerializeField] List<ItemScriptableObject> char3;
    [SerializeField] List<Button> itensButtons;


    private void Start()
    {
        ScreensSetActive(false);
        CharacterButtonsFunction();
        DeckToggleFunction();
        CharacterSlotFunction();
        chest = Storage.LoadInventory();
        CharacterButtons(0);
        LoadChest();
        ItemButtonFunction();
        LoadCards();
    }

    #region  Adding Listeners
    private void CharacterButtonsFunction()
    {
        characterButtons[0].onClick.AddListener(() => { CharacterButtons(0); });
        characterButtons[1].onClick.AddListener(() => { CharacterButtons(1); });
        characterButtons[2].onClick.AddListener(() => { CharacterButtons(2); });
    }
    private void CharacterButtons(int buttonIndex)
    {
        itemView.SetActive(false);
        charactersModels[(int)currentCharacter].SetActive(false);
        currentCharacter = (Characters)buttonIndex;
        characterSlot.SetActive(true);
        charactersModels[buttonIndex].SetActive(true);
    }


    private void CharacterSlotFunction()
    {
        foreach (Button btn in characterSlots)
        {
            btn.onClick.AddListener(() => CharacterSlotButtons(btn));
        }
    }
    private void CharacterSlotButtons(Button slot)
    {

        itemView.SetActive(true);
        currentSlot = slot;
    }


    private void DeckToggleFunction()
    {
        deckToggle.onValueChanged.AddListener(delegate { DeckButton(); });
    }
    private void DeckButton()
    {
        deckView.SetActive(deckToggle.isOn);
    }

    private void ItemButtonFunction()
    {
        foreach (var itemBtn in itensButtons)
        {
            // Debug.Log(itemBtn);
            itemBtn.onClick.AddListener(() => ItemButton(itemBtn.GetComponent<InventoryItem>()));
        }
    }
    private void ItemButton(InventoryItem refItem)
    {
        if (currentSlot)
        {
            InventoryItem inventoryItem = currentSlot.GetComponent<InventoryItem>();
            inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
            inventoryItem.itemSO = refItem.itemSO;
            refItem.gameObject.SetActive(false);

            switch (currentCharacter)
            {
                case Characters.CHARACTER1: char1.Add(inventoryItem.itemSO); break;
                case Characters.CHARACTER2: char2.Add(inventoryItem.itemSO); break;
                case Characters.CHARACTER3: char3.Add(inventoryItem.itemSO); break;
                default: break;
            }
            // foreach (var item in chest)
            // {
            //     Debug.Log(item.name);
            // }
            // foreach (var item in char1)
            // {
            //     Debug.Log(item.name);
            // }
            // foreach (var item in char2)
            // {
            //     Debug.Log(item.name);
            // }
            // foreach (var item in char3)
            // {
            //     Debug.Log(item.name);
            // }

            Storage.SaveInventory(chest, char1, char2, char3);
            LoadCards();
        }
    }
    #endregion

    private void LoadChest()
    {
        foreach (var i in chest)
        {
            InventoryItem item = Instantiate(itemPrefab, itemView.transform.GetChild(0).GetChild(0)).GetComponent<InventoryItem>();
            itensButtons.Add(item.GetComponent<Button>());
            item.itemImage.sprite = i.sprite;
            item.itemSO = i;
        }
    }
    private void LoadCards()
    {
        for (int i = 0; i < deckView.transform.GetChild(0).GetChild(0).childCount; i++)
        {
            Destroy(deckView.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
        }
        foreach (var i in char1)
        {
            foreach (var j in i.cards)
            {
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0)).GetComponent<InventoryCard>();
                card.Setup(j);
            }
        }
        foreach (var i in char2)
        {
            foreach (var j in i.cards)
            {
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0)).GetComponent<InventoryCard>();
                card.Setup(j);
            }
        }
        foreach (var i in char3)
        {
            foreach (var j in i.cards)
            {
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0)).GetComponent<InventoryCard>();
                card.Setup(j);
            }
        }
    }

    private void ScreensSetActive(bool value)
    {
        deckView.SetActive(value);
        itemView.SetActive(value);
        characterSlot.SetActive(value);
    }
}
