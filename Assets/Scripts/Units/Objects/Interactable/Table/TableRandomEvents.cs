using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class TableRandomEvents : MonoBehaviour, IObserver
    {

        [SerializeField] private Subject _tableSubject;

        private Table _table;
        //private Waitress _currentWaitress;
        //private Guests _currentGuests;
        private RandomEventScrollList scrollList;
        private string[] messages = new string[]; 

        public void OnNotify(Actions action)
        {
            switch (action)
            {
                case Actions.TableServing:
                    StartRandomEventsGenerator();
                    break;
            }
        }

        private void StartRandomEventsGenerator()
        {
            StartCoroutine(EventSpawn());
        }

        private void Awake()
        {
            _table = gameObject.GetComponent<Table>();
            scrollList = FindAnyObjectByType<RandomEventScrollList>();
        }

        private IEnumerator EventSpawn()
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(5, 15));

            if (!_table.serving)
            {
                StopCoroutine(EventSpawn());
                yield break;
            }

            int[] moneyChange = new int[] { -30, 50, 20, -50, 50, 100 };

            int randomIndex = UnityEngine.Random.Range(0, messages.Length);

            string currentMessage = messages[randomIndex].Replace("{waitressName}",
                _table.currentWaitress.waitressName).Replace("{guestsName}", _table.currentGuests.guestsName);

            GameObject newEventText = new GameObject("EventText", typeof(TextMeshProUGUI));

            GameObject newMessageObj = Instantiate(newEventText, scrollList.currentGameObject.transform);

            TextMeshProUGUI newMessageText = newMessageObj.GetComponent<TextMeshProUGUI>();
            newMessageText.SetText(currentMessage);
            newMessageText.fontSize = 20;
            newMessageText.color = new Color(0, 0, 0, 255);

            PlayerData.money += moneyChange[randomIndex];

            FindAnyObjectByType<AudioManager>().Play("TableRandomEventSpawned");

            StopCoroutine(EventSpawn());
            StartCoroutine(RemoveMessageAfterDelay(newMessageObj));
        }

        private IEnumerator RemoveMessageAfterDelay(GameObject newMessage)
        {
            yield return new WaitForSeconds(15);

            Destroy(newMessage);
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