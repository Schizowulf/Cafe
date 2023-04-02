using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class WaitressOnUi : MonoBehaviour
    {

        [SerializeField] private Canvas mainCanvas;

        private List<Waitress> _waitresses;

        public void Start()
        {
            _waitresses = FindAnyObjectByType<WaitressProvider>()._waitresses;
            GameObject parentCanvas = gameObject;

            for (int i = 0; i < _waitresses.Count; i++)
            {
                GameObject waitressUiObj = new GameObject("WaitressIcon_" + _waitresses[i], typeof(RectTransform), typeof(CanvasGroup));
                GameObject instantiatedObj = Instantiate(waitressUiObj, parentCanvas.transform);

                Image waitressUiImage = instantiatedObj.AddComponent<Image>();
                waitressUiImage.sprite = _waitresses[i]._uiSprite;

                WaitressDragAndDrop dragAndDropObj = instantiatedObj.AddComponent<WaitressDragAndDrop>();
                dragAndDropObj.assignedWaitress = _waitresses[i];
                dragAndDropObj.mainCanvas = mainCanvas;

                StartCoroutine(CoWaitForPosition(dragAndDropObj));
            }
        }

        private IEnumerator CoWaitForPosition(WaitressDragAndDrop obj)
        {
            yield return new WaitForEndOfFrame();

            obj.setDefaultPosition();
        }
    }
}