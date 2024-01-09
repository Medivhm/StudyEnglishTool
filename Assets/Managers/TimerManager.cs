using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Managers
{
    class TimerManager : MonoSingleton<TimerManager>
    {
        class Timer
        {
            private bool isRunning;
            public bool IsRunning
            {
                get { return isRunning; }
            }
            float time;
            float total;
            Action callback;

            public Timer(float time, Action callback)
            {
                this.isRunning = true;
                this.time = time;
                this.callback = callback;
                this.total = 0;
            }

            public void Update(float deltaTime)
            {
                this.total += deltaTime;
                if(this.total >= time)
                {
                    this.callback();
                    this.isRunning = false;
                }
            }

            public void Stop()
            {
                this.isRunning = false;
            }
        };

        Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        List<string> deleteTimers = new List<string>();

        public void CreateTimer(string timerName, float time, Action callback)
        {
            if(timers.ContainsKey(timerName))
            {
                DebugTool.Error("[ERROR --- 004] TimersManager 已存在相同名称Timer");
                return;
            }
            timers.Add(timerName, new Timer(time, callback));
        }

        public void RemoveTimer(string timerName)
        {
            Timer timer = null;
            timers.TryGetValue(timerName, out timer);
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            TimerUpdate(deltaTime);
            TimerDestroy();
        }

        private void TimerUpdate(float deltaTime)
        {
            List<string> timerNames = timers.Keys.ToList();
            foreach (string timerName in timerNames)
            {
                Timer timer = null;
                timers.TryGetValue(timerName, out timer);
                if (timer != null)
                {
                    if (timer.IsRunning)
                    {
                        timer.Update(deltaTime);
                    }
                    else
                    {
                        deleteTimers.Add(timerName);
                    }
                }
            }
        }

        private void TimerDestroy()
        {
            foreach (string timerName in deleteTimers)
            {
                if (timers.TryGetValue(timerName, out _))
                {
                    timers.Remove(timerName);
                }
            }
            deleteTimers.Clear();
        }
    }
}