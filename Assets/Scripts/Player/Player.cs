using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static PlayerData Data { get; private set; } = new PlayerData();
    [SerializeField] Pickaxe pickaxe;
    private Animation pickaxeAnimation = null;

    private void Start()
    {
        pickaxeAnimation = pickaxe.GetComponent<Animation>();
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log($"MousePosition: {Input.mousePosition}");
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pickaxeAnimation.Play("PickNew");
            PlayerMove.IsPicking = true;
        }
    }
}
