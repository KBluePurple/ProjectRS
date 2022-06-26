using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int CoinID = 0;
    public int Count = 0;

    public InventoryItem(int coinID, int count)
    {
        CoinID = coinID;
        Count = count;
    }
}

public static class Inventory
{
    public static float Money 
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            UICanvas.Instance.UpdateMoney();
        }
    }

    private static List<InventoryItem> items = new List<InventoryItem>();
    private static float money = 0;

    public static void AddCoin(Coin coin, int count = 1)
    {
        if (coin == null)
            return;

        InventoryItem item = items.Find(x => x.CoinID == coin.ID);
        if (item == null)
        {
            item = new InventoryItem(coin.ID, count);
            items.Add(item);
        }
        else
        {
            item.Count += count;
        }
        Debug.Log($"Added {coin.Name} x{count}");

        InventoryUI.Instance.AddItem(CoinData.GetCoin(item.CoinID), count);
    }

    public static void RemoveCoin(Coin coin, int count = 1)
    {
        if (coin == null)
            return;

        InventoryItem item = items.Find(x => x.CoinID == coin.ID);
        if (item == null)
        {
            return;
        }
        else
        {
            item.Count -= count;
            if (item.Count <= 0)
            {
                items.Remove(item);
            }
        }

        InventoryUI.Instance.RemoveItem(CoinData.GetCoin(item.CoinID), count);
    }

    public static int GetCount(Coin coin)
    {
        if (coin == null)
            return 0;

        InventoryItem item = items.Find(x => x.CoinID == coin.ID);
        if (item == null)
        {
            return 0;
        }
        else
        {
            return item.Count;
        }
    }
}