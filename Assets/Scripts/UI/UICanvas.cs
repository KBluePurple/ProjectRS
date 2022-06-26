using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoSingleton<UICanvas>
{
    public static Icon BackpackIcon;
    public static Vector2 BackpackIconPosition => (BackpackIcon.transform as RectTransform).anchoredPosition;
    public static Canvas Canvas;

    public TMPro.TMP_Text MoneyText;

    [SerializeField] Icon backpackIcon;
    [SerializeField] ListItem selectedListItem = null;

    internal void UpdateMoney()
    {
        MoneyText.text = $"총 자산: {Inventory.Money}$";
    }

    [SerializeField] Sprite nullSprite = null;

    private new Animation animation = null;
    private bool isInventoryOpen = false;

    private void Awake()
    {
        Canvas = GetComponent<Canvas>();
        BackpackIcon = backpackIcon;
    }

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SelectItem(ListItem listItem)
    {
        selectedListItem.SetCoin(listItem.Coin);
        selectedListItem.Count = 1;
    }

    private void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            animation.Play("InventoryHide");
        }
        else
        {
            animation.Play("InventoryShow");
            backpackIcon.Hide();
        }
        isInventoryOpen = !isInventoryOpen;
    }

    public void OnSellCoinButtonDown()
    {
        if (Inventory.GetCount(selectedListItem.Coin) > 0)
        {
            Inventory.RemoveCoin(selectedListItem.Coin);
            Inventory.Money += selectedListItem.Coin.Price;
        }

        if (Inventory.GetCount(selectedListItem.Coin) <= 0)
        {
            Debug.Log("Not enough coins");
            selectedListItem.IconImage.sprite = nullSprite;
            selectedListItem.NameText.text = "";
            selectedListItem.PriceText.text = "";
            selectedListItem.CountText.text = "";
        }
    }
}
