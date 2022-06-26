using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class CoinOre : MonoBehaviour, IPoolable
{
    public Material Material { get; private set; }

    [SerializeField] public Slider FillSlider;
    [SerializeField] List<MeshRenderer> sculptures = new List<MeshRenderer>();
    [SerializeField] TMPro.TMP_Text nameText;
    [SerializeField] TMPro.TMP_Text priceText;

    [SerializeField] int count = 0;
    [SerializeField] float health = 0;

    public Image IconImage; // 아이콘
    private Image fillImage; // 색상 변경용
    private Coin coin; // 코인 정보

    List<Tween> tweens = new List<Tween>();

    public int Count
    {
        get { return count; }
        set
        {
            count = value;
            if (count <= 0)
            {
                DestroyOre();
            }
            UpdateHUD();
        }
    }

    private void DestroyOre()
    {
        PoolManager<CoinOre>.Release(this);
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
                health = coin.Quotes[0].Price;
                FillSlider.value = health;
                var particle = PoolManager<GetParticle>.Get(UICanvas.Canvas.transform);
                particle.SetSprite(Coin.Icon)
                    .StartEffect(IconImage.transform.position, (UICanvas.BackpackIcon.transform as RectTransform).position, 2f);
                Inventory.Add(coin, 1);
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
                    IconImage.sprite = icon;
                }
                FillSlider.maxValue = coin.Quotes[0].Price;
                health = coin.Quotes[0].Price;
                FillSlider.value = 0;
                fillImage.SetAlpha(0.2f);
                UpdateHUD(false);
            });
        }
    }

    public void Initialize()
    {
        Coin = CoinData.List[Random.Range(0, CoinData.List.Count)];
    }

    void Start()
    {
        IconImage = GetComponentInChildren<Image>();
        fillImage = FillSlider.fillRect.GetComponent<Image>();
        Material = GetComponentInChildren<MeshRenderer>().material;
        Initialize();
    }

    public void Blink()
    {
        float time = 0.075f;
        tweens.Add(Material.DOColor(Color.white, "_BaseColor", time).SetLoops(2, LoopType.Yoyo));
        tweens.Add(Material.DOColor(Color.white, "_1st_ShadeColor", time).SetLoops(2, LoopType.Yoyo));

        for (int i = 0; i < sculptures.Count; i++)
        {
            tweens.Add(sculptures[i].material.DOColor(Color.white, "_BaseColor", time).SetLoops(2, LoopType.Yoyo));
            tweens.Add(sculptures[i].material.DOColor(Color.white, "_1st_ShadeColor", time).SetLoops(2, LoopType.Yoyo));
        }

        tweens.Add(fillImage.DOColor(new Color(1, 1, 1, 0.5f), time).SetLoops(2, LoopType.Yoyo));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Coin = CoinData.List[Random.Range(0, CoinData.List.Count)];
        }

        for (int i = 0; i < tweens.Count; i++)
        {
            if (!tweens[i].IsActive())
            {
                tweens.RemoveAt(i);
                i--;
            }
        }
    }

    private void UpdateHUD(bool isAnimation = true)
    {
        nameText.text = $"<font-weight=100><font-weight=300>{coin.Name}</font-weight> ({coin.Symbol}) x{count}</font-weight>";
        priceText.text = $"{string.Format("{0:#,0.000}", coin.Quotes[0].Price)}$";
        nameText.color = coin.Colors[0];
        priceText.color = coin.Colors[0];
        fillImage.color = coin.Colors[0];
        fillImage.SetAlpha(0.2f);

        if (isAnimation) tweens.Add(FillSlider.DOValue(coin.Quotes[0].Price - health, 0.5f));
        else FillSlider.value = coin.Quotes[0].Price - health;
    }
}
