using System.Collections.Generic;
using Project.Scripts.Events.Base;
using UnityEngine.Events;

namespace Project.Scripts.Events.Systems
{
    public class GlobalEventSystem : IEventSystem
    {
        private static GlobalEventSystem instance;
        public static GlobalEventSystem I
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalEventSystem();
                }

                return instance;
            }
        }

        private Dictionary<string, UnityEvent<GameEventArgs>> allListeners;

        private GlobalEventSystem()
        {
            allListeners = new Dictionary<string, UnityEvent<GameEventArgs>>();

            foreach (var msgName in EventNames.TEXT_ALL_NAMES)
            {
                allListeners.Add(msgName, new UnityEvent<GameEventArgs>());
            }
        }

        public void SendEvent(string eventName, GameEventArgs args)
        {
            allListeners[eventName].Invoke(args);
        }

        public void Subscribe(string eventName, UnityAction<GameEventArgs> listener)
        {
            allListeners[eventName].AddListener(listener);
        }

        public void Unsubscribe(string eventName, UnityAction<GameEventArgs> listener)
        {
            allListeners[eventName].RemoveListener(listener);
        }
    }
}