using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0, y);
        transform.Translate(direction * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, mouseX, 0));
        cameraTransform.Rotate(new Vector3(-mouseY, 0, 0));
    }
}
