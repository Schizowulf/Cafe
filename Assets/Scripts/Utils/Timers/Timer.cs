using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class Timer
    {
        public float remainingSeconds { get; private set; }
        public UnityEvent onTimerEnd = new UnityEvent();

        public Timer(float duration)
        {
            remainingSeconds = duration;
        }

        public void Tick(float deltaTime)
        {
            if (remainingSeconds == 0f)
            {
                return;
            }

            remainingSeconds -= deltaTime;

            CheckForTimerEnd();
        }

        private void CheckForTimerEnd()
        {
            if (remainingSeconds > 0f)
            {
                return;
            }

            remainingSeconds = 0f;

            onTimerEnd?.Invoke();
        }
    }
}
