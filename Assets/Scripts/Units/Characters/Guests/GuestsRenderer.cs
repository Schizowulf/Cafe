using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class GuestsRenderer : MonoBehaviour
    {
        private Table _table;
        private List<Guests> possibleGuests;

        public void Awake()
        {
            _table = gameObject.GetComponent<Table>();
            possibleGuests = FindAnyObjectByType<GuestsProvider>()._guests;
        }
        
        public Guests RenderGuests()
        {
            Guests currentGuests = GetRandomGuests();
            Sprite[] guestSprites = currentGuests._guestsSprites;
            int totalGuests = currentGuests._guestsSprites.Length;

            for (int i = 0; i < totalGuests; i++)
            {
                AssignSprite(_table._chairs.transform.GetChild(i), guestSprites[i]);
            }

            return currentGuests;
        }

        public void RemoveGuests()
        {
            for (int i = 0; i < _table._chairs.transform.childCount; i++)
            {
                AssignSprite(_table._chairs.transform.GetChild(i), _table._chairs.singleChairDefaultSprite);
            }
        }

        private void AssignSprite(Transform gameObject, Sprite sprite)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
        private Guests GetRandomGuests()
        {
            List<Guests> waitingGuests = possibleGuests.FindAll(e => e.inGame == false);
            return waitingGuests[UnityEngine.Random.Range(0, waitingGuests.Count)];
        }
    }
}