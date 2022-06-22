using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static PlayerData Data { get; private set; } = new PlayerData();
    public static bool IsSlowed = false;
    public static bool IsPicking = false;
    [SerializeField] Pickaxe pickaxe;
    private Animator pickaxeAnimation = null;

    private void Start()
    {
        pickaxeAnimation = pickaxe.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log($"MousePosition: {Input.mousePosition}");
    }
    
    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.None) return;

        if (Input.GetMouseButton(0) && !IsPicking)
        {
            pickaxeAnimation.Play("PickNew");
            IsPicking = true;
            IsSlowed = true;
        }
    }
}
