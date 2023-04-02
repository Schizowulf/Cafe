using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TableUi : MonoBehaviour
    {
        [SerializeField] private Image timerBar;

        public Image _timerBar { get; private set; }
        public GameObject _gameObject { get; private set; }

        private void Awake()
        {
            _timerBar = timerBar;
            _gameObject = gameObject;
        }
    }
}