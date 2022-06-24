using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int count = 10;
    [SerializeField] Transform oreParant = null;

    private void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            var coinOre = PoolManager<CoinOre>.Get();
            coinOre.transform.SetParent(oreParant);
            coinOre.transform.localPosition = new Vector3(Random.Range(0, 100), 0, Random.Range(0, 100));
        }
    }
}