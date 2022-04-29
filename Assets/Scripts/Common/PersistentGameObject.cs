using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class PersistentGameObject : MonoBehaviour
    {
        private static List<string> _activeObjectNames = new List<string>();

        public void Awake()
        {
            if (PersistentGameObject._activeObjectNames.Contains(this.gameObject.name))
            {
                Destroy(this.gameObject);
                return;
            }
            PersistentGameObject._activeObjectNames.Add(this.gameObject.name);
            DontDestroyOnLoad(this.gameObject);
        }
    }
}