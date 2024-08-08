using System;
using TMPro;

namespace Card
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CardSwipe : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] 
        private float _maxZRotation;
        private Vector3 _initialPosition;
        private CardTemplate _cardTemplate;

        public static Action<bool> OnUsedCard;

        private void Awake()
        {
            _cardTemplate = GetComponent<CardTemplate>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);
            
            OptionTextFade(transform.localPosition.x > 0
                ? _cardTemplate.LeftOptionText
                : _cardTemplate.RightOptionText);
            
            var zRot = (-transform.localPosition.x/500) * _maxZRotation;
        
            transform.DOLocalRotate(new Vector3(0, 0, Mathf.Clamp(zRot, -_maxZRotation, _maxZRotation)), .2f);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _initialPosition = transform.localPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (transform.localPosition.x is >= 500 or <= -500)
            {
                OnUsedCard?.Invoke(transform.localPosition.x >= 500);
            }
            ResetTextOptionColor(transform.localPosition.x > 0
                ? _cardTemplate.LeftOptionText
                : _cardTemplate.RightOptionText);
            transform.localPosition = _initialPosition;
            transform.DOLocalRotate(Vector3.zero, .2f);
        }

        private void ResetTextOptionColor(TMP_Text text)
        {
            text.color = new Color(0, 0, 0, 0);
        }

        private void OptionTextFade(TMP_Text text)
        {
            var color = text.color;
            color.a = Mathf.Abs(transform.localPosition.x) / 255;
            text.color = color;
        }
    }
}

