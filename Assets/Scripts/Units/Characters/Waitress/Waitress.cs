using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Waitress : Character
    {

        [SerializeField] private Sprite mainSprite;
        [SerializeField] private int _income = 10;

        private Table _currentTable;
        private WaitressStatuses _currentStatus;

        public Sprite _mainSprite { get; private set; }
        public Sprite _uiSprite { get; private set; }
        public UnityEvent onTableChanged = new UnityEvent();
        public int income { get; private set; }
        public string waitressName { get; private set; }

        private void Awake()
        {
            _currentStatus = WaitressStatuses.Resting;
            _mainSprite = mainSprite;
            _uiSprite = iconUI;
            income = _income;
            waitressName = characterUnitName;
        }

        public void ChangeStatus(WaitressStatuses newStatus)
        {
            _currentStatus = newStatus;
        }

        public void AssignToTable(Table table)
        {
            _currentTable = table;
            onTableChanged?.Invoke();
        }

        public Table GetCurrentTable()
        {
            return _currentTable;
        }
    }
}