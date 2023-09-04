using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using Combat;


public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
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
    [SerializeField] InventoryItem[] characterSlots;
    [SerializeField] InventoryItem currentSlot;
    [SerializeField] Image[] characterSlotsImage;


    [Header("Screens")]
    [SerializeField] GameObject itemView;
    [SerializeField] InventoryItem itemPrefab;
    [SerializeField] InventoryCard cardPrefab;
    [SerializeField] GameObject characterSlot;
    [SerializeField] GameObject cardsPreview;
    [SerializeField] GameObject alert;


    [Header("Deck")]
    [SerializeField] GameObject deckView;
    [SerializeField] Toggle deckToggle;


    [Header("Chest")]
    [SerializeField] List<ItemScriptableObject> chest;
    [SerializeField] List<ItemScriptableObject> char1 = new();
    [SerializeField] List<ItemScriptableObject> char2 = new();
    [SerializeField] List<ItemScriptableObject> char3 = new();
    [SerializeField] List<InventoryItem> itensButtons;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        chest = Storage.LoadInventory();
        // char1 = Storage.LoadHeroInventory(1);
        // char2 = Storage.LoadHeroInventory(2);
        // char3 = Storage.LoadHeroInventory(3);
        ScreensSetActive(false);
        CharacterButtonsFunction();
        DeckToggleFunction();
        CharacterSlotFunction();
        LoadChest();
        ItemButtonFunction();
        LoadCards();
        CharacterButtons(0);
    }
    public bool VerifyCards()
    {
        bool hasCards = false;
        if (char1.Count > 0 && char2.Count > 0 && char3.Count > 0)
        {
            hasCards = true;
        }
        if (!hasCards)
        {
            ShowAlert();
        }
        return hasCards;
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
        //AudioManager.audioManager.PlaySoundEffect("Menu/Click");
        itemView.SetActive(false);
        charactersModels[(int)currentCharacter].SetActive(false);
        currentCharacter = (Characters)buttonIndex;
        characterSlot.SetActive(true);
        switch (currentCharacter)
        {
            case Characters.CHARACTER1:
                {
                    int i = 0;
                    foreach (var itemSlot in characterSlots)
                    {
                        InventoryItem inventoryItem = itemSlot;
                        if (char1.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char1[i].sprite;
                            inventoryItem.itemSO = char1[i];
                            inventoryItem.cardName = char1[i].name;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.cardName = "";
                        }
                        i++;
                    }
                    break;
                }
            case Characters.CHARACTER2:
                {
                    int i = 0;
                    foreach (var itemSlot in characterSlots)
                    {
                        InventoryItem inventoryItem = itemSlot;
                        if (char2.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char2[i].sprite;
                            inventoryItem.itemSO = char2[i];
                            inventoryItem.cardName = char2[i].name;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.cardName = "";
                        }
                        i++;
                    }
                    break;
                }
            case Characters.CHARACTER3:
                {
                    int i = 0;
                    foreach (var itemSlot in characterSlots)
                    {
                        InventoryItem inventoryItem = itemSlot;
                        if (char3.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char3[i].sprite;
                            inventoryItem.itemSO = char3[i];
                            inventoryItem.cardName = char3[i].name;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.cardName = "";
                        }
                        i++;
                    }
                    break;
                }
        }
        charactersModels[buttonIndex].SetActive(true);
    }


    private void CharacterSlotFunction()
    {
        foreach (InventoryItem item in characterSlots)
        {
            item.button.onClick.AddListener(() => CharacterSlotButtons(item));
        }
    }
    private void CharacterSlotButtons(InventoryItem slot)
    {
        //AudioManager.audioManager.PlaySoundEffect("InventoryEffects/SelectSlot");
        if (!itemView.activeInHierarchy)
        {
            itemView.SetActive(true);
        }
        else
        {
            InventoryItem item = slot;
            if (item.itemImage.sprite != null)
            {
                switch (currentCharacter)
                {
                    case Characters.CHARACTER1:
                        if ((char1.Count - 1) > 0)
                        {
                            char1.Remove(item.itemSO);
                            InventoryItem item1 = null;
                            for (int i = 0; i < itensButtons.Count; i++)
                            {
                                if (itensButtons[i].cardName == item.cardName)
                                {
                                    item1 = itensButtons[i];
                                    break;
                                }
                            }
                            item1?.gameObject.SetActive(true);
                            item.itemImage.sprite = null;
                        }
                        else
                        {
                            ShowAlert();
                        }
                        break;
                    case Characters.CHARACTER2:
                        if ((char2.Count - 1) > 0)
                        {
                            char2.Remove(item.itemSO);
                            InventoryItem item2 = null;
                            for (int i = 0; i < itensButtons.Count; i++)
                            {
                                if (itensButtons[i].cardName == item.cardName)
                                {
                                    item2 = itensButtons[i];
                                    break;
                                }
                            }
                            item2?.gameObject.SetActive(true);
                            item.itemImage.sprite = null;
                        }
                        else
                        {
                            ShowAlert();
                        }
                        break;
                    case Characters.CHARACTER3:
                        if ((char3.Count - 1) > 0)
                        {
                            char3.Remove(item.itemSO);
                            InventoryItem item3 = null;
                            for (int i = 0; i < itensButtons.Count; i++)
                            {
                                if (itensButtons[i].cardName == item.cardName)
                                {
                                    item3 = itensButtons[i];
                                    break;
                                }
                            }
                            item3?.gameObject.SetActive(true);
                            item.itemImage.sprite = null;
                        }
                        else
                        {
                            ShowAlert();
                        }
                        break;
                    default: break;
                }
            }
            LoadCards();
        }
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
            itemBtn.button.onClick.AddListener(() => ItemButton(itemBtn));
        }
    }
    private void ItemButton(InventoryItem refItem)
    {
        //AudioManager.audioManager.PlaySoundEffect("InventoryEffects/PlaceItem");
        if (currentSlot)
        {
            HideCardsPreview();
            InventoryItem inventoryItem = currentSlot;
            if (inventoryItem.itemImage.sprite != null)
            {
                switch (currentCharacter)
                {
                    case Characters.CHARACTER1:
                        char1.Remove(inventoryItem.itemSO);
                        InventoryItem item1 = null;
                        for (int i = 0; i < itensButtons.Count; i++)
                        {
                            if (itensButtons[i].cardName == inventoryItem.cardName)
                            {
                                item1 = itensButtons[i];
                                break;
                            }
                        }
                        item1?.gameObject.SetActive(true);
                        //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    case Characters.CHARACTER2:
                        char2.Remove(inventoryItem.itemSO);
                        InventoryItem item2 = null;
                        for (int i = 0; i < itensButtons.Count; i++)
                        {
                            if (itensButtons[i].cardName == inventoryItem.cardName)
                            {
                                item2 = itensButtons[i];
                                break;
                            }
                        }
                        item2?.gameObject.SetActive(true);
                        //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    case Characters.CHARACTER3:
                        char3.Remove(inventoryItem.itemSO);
                        InventoryItem item3 = null;
                        for (int i = 0; i < itensButtons.Count; i++)
                        {
                            if (itensButtons[i].cardName == inventoryItem.cardName)
                            {
                                item3 = itensButtons[i];
                                break;
                            }
                        }
                        item3?.gameObject.SetActive(true);
                        //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    default: break;
                }
                inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
                inventoryItem.itemSO = refItem.itemSO;
                inventoryItem.cardName = refItem.cardName;
            }
            inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
            inventoryItem.itemSO = refItem.itemSO;
            inventoryItem.cardName = refItem.cardName;
            refItem.gameObject.SetActive(false);

            switch (currentCharacter)
            {
                case Characters.CHARACTER1: char1.Add(inventoryItem.itemSO); break;
                case Characters.CHARACTER2: char2.Add(inventoryItem.itemSO); break;
                case Characters.CHARACTER3: char3.Add(inventoryItem.itemSO); break;
                default: break;
            }

            Storage.SaveInventory(chest, char1, char2, char3);
            LoadCards();
        }
    }
    public void ShowCardsPreview(ItemScriptableObject item)
    {
        cardsPreview.SetActive(true);
        foreach (var cardObj in item.cards)
        {
            InventoryCard card = Instantiate(cardPrefab, cardsPreview.transform.GetChild(0));
            card.Setup(cardObj);
        }
    }
    public void HideCardsPreview()
    {
        cardsPreview.SetActive(false);
        for (int i = 0; i < cardsPreview.transform.GetChild(0).childCount; i++)
        {
            Destroy(cardsPreview.transform.GetChild(0).GetChild(i).gameObject);
        }
    }
    #endregion

    private void LoadChest()
    {
        foreach (var i in chest)
        {
            InventoryItem item = Instantiate(itemPrefab, itemView.transform.GetChild(0).GetChild(0));
            itensButtons.Add(item);
            item.itemImage.sprite = i.sprite;
            item.itemSO = i;
            item.cardName = i.name;
            // if (char1.Contains(i) || char2.Contains(i) || char3.Contains(i))
            // {
            //     item.gameObject.SetActive(false);
            // }
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
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
                card.Setup(j);
            }
        }
        foreach (var i in char2)
        {
            foreach (var j in i.cards)
            {
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
                card.Setup(j);
            }
        }
        foreach (var i in char3)
        {
            foreach (var j in i.cards)
            {
                InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
                card.Setup(j);
            }
        }
    }

    private void ShowAlert()
    {
        StartCoroutine(ShowDelay());
        IEnumerator ShowDelay()
        {
            alert.SetActive(true);
            yield return new WaitForSeconds(3);
            alert.SetActive(false);
        }
    }

    private void ScreensSetActive(bool value)
    {
        deckView.SetActive(value);
        itemView.SetActive(value);
        characterSlot.SetActive(value);
    }
}
