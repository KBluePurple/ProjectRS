using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    [SerializeField] Transform particleTransform;
    private ParticleSystem particles;
    // private ParticleSystem

    private void Start()
    {
        particles = particleTransform.GetComponent<ParticleSystem>();
    }

    public void OnPick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            CoinOre coinOre = hit.transform.GetComponent<CoinOre>();
            if (coinOre != null)
            {
                coinOre.Health -= Player.Data.MiningPower;
                Color color = coinOre.Coin.Colors[0] + new Color(0.1f, 0.1f, 0.1f);
                particleTransform.position = hit.point;
                particles.Play();
                Debug.Log($"{coinOre.name}의 체력: {coinOre.Health}");
            }
        }
        PlayerMove.IsPicking = false;
    }
}
