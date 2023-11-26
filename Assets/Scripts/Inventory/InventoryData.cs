using System.Collections.Generic;
using Combat;
using UnityEngine;
using Inventory;

public class InventoryData : ScriptableObject{
    public List<ItemScriptableObject> inventoryData;
    public List<ItemScriptableObject> inventory;

    public bool TryInitInventory(out List<ItemScriptableObject> _inventory){
        bool success = false;
        if(Storage.TryLoadInventory(out var _items)) success = true;
        else success = false;
        _inventory = _items;
        return success;
    }
    public static InventoryData CreateInstance(){
        InventoryData inventoryDataInstance = CreateInstance<InventoryData>();
        if(Storage.TryLoadAllItems(out var _inventoryData)) inventoryDataInstance.inventoryData = _inventoryData;
        inventoryDataInstance.TryInitInventory(out var _inventory);
        inventoryDataInstance.inventory = _inventory;
        return inventoryDataInstance;
    }
    public bool TryGetItemDataByName(string itemName, out ItemScriptableObject item){
        item = new ItemScriptableObject();
        bool find = false;
        for (int i = 0; i < inventoryData.Count; i++){
            if(inventoryData[i].cardName == itemName) {item = inventoryData[i]; find = true;}
        }
        return find;
    }
    public bool TryGetRandomItemByRarity(E_ItemRarity rarity, out ItemScriptableObject item){
        item = new ItemScriptableObject();
        bool find = false;
        List<ItemScriptableObject> items = inventoryData.FindAll((x) => x.rarity == rarity);
        foreach (var _item in inventory){
            if(items.Contains(_item)) items.Remove(_item);
        }
        if(items.Count > 0){
            int random_item_index = Random.Range(0, items.Count);
            item = items[random_item_index];
            find = true;
        }
        return find;
    }
    public bool TryGetRandomItemPerProbability(out ItemScriptableObject item){
        item = new ItemScriptableObject();
        bool find = false;
        E_ItemRarity random_rarity = GetRandomRarity();
        if(TryGetRandomItemByRarity(random_rarity, out ItemScriptableObject _item)) {
            item = _item;
            find = true;
        }
        return find;
    }
    private E_ItemRarity GetRandomRarity(){
        float probability = Random.value;
        if(probability < 0.6f){
            return E_ItemRarity.COMMOM;
        }
        else if(probability < 0.8f){
            return E_ItemRarity.RARE;
        }
        else if(probability < 0.95f){
            return E_ItemRarity.EPIC;
        }
        else{
            return E_ItemRarity.LENGENDARY;
        }
    }
    public void AddItemToInventory(ItemScriptableObject item){
        inventory.Add(item);
    }

    public void DeleteArchive()
    {
        Storage.DeleteInventory();
    }
}

