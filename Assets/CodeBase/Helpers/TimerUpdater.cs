using System;

namespace CodeBase.Helpers
{
    public class TimerUpdater
    {
        private float _timer;

        public void TimerUpdateWithAction(float timerCooldown, bool check, Action timerAction, float deltaTime)
        {
            if (_timer > timerCooldown)
            {
                if (check)
                {
                    _timer = 0;
                    timerAction?.Invoke();
                }
            }
            else
                _timer += deltaTime;
        }
    }
}