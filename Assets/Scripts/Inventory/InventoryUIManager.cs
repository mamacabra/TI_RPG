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
    [SerializeField] Button[] characterSlots;
    [SerializeField] Button currentSlot;
    [SerializeField] Image[] characterSlotsImage;


    [Header("Screens")]
    [SerializeField] GameObject itemView;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject cardPrefab;
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
    [SerializeField] List<Button> itensButtons;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        chest = Storage.LoadInventory();
        char1 = Storage.LoadHeroInventory(1);
        char2 = Storage.LoadHeroInventory(2);
        char3 = Storage.LoadHeroInventory(3);
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
                        InventoryItem inventoryItem = itemSlot.GetComponent<InventoryItem>();
                        if (char1.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char1[i].sprite;
                            inventoryItem.itemSO = char1[i];
                            inventoryItem.id = i;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.id = -1;
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
                        InventoryItem inventoryItem = itemSlot.GetComponent<InventoryItem>();
                        if (char2.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char2[i].sprite;
                            inventoryItem.itemSO = char2[i];
                            inventoryItem.id = i;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.id = -1;
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
                        InventoryItem inventoryItem = itemSlot.GetComponent<InventoryItem>();
                        if (char3.Count > i)
                        {
                            inventoryItem.itemImage.sprite = char3[i].sprite;
                            inventoryItem.itemSO = char3[i];
                            inventoryItem.id = i;
                        }
                        else
                        {
                            inventoryItem.itemImage.sprite = null;
                            inventoryItem.itemSO = null;
                            inventoryItem.id = -1;
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
        foreach (Button btn in characterSlots)
        {
            btn.onClick.AddListener(() => CharacterSlotButtons(btn));
        }
    }
    private void CharacterSlotButtons(Button slot)
    {
        // Debug.Log(deckView.transform.GetChild(0).GetChild(0).childCount);
        if (!itemView.activeInHierarchy)
        {
            itemView.SetActive(true);
        }
        else
        {
            InventoryItem item = slot.GetComponent<InventoryItem>();
            if (item.itemImage.sprite != null)//tem item no slot
            {
                // if ((deckView.transform.GetChild(0).GetChild(0).childCount - item.itemSO.cards.Length) > 0)
                // {
                switch (currentCharacter)
                {
                    case Characters.CHARACTER1:
                        if ((char1.Count - 1) > 0)
                        {
                            char1.Remove(item.itemSO);
                            itensButtons.Find(i => i.GetComponent<InventoryItem>().id == item.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
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
                            itensButtons.Find(i => i.GetComponent<InventoryItem>().id == item.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
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
                            itensButtons.Find(i => i.GetComponent<InventoryItem>().id == item.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
                            item.itemImage.sprite = null;
                        }
                        else
                        {
                            ShowAlert();
                        }
                        break;
                    default: break;
                }
                // }
                // else
                // {
                //     ShowAlert();
                // }

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
            itemBtn.onClick.AddListener(() => ItemButton(itemBtn.GetComponent<InventoryItem>()));
        }
    }
    private void ItemButton(InventoryItem refItem)
    {
        if (currentSlot)
        {
            HideCardsPreview();
            InventoryItem inventoryItem = currentSlot.GetComponent<InventoryItem>();
            if (inventoryItem.itemImage.sprite != null)
            {
                switch (currentCharacter)
                {
                    case Characters.CHARACTER1:
                        char1.Remove(inventoryItem.itemSO);
                        itensButtons.Find(i => i.GetComponent<InventoryItem>().id == inventoryItem.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    case Characters.CHARACTER2:
                        char2.Remove(inventoryItem.itemSO);
                        itensButtons.Find(i => i.GetComponent<InventoryItem>().id == inventoryItem.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    case Characters.CHARACTER3:
                        char3.Remove(inventoryItem.itemSO);
                        itensButtons.Find(i => i.GetComponent<InventoryItem>().id == inventoryItem.GetComponent<InventoryItem>().id).gameObject.SetActive(true);
                        inventoryItem.itemImage.sprite = null;
                        break;
                    default: break;
                }
                inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
                inventoryItem.itemSO = refItem.itemSO;
                inventoryItem.id = refItem.id;
            }
            inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
            inventoryItem.itemSO = refItem.itemSO;
            inventoryItem.id = refItem.id;
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
    public void ShowCardsPreview(ItemScriptableObject item)
    {
        cardsPreview.SetActive(true);
        foreach (var cardObj in item.cards)
        {
            InventoryCard card = Instantiate(cardPrefab, cardsPreview.transform.GetChild(0)).GetComponent<InventoryCard>();
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
        int j = 0;
        foreach (var i in chest)
        {
            InventoryItem item = Instantiate(itemPrefab, itemView.transform.GetChild(0).GetChild(0)).GetComponent<InventoryItem>();
            itensButtons.Add(item.GetComponent<Button>());
            item.itemImage.sprite = i.sprite;
            item.itemSO = i;
            item.id = j;
            if (char1.Contains(i) || char2.Contains(i) || char3.Contains(i))
            {
                item.gameObject.SetActive(false);
            }
            j++;
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
