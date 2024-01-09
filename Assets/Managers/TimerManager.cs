using System;
using System.Collections.Generic;
using System.Linq;
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
                if (this.total >= time)
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

        // 手动定时器
        Dictionary<string, Timer> timers = new Dictionary<string, Timer>();
        List<string> deleteTimers = new List<string>();
        // 自动定时器
        Dictionary<int, Timer> autoTimers = new Dictionary<int, Timer>();
        List<int> deleteAutoTimers = new List<int>();

        private int timerId;
        private int TimerId
        {
            get
            {
                int temp = timerId;
                int rangeTimes = 0;
                while (autoTimers.ContainsKey(temp) && rangeTimes < Define.Constant.TimerMax)
                {
                    temp = (temp + 1) % Define.Constant.TimerMax;
                    rangeTimes++;
                }
                if (rangeTimes == Define.Constant.TimerMax)
                {
                    DebugTool.Error("定时器超数量了");
                    throw new Exception("定时器超数量了");
                }
                else
                {
                    timerId = temp;
                    return temp;
                }
            }
            set
            {
                timerId = value;
            }
        }

        public void Init()
        {
            TimerId = 0;
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

            List<int> autoTimerIds = autoTimers.Keys.ToList();
            foreach (int timerId in autoTimerIds)
            {
                Timer timer = null;
                autoTimers.TryGetValue(timerId, out timer);
                if (timer != null)
                {
                    if (timer.IsRunning)
                    {
                        timer.Update(deltaTime);
                    }
                    else
                    {
                        deleteAutoTimers.Add(timerId);
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

            foreach (int timerId in deleteAutoTimers)
            {
                if (autoTimers.TryGetValue(timerId, out _))
                {
                    autoTimers.Remove(timerId);
                }
            }
            deleteAutoTimers.Clear();
        }

        public void CreateTimer(string timerName, float time, Action callback)
        {
            if (timers.ContainsKey(timerName))
            {
                DebugTool.Error("TimersManager 已存在相同名称Timer");
                return;
            }
            timers.Add(timerName, new Timer(time, callback));
        }

        public int CreateTimer(float time, Action callback)
        {
            int timerId = TimerId;
            autoTimers.Add(TimerId, new Timer(time, callback));
            return timerId;
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

        public void RemoveTimer(int timerId)
        {
            Timer timer = null;
            autoTimers.TryGetValue(timerId, out timer);
            if (timer != null)
            {
                timer.Stop();
            }
        }
    }
}