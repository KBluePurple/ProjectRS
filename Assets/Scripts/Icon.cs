using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Icon : MonoBehaviour
{
    [SerializeField] float animationDelay;

    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }

    public void Show()
    {
        iconImage.DOFade(1f, animationDelay);
    }

    public void Hide()
    {
        iconImage.DOFade(0f, animationDelay);
    }
}
