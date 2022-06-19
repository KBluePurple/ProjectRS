using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static bool IsPicking = false;
    [SerializeField] float speed = 10f;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    
    private void Update()
    {
        float modify = IsPicking ? 0.1f : 1;
        
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0, y);
        transform.Translate(direction * speed * modify * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, mouseX * modify, 0));
        cameraTransform.Rotate(new Vector3(-mouseY * modify, 0, 0));
    }
}
