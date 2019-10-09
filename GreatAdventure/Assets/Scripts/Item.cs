using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    [Header("Item selected in inventory function")]
    public UnityEvent selectedInInventoryFunc;
    
    void Start()
    {
    }

    void Update()
    {
        
    }

    public void AddItemToInventory()
    {
        Inventory.AddItem(itemName, this);
    }

    public void RemoveItemFromInventory()
    {
        Inventory.RemoveItem(itemName);
    }

    public void ItemSelectedInInventory()
    {
        selectedInInventoryFunc.Invoke();
        Debug.Log(itemName + " selected");
    }
}
