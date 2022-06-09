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
            foreach (ANotebookCollectedEntry entry in this._collected.GetAll())
                this.OnAddElement?.Invoke(this.GetCollectionEntry(entry));
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

        private ANotebookCollectionEntry GetCollectionEntry(ANotebookCollectedEntry entry)
        {
            ANotebookCollectionEntry collectionEntry = entry.ResolveReference<ANotebookCollectionEntry>(new ReferenceToOne
            {
                KeyName = "CollectionEntryId",
                ForeignKeyName = "Id",
                ForeignTable = this._collection
            });
            Debug.Assert(collectionEntry != null, $"No collection entry found matching given id");
            return collectionEntry;
        }

        public void AddEntry(ANotebookCollectedEntry entry)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntry(entry);
            if (this._collected.Insert(entry))
                this.OnAddElement?.Invoke(collectionEntry);
        }
    }
}
