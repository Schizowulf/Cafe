using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class WaitressProvider : MonoBehaviour
    {

        public List<Waitress> _waitresses = new List<Waitress>();

        private void Awake()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Waitress currentWaitress = gameObject.transform.GetChild(i).gameObject.GetComponent<Waitress>();
                _waitresses.Add(currentWaitress);
            }
        }
    }
}