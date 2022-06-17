using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static PlayerData Data { get; private set; } = new PlayerData();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log($"MousePosition: {Input.mousePosition}");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                CoinOre coinOre = hit.transform.GetComponent<CoinOre>();
                if (coinOre != null)
                {
                    coinOre.Health -= Data.MiningPower;
                    Debug.Log($"{coinOre.name}의 체력: {coinOre.Health}");
                }
            }
        }
    }
}
