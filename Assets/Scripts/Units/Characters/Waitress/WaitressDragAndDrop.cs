using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class WaitressDragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        private RectTransform rectTransform;
        private Vector2 defaultPosition;
        private CanvasGroup canvasGroup;

        public Waitress assignedWaitress { get; set; }
        public Canvas mainCanvas;

        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void setDefaultPosition()
        {
            defaultPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!PlayerData.dragActive)
            {
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {

            rectTransform.GetComponent<RectTransform>().anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
            PlayerData.dragActive = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            rectTransform.GetComponent<RectTransform>().anchoredPosition = defaultPosition;
            canvasGroup.blocksRaycasts = true;
            PlayerData.dragActive = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}