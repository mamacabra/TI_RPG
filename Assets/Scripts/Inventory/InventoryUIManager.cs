using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Inventory;
using Combat;
using DG.Tweening;
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

    private void Start()
    {
        if (MapManager.Instance)
            MapManager.Instance.DisableLight();
        chest = InventoryManager.instance.inventoryData.inventory;

        var c = SaveDeath.Instance.CharacterSaveData;
        for (int i = 0; i < characters.Length; i++){
            characters[i] = new();
            
            if (!c[i].isDead){
                if (Storage.TryLoadHeroInventory(out List<ItemScriptableObject> _characterInventory, i)){
                    characters[i] = _characterInventory;
                }
            }
        }

        ScreensSetActive(false);
        itemView.SetActive(true);
        characterSlot.SetActive(true);
        SetCharacterTogglesFunction();
        SetDeckToggleFunction();
        SetCharacterSlotFunction();
        FillChest();
        SetItemButtonFunction();
        FillCards();
        CharacterToggleClick(0, true);
        if(string.IsNullOrEmpty(characterSlots[0].itemName))CharacterSlotClick(characterSlots[0]);

        for (int i = 0; i < c.Length; i++)
        {
            if (c[i].isDead)
            {
                charactersModels[i].SetActive(false);
                characterToggles[i].gameObject.SetActive(false);
            }
        }
    }

    public bool VerifyCards(){
        bool hasCards = false;
        var c = SaveDeath.Instance.CharacterSaveData;
        int i = 0;
        foreach (var character in characters){
            if(character.Count > 0 || c[i].isDead)
                hasCards = true;
            else{
                hasCards = false;
                break;
            }
            i++;
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
    }
    private void CharacterToggleClick(int index, bool toggleValue){
        if(toggleValue){
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
    }
    private void SetCharacterSlotFunction(){
        foreach (InventoryItem item in characterSlots){
            item.button.onClick.AddListener(() => CharacterSlotClick(item));
        }
    }
    private void CharacterSlotClick(InventoryItem slot){
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
            }
            Storage.SaveInventory(InventoryManager.instance.inventoryData.inventory, characters[0], characters[1], characters[2]);
            FillCards();
        }

        if (currentSlot != null)
        {
            DOTween.Kill(currentSlot.transform);
            currentSlot.transform.localScale = Vector3.one;
        }

        currentSlot = slot;
        currentSlot.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack);
    }
    private void SetDeckToggleFunction(){
        deckToggle.onValueChanged.AddListener( (bool toggleValue) => { DeckToggleClick(toggleValue); });
    }
    private void DeckToggleClick(bool toggleValue){
        deckView.SetActive(toggleValue);
    }
    private void SetItemButtonFunction(){
        foreach (var itemBtn in itensButtons){
            itemBtn.button.onClick.AddListener(() => ItemButtonClick(itemBtn));
        }
    }
    private void ItemButtonClick(InventoryItem refItem){
        if (currentSlot){
            
            DOTween.Kill(currentSlot.transform);
            currentSlot.transform.localScale = Vector3.one;
            currentSlot.transform.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutBack);
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
                inventoryItem.itemImage.color = new Color(1, 1, 1, 1);
            }
            inventoryItem.itemImage.sprite = refItem.itemImage.sprite;
            inventoryItem.itemSO = refItem.itemSO;
            inventoryItem.itemName = refItem.itemName;
            refItem.gameObject.SetActive(false);

            characters[(int)currentCharacter].Add(inventoryItem.itemSO);

            Storage.SaveInventory(InventoryManager.instance.inventoryData.inventory, characters[0], characters[1], characters[2]);
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
            InventoryItem item = Instantiate(itemPrefab, itemView.transform.GetChild(1).GetChild(0));
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
