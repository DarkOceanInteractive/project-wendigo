using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using ProjectWendigo.Database.Extensions.Save;
using ProjectWendigo.Database.Extensions.Reference;

namespace ProjectWendigo
{
    public class NotebookSection : MonoBehaviour
    {
        public UnityEvent OnSave;
        public UnityEvent OnClear;
        public UnityEvent<object> OnAddElement;

        [SerializeField] private DatabaseTable _collection;
        [SerializeField] private DatabaseTable _collected;

        private void Start()
        {
            this.Clear();
            this._collected.Load();
            foreach (object element in this._collected.GetAll())
                this.OnAddElement?.Invoke(this.GetCollectedElement(element));
        }

        public void Save()
        {
            this._collected.Save();
            this.OnSave?.Invoke();
        }

        public void Clear()
        {
            this._collected.Clear();
            this.OnClear?.Invoke();
        }

        private object GetCollectedElement(object element)
        {
            object collectionEntry = this._collected.ResolveReference(new ReferenceToOne
            {
                KeyName = "CollectionEntryId",
                ForeignKeyName = "Id",
                ForeignTable = this._collection
            }, element);
            Debug.Assert(collectionEntry != null, $"No collection entry found matching given id");
            return collectionEntry;
        }

        public void AddElement(object element)
        {
            object collectionEntry = this.GetCollectedElement(element);
            this._collected.Insert(element);
            this.OnAddElement?.Invoke(collectionEntry);
        }
    }
}
