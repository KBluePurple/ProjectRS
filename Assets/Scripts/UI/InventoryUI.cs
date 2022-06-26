using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoSingleton<InventoryUI>
{
    Dictionary<int, ListItem> listItems = new Dictionary<int, ListItem>();
    [SerializeField] Transform contentParant;

    public void AddItem(Coin coin, int count)
    {
        if (listItems.ContainsKey(coin.ID))
        {
            listItems[coin.ID].Count += count;
        }
        else
        {
            ListItem item = PoolManager<ListItem>.Get(contentParant);
            item.IconImage.sprite = coin.Icon;
            item.NameText.text = coin.Name;
            item.Price = coin.Price;
            item.Count = count;

            listItems.Add(coin.ID, item);
        }
    }

    public void RemoveItem(Coin coin, int count)
    {
        if (listItems.ContainsKey(coin.ID))
        {
            listItems[coin.ID].Count -= count;
            if (listItems[coin.ID].Count <= 0)
            {
                PoolManager<ListItem>.Release(listItems[coin.ID]);
                listItems.Remove(coin.ID);
            }
        }
    }

    public void Clear()
    {
        foreach (var item in listItems)
        {
            PoolManager<ListItem>.Release(item.Value);
        }
        listItems.Clear();
    }

    public void UpdateItem(Coin coin, int count)
    {
        if (listItems.ContainsKey(coin.ID))
        {
            listItems[coin.ID].Count = count;
        }
    }
}