using System.Collections;
using System.Reflection;
using UnityEngine;

namespace ProjectWendigo
{
    public static class InvokeWithParams
    {
        private static IEnumerator InvokeCoroutine(MonoBehaviour behaviour, string methodName, float time, params object[] args)
        {
            yield return new WaitForSeconds(time);
            var method = behaviour.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            Debug.Assert(method != null, $"Method {methodName} not found in type {behaviour.GetType()}");
            method?.Invoke(behaviour, args);
        }

        public static void Invoke(this MonoBehaviour behaviour, string methodName, float time, params object[] args)
        {
            behaviour.StartCoroutine(InvokeWithParams.InvokeCoroutine(behaviour, methodName, time, args));
        }
    }
}