using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class MoneyOnUi : MonoBehaviour
    {
        [SerializeField] private GameObject moneyText;

        void Update()
        {
            moneyText.GetComponent<TextMeshProUGUI>().SetText(PlayerData.money.ToString());
        }
    }
}