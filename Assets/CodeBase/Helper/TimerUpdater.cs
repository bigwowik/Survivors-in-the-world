using System;
using UnityEngine;

namespace CodeBase.Hero.Weapon
{
    public class TimerUpdater
    {
        private float timer;

        public void TimerUpdateWithAction(float timerCooldown, bool check, Action timerAction, float deltaTime)
        {
            if (timer > timerCooldown)
            {
                if (check)
                {
                    timer = 0;
                    timerAction?.Invoke();
                }
            }
            else
                timer += deltaTime;
        }
    }
}