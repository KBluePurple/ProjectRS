using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int count = 10;
    [SerializeField] Transform oreParant = null;

    private void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var coinOre = PoolManager<CoinOre>.Get(oreParant);
            Vector2 randomPos = Random.insideUnitCircle;
            Vector3 pos = new Vector3(randomPos.x, 0, randomPos.y) * 70;
            coinOre.transform.localPosition = transform.position + pos;
        }
    }
}