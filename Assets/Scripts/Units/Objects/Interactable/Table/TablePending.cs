using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class TablePending : MonoBehaviour, IObserver
    {

        [SerializeField] private float duration = 7f;
        [SerializeField] private Subject _tableSubject;
        [SerializeField] private TableUi tableUi;

        private Timer timer;
        private Table _table;

        private void Start()
        {
            tableUi._gameObject.SetActive(false);
            _table = gameObject.GetComponent<Table>();
        }
        void Update()
        {
            if (timer != null)
            {
                timer.Tick(Time.deltaTime);
                tableUi._timerBar.fillAmount = (timer == null ? 0 : timer.remainingSeconds) / duration;
            }
        }

        public void StartPending()
        {
            timer = new Timer(duration);
            timer.onTimerEnd.AddListener(EndPending);
            tableUi._gameObject.SetActive(true);
            tableUi._timerBar.color = new Color32(255, 255, 225, 255);
            FindAnyObjectByType<AudioManager>().Play("GuestsSpawned");
        }

        private void EndPending()
        {
            BreakPending();
            _table.PendingStopped();
        }

        private void BreakPending()
        {
            tableUi._gameObject.SetActive(false);
            timer = null;
        }

        public void OnNotify(Actions action)
        {
            switch (action)
            {
                case Actions.TablePending:
                    StartPending();
                    break;
                case Actions.TablePendingStop:
                    BreakPending();
                    break;
            }
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