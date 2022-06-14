using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class EventManager : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, UnityEventBase> _events = new Dictionary<string, UnityEventBase>();

        public EventType GetEvent<EventType>(string eventName)
             where EventType : class, new()
        {
            if (!this._events.ContainsKey(eventName))
                this._events.Add(eventName, new EventType() as UnityEventBase);
            EventType evt = this._events[eventName] as EventType;
            if (evt == null)
                Debug.LogWarning($"Incompatible event type {this._events[eventName].GetType()} with {typeof(EventType)}");
            return evt;
        }

        public void On(string eventName, UnityAction listener)
        {
            this.GetEvent<UnityEvent>(eventName)?.AddListener(listener);
        }

        public void On<T0>(string eventName, UnityAction<T0> listener)
        {
            this.GetEvent<UnityEvent<T0>>(eventName)?.AddListener(listener);
        }

        public void On<T0, T1>(string eventName, UnityAction<T0, T1> listener)
        {
            this.GetEvent<UnityEvent<T0, T1>>(eventName)?.AddListener(listener);
        }

        public void On<T0, T1, T2>(string eventName, UnityAction<T0, T1, T2> listener)
        {
            this.GetEvent<UnityEvent<T0, T1, T2>>(eventName)?.AddListener(listener);
        }

        public void On<T0, T1, T2, T3>(string eventName, UnityAction<T0, T1, T2, T3> listener)
        {
            this.GetEvent<UnityEvent<T0, T1, T2, T3>>(eventName)?.AddListener(listener);
        }

        public void Trigger(string eventName)
        {
            this.GetEvent<UnityEvent>(eventName)?.Invoke();
        }

        public void Trigger<T0>(string eventName, T0 arg0)
        {
            this.GetEvent<UnityEvent<T0>>(eventName)?.Invoke(arg0);
        }

        public void Trigger<T0, T1>(string eventName, T0 arg0, T1 arg1)
        {
            this.GetEvent<UnityEvent<T0, T1>>(eventName)?.Invoke(arg0, arg1);
        }

        public void Trigger<T0, T1, T2>(string eventName, T0 arg0, T1 arg1, T2 arg2)
        {
            this.GetEvent<UnityEvent<T0, T1, T2>>(eventName)?.Invoke(arg0, arg1, arg2);
        }

        public void Trigger<T0, T1, T2, T3>(string eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            this.GetEvent<UnityEvent<T0, T1, T2, T3>>(eventName)?.Invoke(arg0, arg1, arg2, arg3);
        }
    }
}
