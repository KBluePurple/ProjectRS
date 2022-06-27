using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static bool IsPicking = false;
    [SerializeField] float speed = 10f;
    private Transform cameraTransform;
    private CharacterController controller;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        controller = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.None) return;

        float modify = Player.IsSlowed ? 0.1f : 1;
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, y);
        move = transform.TransformDirection(move);
        move *= speed * modify;
        controller.SimpleMove(move);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, mouseX * modify, 0));
        cameraTransform.Rotate(new Vector3(-mouseY * modify, 0, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.isGrounded)
            {
                controller.Move(Vector3.up * 0.1f);
            }
        }

        if (!controller.isGrounded)
        {
            Vector3 gravity = new Vector3(0, -9.81f, 0);
            controller.Move(gravity * Time.deltaTime);
        }
    }
}
