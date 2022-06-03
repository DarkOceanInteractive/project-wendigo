using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectWendigo
{
    public class NotebookSection : MonoBehaviour
    {
        public UnityEvent<List<object>> OnLoad;
        public UnityEvent OnSave;
        public UnityEvent<object> OnAddElement;

        [SerializeField] private ANotebookDatabase _database;

        private void Start()
        {
            this._database.Load();
            this.OnLoad?.Invoke(this._database.Entries);
        }

        public void Save()
        {
            this._database.Save();
            this.OnSave?.Invoke();
        }

        public void AddElement(object element)
        {
            this._database.AddEntry(element);
            this.OnAddElement?.Invoke(element);
        }
    }
}
