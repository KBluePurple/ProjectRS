using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int CoinID = 0;
    public int Count = 0;
}

public static class Inventory
{
    private static List<InventoryItem> items = new List<InventoryItem>();

    public static void Add(InventoryItem item)
    {
        if (item.CoinID == 0)
            return;

        InventoryItem existingItem = items.Find(i => i.CoinID == item.CoinID);
        if (existingItem != null)
        {
            existingItem.Count += item.Count;
        }
        else
        {
            items.Add(item);
        }
    }

    public static void Remove(InventoryItem item)
    {
        if (item.CoinID == 0)
            return;

        InventoryItem existingItem = items.Find(i => i.CoinID == item.CoinID);
        if (existingItem != null)
        {
            existingItem.Count -= item.Count;
            if (existingItem.Count <= 0)
                items.Remove(existingItem);
        }
    }
}