using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class TableIdle : MonoBehaviour, IObserver
    {

        [SerializeField] private Subject _tableSubject;

        private float maxSpawnDelay = 10f;
        private float minSpawnDelay = 5f;

        private Table _table;

        private void Awake()
        {
            _table = gameObject.GetComponent<Table>();
        }
        private IEnumerator GuestSpawn()
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            _table.IdleStopped();
            StopCoroutine(GuestSpawn());
        }

        public void OnNotify(Actions action)
        {
            switch (action)
            {
                case Actions.TableIdle:
                    StartCoroutine(GuestSpawn());
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