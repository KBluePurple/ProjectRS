using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public int CoinID = 0;
    public int Count = 0;
}

public class Inventory : MonoBehaviour
{
    List<InventoryItem> items = new List<InventoryItem>();
}