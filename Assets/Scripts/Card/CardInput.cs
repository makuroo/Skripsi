using System;
using TMPro;

namespace Card
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class CardInput : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
    {
        [SerializeField] private float _maxZRotation;
        private Vector3 _initialPosition;
        private CardTemplate _cardTemplate;

        public static Action OnUsedCard;

        private void Awake()
        {
            _cardTemplate = GetComponent<CardTemplate>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);
            if (transform.localPosition.x < 0)
            {
                var color = _cardTemplate.LeftOptionText.color;
                color.a = Mathf.Abs(transform.localPosition.x) / 255;
                _cardTemplate.LeftOptionText.color = color;
            }
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
                if (transform.localPosition.x >= 500)
                {
                    _cardTemplate.ActivateRightStrategy();
                }
                else
                {
                    _cardTemplate.ActivateLeftStrategy();
                }
                OnUsedCard?.Invoke();
                Destroy(gameObject);
            }
        
            transform.localPosition = _initialPosition;
            transform.DOLocalRotate(Vector3.zero, .2f);
            
            ResetTextOptionColor(_cardTemplate.LeftOptionText);
        }

        private void ResetTextOptionColor(TMP_Text text)
        {
            var color = text.color;
            DOVirtual.Color(color, new Color(0, 0, 0,0), .2f,
                (value)=>text.color=value);
        }
    }
}

