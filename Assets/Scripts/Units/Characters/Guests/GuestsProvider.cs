using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GuestsProvider : MonoBehaviour
    {

        public List<Guests> _guests = new List<Guests>();

        private void Awake()
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                Guests currentGuests = gameObject.transform.GetChild(i).gameObject.GetComponent<Guests>();
                _guests.Add(currentGuests);
            }
        }
    }
}