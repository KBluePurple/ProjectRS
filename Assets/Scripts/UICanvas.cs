using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] Icon backpackIcon;

    private new Animation animation = null;
    private bool isInventoryOpen = false;

    private void Start()
    {
        animation = GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            animation.Play("InventoryHide");
        }
        else
        {
            animation.Play("InventoryShow");
            backpackIcon.Hide();
        }
        isInventoryOpen = !isInventoryOpen;
    }
}
