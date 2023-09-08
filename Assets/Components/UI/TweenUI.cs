using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.ComponentModel;
using DG.Tweening.Core;

public class TweenUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private Vector3 targetScale;
    [SerializeField] private Vector3 targetRotation;

    private Vector3 originalPos;
    private Vector3 originalScale;

    [SerializeField]
    private Image image;

    [SerializeField] private float speed;

    void Start()
    {
        this.originalPos = this.GetComponent<RectTransform>().localPosition;
        this.originalScale = this.GetComponent<RectTransform>().localScale;
            
        this.image = this.GetComponent<Image>();
    }

    public void SequenceTween()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(this.image.DOFade(0, speed).SetEase(Ease.OutSine).OnComplete(ResetAppearance));
        sequence.Append(this.transform.DOLocalMove(targetPos, speed).SetEase(Ease.OutQuad));
        sequence.AppendInterval(speed / 4f);
        sequence.Append(this.transform.DOLocalRotate(this.targetRotation, speed));
        sequence.Append(this.transform.DOScale(Vector3.zero, speed / 2f).SetEase(Ease.InSine));
        sequence.AppendInterval(speed / 4f);
        sequence.Append(this.transform.DOLocalRotate(Vector3.zero, speed));
    }

    public void MoveTween()
    {
        this.transform.DOLocalMove(targetPos, speed).SetEase(Ease.OutQuad).OnComplete(ResetAppearance);
        this.transform.DOLocalRotate(Vector3.zero, speed);
        this.Fade();
    }

    public void ResetAppearance()
    {
        this.transform.DOLocalMove(originalPos, speed).SetEase(Ease.OutBounce);
        this.image.DOFade(1, speed / 2f).SetEase(Ease.InSine);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.DOScale(originalScale, speed / 2f).SetEase(Ease.InSine);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.DOScale(targetScale, speed / 2f).SetEase(Ease.OutSine);
    }


    public void Fade()
    {
        this.image.DOFade(0, speed / 2f).SetEase(Ease.OutSine).OnComplete(ResetAppearance);
    }

    public void Rotate()
    {
        this.transform.DOLocalRotate(new Vector3(0, 0, 180f), .2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }
}
