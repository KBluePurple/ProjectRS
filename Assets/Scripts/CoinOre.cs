using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class CoinOre : MonoBehaviour
{
    [SerializeField] public Slider FillSlider;
    [SerializeField] List<MeshRenderer> sculptures = new List<MeshRenderer>();
    [SerializeField] TMPro.TMP_Text nameText;
    [SerializeField] TMPro.TMP_Text priceText;

    [SerializeField] int count = 0;
    [SerializeField] float health = 0;

    private Image image; // 아이콘
    private Image fillImage; // 색상 변경용
    private Coin coin; // 코인 정보

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count <= 0)
            {
                Destroy(gameObject);
            }
            UpdateHUD();
        }
    }

    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health <= 0)
            {
                Count--;
                health = coin.Price;
            }
            UpdateHUD();
        }
    }

    public Coin Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
            count = Random.Range(1, 10);
            coin.GetIcon((coin, icon) =>
            {
                for (int i = 0; i < coin.Colors.Length; i++)
                {
                    Debug.DrawRay(transform.position + new Vector3(0.005f * i, 0, 0), transform.up * 100, coin.Colors[i], 100);
                }

                Debug.Log($"Color count: {coin.Colors.Length}");

                if (icon != null)
                {
                    for (int i = 0; i < sculptures.Count; i++)
                    {
                        Color color = coin.Colors[i % coin.Colors.Length];
                        sculptures[i].material.SetColor("_BaseColor", color);
                        sculptures[i].material.SetColor("_1st_ShadeColor", color - new Color(0.1f, 0.1f, 0.1f));
                        sculptures[i].material.SetColor("_Emissive_Color", coin.Colors[0] * 0.5f);
                    }
                    image.sprite = icon;
                }
                FillSlider.maxValue = coin.Price;
                health = coin.Price;
                FillSlider.value = 0;
                fillImage.SetAlpha(0.2f);
                UpdateHUD();
            });
        }
    }

    void Start()
    {
        image = GetComponentInChildren<Image>();
        fillImage = FillSlider.fillRect.GetComponent<Image>();

        Coin = CoinData.List[Random.Range(0, CoinData.List.Count)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Coin = CoinData.List[Random.Range(0, CoinData.List.Count)];
        }
    }

    private void UpdateHUD()
    {
        nameText.text = $"<font-weight=100><font-weight=300>{coin.Name}</font-weight> ({coin.Symbol}) x{count}</font-weight>";
        priceText.text = $"{coin.Price.ToString("F")}$";
        nameText.color = coin.Colors[0];
        priceText.color = coin.Colors[0];
        fillImage.color = coin.Colors[0];
        fillImage.SetAlpha(0.2f);
        FillSlider.DOValue(coin.Price - health, 0.5f);
    }
}
