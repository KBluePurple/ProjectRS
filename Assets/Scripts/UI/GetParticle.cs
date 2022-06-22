using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections;

public class GetParticle : MonoBehaviour, IPoolable
{
    private Image image;

    private void Awake()
    {
        // Initialize();
    }

    public GetParticle SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        image.color = Color.white;
        return this;
    }

    public void StartEffect(Vector3 from, Vector2 to, float duration)
    {
        image.rectTransform.localScale = Vector2.one;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(from);
        UICanvas.BackpackIcon.Show();
        image.enabled = true;
        StartCoroutine(effectCorutine(transform.parent.InverseTransformPoint(screenPos), to, duration));
    }

    private IEnumerator effectCorutine(Vector2 from, Vector2 to, float duration)
    {
        duration /= 2;
        image.rectTransform.anchoredPosition = from;
        image.rectTransform.DOAnchorPos(from + UnityEngine.Random.insideUnitCircle * 500, duration);
        yield return new WaitForSeconds(duration / 4);
        image.rectTransform.DOMove(to, duration);
        image.DOFade(0, duration);
        yield return new WaitForSeconds(duration);
        if (PoolManager<GetParticle>.TotalCount - PoolManager<GetParticle>.Count <= 1)
        {
            UICanvas.BackpackIcon.Hide();
        }
        PoolManager<GetParticle>.Put(this);
    }

    public void Initialize()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }
}