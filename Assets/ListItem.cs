using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListItem : MonoBehaviour, IPoolable
{
    public Image IconImage;
    public TMP_Text NameText;

    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text countText;

    private int count = 0;
    private float price = 0;

    public int Count
    {
        get
        {
            return count;
        }

        set
        {
            count = value;
            countText.text = count.ToString();
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
            priceText.text = price.ToString();
        }
    }

    public void Initialize()
    {
        IconImage.sprite = null;
        NameText.text = "";
        priceText.text = "";
        countText.text = "";
    }
}
