using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Guests : Character
    {

        [SerializeField] private Sprite[] guestsSprites;
        [SerializeField] private Waitress favoriteWaitress;
        [SerializeField] private float favoriteWaitressIncomeMultiplier;

        public Sprite guestsIcon { get; private set; }
        public string guestsName { get; private set; }

        public Sprite[] _guestsSprites { get; private set; }
        public Waitress _favoriteWaitress { get; private set; }
        public float _favoriteWaitressIncomeMultiplier { get; private set; }
        public bool inGame { get; private set; }

        private void Awake()
        {
            guestsIcon = iconUI;
            guestsName = characterUnitName;
            _guestsSprites = guestsSprites;
            _favoriteWaitress = favoriteWaitress;
            _favoriteWaitressIncomeMultiplier = favoriteWaitressIncomeMultiplier;

            if (_favoriteWaitressIncomeMultiplier < 1)
            {
                _favoriteWaitressIncomeMultiplier = 1.2f;
            }
        }

        public void SetGuestsInCafeStatus()
        {
            inGame = true;
        }

        public void RemoveGuestsInCafeStatus()
        {
            inGame = false;
        }

    }
}