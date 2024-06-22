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
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);
            var zRot = (0-transform.localPosition.x) * _maxZRotation;
        
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
                    _cardTemplate.ActivateLeftStrategy();
                }
                else
                {
                    _cardTemplate.ActivateRightStrategy();
                }
                Destroy(gameObject);
            }
        
            transform.localPosition = _initialPosition;
            transform.DOLocalRotate(Vector3.zero, .2f);
        }
    }
}

