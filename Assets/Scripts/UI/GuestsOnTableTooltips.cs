using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class GuestsOnTableTooltips : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI favWaitress;
        [SerializeField] private TextMeshProUGUI guestsName;
        [SerializeField] private GameObject tooltipCanvas;

        public static GuestsOnTableTooltips _instance;

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this);
            }
            else
            {
                _instance = this;
            }
        }

        private void Start()
        {
            Cursor.visible = true;
            tooltipCanvas.SetActive(false);
        }


        private void Update()
        {
            if(tooltipCanvas.activeSelf)
                tooltipCanvas.transform.position = Input.mousePosition;
        }

        public void ShowTooltip(Table table)
        {
            if (!PlayerData.dragActive)
            {
                favWaitress.SetText(table.currentGuests._favoriteWaitress.waitressName);
                guestsName.SetText(table.currentGuests.guestsName);
                tooltipCanvas.SetActive(true);
            }
        }

        public void HideTooltip()
        {
            tooltipCanvas.SetActive(false);
        }
    }
}