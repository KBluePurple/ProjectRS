using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListItem : MonoBehaviour, IPoolable
{
    public Image IconImage;
    public TMP_Text NameText;
    public Button Button;
    public Coin Coin => coin;

    public TMP_Text PriceText;
    public TMP_Text CountText;

    private int count = 0;
    private float price = 0;
    private Coin coin;

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
            CountText.text = count.ToString();
        }
    }

    public float Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
            PriceText.text = price.ToString();
        }
    }

    public void SetCoin(Coin coin)
    {
        this.coin = coin;
        IconImage.sprite = coin.Icon;
        NameText.text = coin.Name;
        Price = coin.Price;
    }

    public void Initialize()
    {
        IconImage.sprite = null;
        NameText.text = "";
        PriceText.text = "";
        CountText.text = "";
        Button.onClick.RemoveAllListeners();
    }
}
