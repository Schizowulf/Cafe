using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Chairs : MonoBehaviour
    {

        public int chairsNumber { get; private set; }
        public Sprite singleChairDefaultSprite { get; private set; }
        
        void Awake()
        {
            chairsNumber = gameObject.transform.childCount;
            singleChairDefaultSprite = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        }

    }
}