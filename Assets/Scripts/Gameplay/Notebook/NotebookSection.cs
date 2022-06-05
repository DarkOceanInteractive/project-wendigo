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
        public UnityEvent<ANotebookCollectionEntry> OnAddElement;

        [SerializeField] private DatabaseTable _collection;
        [SerializeField] private DatabaseTable _collected;

        private void Start()
        {
            this.Clear();
            this._collected.Load();
            foreach (ANotebookCollectedEntry element in this._collected.GetAll())
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

        private ANotebookCollectionEntry GetCollectedElement(IDatabaseEntry element)
        {
            ANotebookCollectionEntry collectionEntry = element.ResolveReference<ANotebookCollectionEntry>(new ReferenceToOne
            {
                KeyName = "CollectionEntryId",
                ForeignKeyName = "Id",
                ForeignTable = this._collection
            });
            Debug.Assert(collectionEntry != null, $"No collection entry found matching given id");
            return collectionEntry;
        }

        public void AddElement(ANotebookCollectedEntry element)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectedElement(element);
            this._collected.Insert(element);
            this.OnAddElement?.Invoke(collectionEntry);
        }
    }
}
