using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHider : MonoBehaviour
{
    [SerializeField] float maxViewDistance = 10f;
    [SerializeField] float minViewDistance = 1f;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        if (distance < maxViewDistance - 1)
            canvasGroup.alpha = Mathf.Clamp01(distance - 1 / (minViewDistance));
        else
            canvasGroup.alpha = Mathf.Clamp01(1 - (distance - maxViewDistance) / (maxViewDistance - minViewDistance));
    }
}
