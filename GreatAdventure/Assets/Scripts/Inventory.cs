using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    private static Dictionary<string, Item> m_items = new Dictionary<string, Item>();
    public static void AddItem(string itemName, Item item)
    {
        m_items.Add(itemName, item);
    }

    public static void RemoveItem(string itemName)
    {
        m_items.Remove(itemName);
    }

    public static Item GetItemFromName(string itemName)
    {
        Item item;
        m_items.TryGetValue(itemName, out item);
        return item;
    }

    public static bool HasItem(string ItemName)
    {
        return m_items.ContainsKey(ItemName);
    }

    public static Dictionary<string, Item> GetCurrentItems()
    {
        return m_items;
    }
}
