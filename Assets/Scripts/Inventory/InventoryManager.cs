using System.Collections;
using System.Collections.Generic;
using Inventory;
using System.Linq;
using Combat;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public InventoryData inventoryData;//All itens data

    public void Awake(){
        if(instance == null) instance = this;
        else { Destroy(instance.gameObject); instance = this; }
        DontDestroyOnLoad(instance);

        inventoryData = InventoryData.CreateInstance();
    }
    
}
