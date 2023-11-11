using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using Combat;
using Unity.VisualScripting;


public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    enum Characters
    {
        CHARACTER1 = 0,
        CHARACTER2 = 1,
        CHARACTER3 = 2
    }

    [Header("Characters")]
    [SerializeField] Characters currentCharacter = Characters.CHARACTER1;
    [SerializeField] InventoryItem currentSlot;
    [SerializeField] Toggle[] characterToggles;
    [SerializeField] GameObject[] charactersModels;
    [SerializeField] InventoryItem[] characterSlots;

    [Header("Screens")]
    [SerializeField] GameObject itemView;
    [SerializeField] GameObject characterSlot;
    [SerializeField] GameObject cardsPreview;
    [SerializeField] GameObject alert;

    [Header("Prefabs")]
    [SerializeField] InventoryItem itemPrefab;
    [SerializeField] InventoryCard cardPrefab;

    [Header("Deck")]
    [SerializeField] GameObject deckView;
    [SerializeField] Toggle deckToggle;

    [Header("Chest")]
    [SerializeField] List<ItemScriptableObject> chest = new List<ItemScriptableObject>();
    [SerializeField] List<ItemScriptableObject>[] characters = new List<ItemScriptableObject>[3];
    [SerializeField] List<InventoryItem> itensButtons;


    private void Awake(){
        instance = this;
    }
    private void Start(){
        chest = InventoryManager.instance.inventoryData.inventory;
        
        for (int i = 0; i < characters.Length; i++){
            characters[i] = Storage.LoadHeroInventory(i);
            //characters[i] = new();
        }
        ScreensSetActive(false);
        SetCharacterTogglesFunction();
        SetDeckToggleFunction();
        SetCharacterSlotFunction();
        FillChest();
        SetItemButtonFunction();
        FillCards();
        CharacterToggleClick(0, true);
    }
    public bool VerifyCards(){
        bool hasCards = false;
        foreach (var character in characters){
            if(character.Count > 0)
                hasCards = true;
            else{
                hasCards = false;
                break;
            }
        }
        if (!hasCards){
            ShowAlert();
        }
        return hasCards;
    }

    #region  Adding Listeners
    private void SetCharacterTogglesFunction(){
        characterToggles[0].onValueChanged.AddListener((bool toggleValue) => {CharacterToggleClick(0, toggleValue);});
        characterToggles[1].onValueChanged.AddListener((bool toggleValue) => {CharacterToggleClick(1, toggleValue);});
        characterToggles[2].onValueChanged.AddListener((bool toggleValue) => {CharacterToggleClick(2, toggleValue);});
        // characterToggles[0].onClick.AddListener(() => { CharacterButtons(0); });
        // characterButtons[1].onClick.AddListener(() => { CharacterButtons(1); });
        // characterButtons[2].onClick.AddListener(() => { CharacterButtons(2); });
    }
    private void CharacterToggleClick(int index, bool toggleValue){
        //AudioManager.audioManager.PlaySoundEffect("Menu/Click");
        if(toggleValue){
            //itemView.SetActive(false);
            charactersModels[(int)currentCharacter].SetActive(false);
            currentCharacter = (Characters)index;
            characterSlot.SetActive(true);

            int i = 0;
            foreach (var itemSlot in characterSlots){
                InventoryItem inventoryItem = itemSlot;
                if(characters[(int)currentCharacter].Count > i){
                    inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
                    inventoryItem.itemImage.sprite = characters[(int)currentCharacter][i].sprite;
                    inventoryItem.itemSO = characters[(int)currentCharacter][i];
                    inventoryItem.itemName = characters[(int)currentCharacter][i].cardName;
                } else{
                    Color colorAlpha = new Color(0, 0, 0, 0);
                    inventoryItem.itemImage.color = colorAlpha;
                    inventoryItem.itemSO = null;
                    inventoryItem.itemName = "";
                }
                i++;
            }
            charactersModels[index].SetActive(true);
        }
        // switch (currentCharacter)
        // {
        //     case Characters.CHARACTER1:
        //         {
        //             int i = 0;
        //             foreach (var itemSlot in characterSlots)
        //             {
        //                 InventoryItem inventoryItem = itemSlot;
        //                 if (char1.Count > i)
        //                 {
        //                     inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
        //                     inventoryItem.itemImage.sprite = char1[i].sprite;
        //                     inventoryItem.itemSO = char1[i];
        //                     inventoryItem.cardName = char1[i].cardName;
        //                 }
        //                 else
        //                 {
        //                     Color colorAlpha = new Color(0, 0, 0, 0);
        //                     inventoryItem.itemImage.color = colorAlpha;
        //                     inventoryItem.itemSO = null;
        //                     inventoryItem.cardName = "";
        //                 }
        //                 i++;
        //             }
        //             break;
        //         }
        //     case Characters.CHARACTER2:
        //         {
        //             int i = 0;
        //             foreach (var itemSlot in characterSlots)
        //             {
        //                 InventoryItem inventoryItem = itemSlot;
        //                 if (char2.Count > i)
        //                 {
        //                     inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
        //                     inventoryItem.itemImage.sprite = char2[i].sprite;
        //                     inventoryItem.itemSO = char2[i];
        //                     inventoryItem.cardName = char2[i].cardName;
        //                 }
        //                 else
        //                 {
        //                     Color colorAlpha = new Color(0, 0, 0, 0);
        //                     inventoryItem.itemImage.color = colorAlpha;
        //                     inventoryItem.itemSO = null;
        //                     inventoryItem.cardName = "";
        //                 }
        //                 i++;
        //             }
        //             break;
        //         }
        //     case Characters.CHARACTER3:
        //         {
        //             int i = 0;
        //             foreach (var itemSlot in characterSlots)
        //             {
        //                 InventoryItem inventoryItem = itemSlot;
        //                 if (char3.Count > i)
        //                 {
        //                     inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
        //                     inventoryItem.itemImage.sprite = char3[i].sprite;
        //                     inventoryItem.itemSO = char3[i];
        //                     inventoryItem.cardName = char3[i].cardName;
        //                 }
        //                 else
        //                 {
        //                     Color colorAlpha = new Color(0, 0, 0, 0);
        //                     inventoryItem.itemImage.color = colorAlpha;
        //                     inventoryItem.itemSO = null;
        //                     inventoryItem.cardName = "";
        //                 }
        //                 i++;
        //             }
        //             break;
        //         }
        // }
        //charactersModels[buttonIndex].SetActive(true);
    }
    private void SetCharacterSlotFunction(){
        foreach (InventoryItem item in characterSlots){
            item.button.onClick.AddListener(() => CharacterSlotClick(item));
        }
    }
    private void CharacterSlotClick(InventoryItem slot){
        //AudioManager.audioManager.PlaySoundEffect("InventoryEffects/SelectSlot");
        if (!itemView.activeInHierarchy){
            itemView.SetActive(true);
        }
        else{
            InventoryItem item = slot;
            if (item.itemImage.sprite != null){
                characters[(int)currentCharacter].Remove(item.itemSO);
                InventoryItem itemRef = null;
                for (int i = 0; i < itensButtons.Count; i++){
                    if (itensButtons[i].itemName == item.itemName){
                        itemRef = itensButtons[i];
                        break;
                    }
                }
                itemRef?.gameObject.SetActive(true);
                item.itemImage.color = new Color(0, 0, 0, 0);
                // switch (currentCharacter)
                // {
                //     case Characters.CHARACTER1:
                //         // if ((char1.Count - 1) > 0)
                //         // {
                //         char1.Remove(item.itemSO);
                //         InventoryItem item1 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == item.itemName)
                //             {
                //                 item1 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item1?.gameObject.SetActive(true);
                //         // item.itemImage.sprite = null;
                //         item.itemImage.color = new Color(0, 0, 0, 0);
                //         // }
                //         // else
                //         // {
                //         //     ShowAlert();
                //         // }
                //         break;
                //     case Characters.CHARACTER2:
                //         // if ((char2.Count - 1) > 0)
                //         // {
                //         char2.Remove(item.itemSO);
                //         InventoryItem item2 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == item.itemName)
                //             {
                //                 item2 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item2?.gameObject.SetActive(true);
                //         // item.itemImage.sprite = null;
                //         item.itemImage.color = new Color(0, 0, 0, 0);
                //         // }
                //         // else
                //         // {
                //         //     ShowAlert();
                //         // }
                //         break;
                //     case Characters.CHARACTER3:
                //         // if ((char3.Count - 1) > 0)
                //         // {
                //         char3.Remove(item.itemSO);
                //         InventoryItem item3 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == item.itemName)
                //             {
                //                 item3 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item3?.gameObject.SetActive(true);
                //         // item.itemImage.sprite = null;
                //         item.itemImage.color = new Color(0, 0, 0, 0);
                //         // }
                //         // else
                //         // {
                //         //     ShowAlert();
                //         // }
                //         break;
                //     default: break;
                // }
            }
            FillCards();
        }
        currentSlot = slot;
    }
    private void SetDeckToggleFunction(){
        deckToggle.onValueChanged.AddListener( (bool toggleValue) => { DeckToggleClick(toggleValue); });
    }
    private void DeckToggleClick(bool toggleValue){
        deckView.SetActive(toggleValue);
    }
    private void SetItemButtonFunction(){
        foreach (var itemBtn in itensButtons){
            // Debug.Log(itemBtn);
            itemBtn.button.onClick.AddListener(() => ItemButtonClick(itemBtn));
        }
    }
    private void ItemButtonClick(InventoryItem refItem){
        //AudioManager.audioManager.PlaySoundEffect("InventoryEffects/PlaceItem");
        if (currentSlot){
            HideCardsPreview();
            InventoryItem inventoryItem = currentSlot;
            if (inventoryItem.itemImage.sprite != null){
                characters[(int)currentCharacter].Remove(inventoryItem.itemSO);
                InventoryItem itemRef = null;
                for (int i = 0; i < itensButtons.Count; i++){
                    if (itensButtons[i].itemName == inventoryItem.itemName){
                        itemRef = itensButtons[i];
                        break;
                    }
                }
                itemRef?.gameObject.SetActive(true);
                // switch (currentCharacter)
                // {
                //     case Characters.CHARACTER1:
                //         char1.Remove(inventoryItem.itemSO);
                //         InventoryItem item1 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == inventoryItem.itemName)
                //             {
                //                 item1 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item1?.gameObject.SetActive(true);
                //         //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                //         inventoryItem.itemImage.color = colorAlpha;
                //         break;
                //     case Characters.CHARACTER2:
                //         char2.Remove(inventoryItem.itemSO);
                //         InventoryItem item2 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == inventoryItem.itemName)
                //             {
                //                 item2 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item2?.gameObject.SetActive(true);
                //         //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                //         inventoryItem.itemImage.color = colorAlpha;
                //         break;
                //     case Characters.CHARACTER3:
                //         char3.Remove(inventoryItem.itemSO);
                //         InventoryItem item3 = null;
                //         for (int i = 0; i < itensButtons.Count; i++)
                //         {
                //             if (itensButtons[i].itemName == inventoryItem.itemName)
                //             {
                //                 item3 = itensButtons[i];
                //                 break;
                //             }
                //         }
                //         item3?.gameObject.SetActive(true);
                //         //itensButtons.Find(i => i.GetComponent<InventoryItem>().cardName == inventoryItem.GetComponent<InventoryItem>().cardName).gameObject.SetActive(true);
                //         inventoryItem.itemImage.color = colorAlpha;
                //         break;
                //     default: break;
                // }
                inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
            }
            inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
            inventoryItem.itemSO = refItem.itemSO;
            inventoryItem.itemName = refItem.itemName;
            refItem.gameObject.SetActive(false);

            characters[(int)currentCharacter].Add(inventoryItem.itemSO);
            // switch (currentCharacter)
            // {
            //     case Characters.CHARACTER1: char1.Add(inventoryItem.itemSO); break;
            //     case Characters.CHARACTER2: char2.Add(inventoryItem.itemSO); break;
            //     case Characters.CHARACTER3: char3.Add(inventoryItem.itemSO); break;
            //     default: break;
            // }

            Storage.SaveInventory(chest, characters[0], characters[2], characters[3]);
            FillCards();
        }
    }
    public void ShowCardsPreview(ItemScriptableObject item){
        cardsPreview.SetActive(true);
        foreach (var cardObj in item.cards){
            InventoryCard card = Instantiate(cardPrefab, cardsPreview.transform.GetChild(0));
            card.Setup(cardObj);
        }
    }
    public void HideCardsPreview(){
        cardsPreview.SetActive(false);
        for (int i = 0; i < cardsPreview.transform.GetChild(0).childCount; i++){
            Destroy(cardsPreview.transform.GetChild(0).GetChild(i).gameObject);
        }
    }
    #endregion

    private void FillChest(){
        foreach (var i in chest){
            InventoryItem item = Instantiate(itemPrefab, itemView.transform.GetChild(0).GetChild(0));
            itensButtons.Add(item);
            item.itemImage.sprite = i.sprite;
            item.itemSO = i;
            item.itemName = i.cardName;
            
            if (characters[0].Contains(i) || characters[1].Contains(i) || characters[2].Contains(i)){
                item.gameObject.SetActive(false);
            }
        }
    }
    private void FillCards(){
        for (int i = 0; i < deckView.transform.GetChild(0).GetChild(0).childCount; i++){
            Destroy(deckView.transform.GetChild(0).GetChild(0).GetChild(i).gameObject);
        }
        foreach (var character in characters){
            foreach (var i in character){
                foreach (var j in i.cards){
                    InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
                    card.Setup(j);
                }
            }
        }
        // foreach (var i in char1)
        // {
        //     foreach (var j in i.cards)
        //     {
        //         InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
        //         card.Setup(j);
        //     }
        // }
        // foreach (var i in char2)
        // {
        //     foreach (var j in i.cards)
        //     {
        //         InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
        //         card.Setup(j);
        //     }
        // }
        // foreach (var i in char3)
        // {
        //     foreach (var j in i.cards)
        //     {
        //         InventoryCard card = Instantiate(cardPrefab, deckView.transform.GetChild(0).GetChild(0));
        //         card.Setup(j);
        //     }
        // }
    }

    private void ShowAlert(){
        StartCoroutine(ShowDelay());
        IEnumerator ShowDelay(){
            alert.SetActive(true);
            yield return new WaitForSeconds(3);
            alert.SetActive(false);
        }
    }

    private void ScreensSetActive(bool value){
        deckView.SetActive(value);
        itemView.SetActive(value);
        characterSlot.SetActive(value);
    }
}
