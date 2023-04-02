using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class TableServing : MonoBehaviour, IObserver
    {

        [SerializeField] private float moneyAddPeriod = 1f;
        [SerializeField] private float servingPeriod = 10f;
        [SerializeField] private Subject _tableSubject;
        [SerializeField] private TableUi tableUi;

        private Timer timerMoney;
        private Timer timerServing;
        private Table _table;
        private Waitress currentWaitress;

        private void Awake()
        {
            _table = gameObject.GetComponent<Table>();
        }

        private void Update()
        {
            timerMoney?.Tick(Time.deltaTime);
            if (timerServing != null)
            {
                timerServing.Tick(Time.deltaTime);
                tableUi._timerBar.fillAmount = (timerServing == null ? 0 : timerServing.remainingSeconds) / servingPeriod;
            }
        }

        public void OnNotify(Actions action)
        {
            switch (action)
            {
                case Actions.TableServing:
                    StartServing();
                    break;
            }
        }

        private void StartServing()
        {
            currentWaitress = _table.currentWaitress;
            currentWaitress.onTableChanged.AddListener(OnWaitressTableChanged);

            tableUi._gameObject.SetActive(true);
            tableUi._timerBar.color = new Color32(0, 100, 225, 255);

            FindAnyObjectByType<AudioManager>().Play("GuestsServing");

            StartMoneyTimer();
            StartServingTimer();
        }

        private void OnWaitressTableChanged()
        {
            if (currentWaitress.GetCurrentTable() != _table)
            {
                ServingDone();
            }
        }

        private void AddMoneyToPlayer()
        {
            if(currentWaitress == _table.currentGuests._favoriteWaitress)
                PlayerData.money += (int) System.Math.Ceiling( (float) currentWaitress.income * _table.currentGuests._favoriteWaitressIncomeMultiplier );
            else
                PlayerData.money += currentWaitress.income;

            StartMoneyTimer();
        }

        private void StartMoneyTimer()
        {
            timerMoney = new Timer(moneyAddPeriod);
            timerMoney.onTimerEnd.AddListener(AddMoneyToPlayer);
        }

        private void StartServingTimer()
        {
            timerServing = new Timer(servingPeriod);
            timerServing.onTimerEnd.AddListener(ServingDone);
        }

        private void ServingDone()
        {
            timerServing = null;
            timerMoney = null;
            tableUi._gameObject.SetActive(false);
            currentWaitress.onTableChanged.RemoveListener(OnWaitressTableChanged);
            _table.ServingStopped();
        }

        private void OnEnable()
        {
            _tableSubject.AddObserver(this);
        }

        private void OnDisable()
        {
            _tableSubject.RemoveObserver(this);
        }
    }
}