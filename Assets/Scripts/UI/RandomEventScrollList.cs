using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomEventScrollList : MonoBehaviour
    {

        public GameObject currentGameObject { get; private set; }

        private void Awake()
        {
            currentGameObject = gameObject;
        }

    }
}