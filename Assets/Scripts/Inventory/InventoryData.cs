using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;


public class InventoryData{
    public List<ItemScriptableObject> itens_Datas;

    public ItemScriptableObject get_inventory_data(string item_name){
        for (int i = 0; i < itens_Datas.Count; i++)
        {
            if(itens_Datas[i].cardName == item_name) return itens_Datas[i];
        }
        return new ItemScriptableObject();
    }
}



