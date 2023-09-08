using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TweenUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 targetScale;

    private Vector3 originalPos;
    private Vector3 originalScale;

    [SerializeField] private float speed;

    void Start()
    {
       this.originalPos = this.GetComponent<RectTransform>().localPosition;
       this.originalScale = this.GetComponent<RectTransform>().localScale;
    }

    public void MoveTween()
    {
        this.transform.DOLocalMove(targetPos, speed).SetEase(Ease.OutQuad).OnComplete(ReturnPos);
    }

    public void ReturnPos()
    {
        this.transform.DOLocalMove(originalPos, speed).SetEase(Ease.OutBounce);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.DOScale(originalScale, speed/2f).SetEase(Ease.InSine); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.DOScale(targetScale, speed/2f).SetEase(Ease.OutSine);
    }
}
