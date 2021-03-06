using System;
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
        public UnityEvent<ANotebookCollectionEntry> OnRemoveElement;

        [SerializeField] private DatabaseTable _collection;
        [SerializeField] private DatabaseTable _collected;

        public void Load()
        {
            this.Clear();
            this._collected.Load();
            foreach (ANotebookCollectedEntry entry in this._collected.GetAll())
                this.OnAddElement?.Invoke(this.GetCollectionEntry(entry));
        }

        private void Redraw()
        {
            this.OnClear?.Invoke();
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

        public ANotebookCollectionEntry GetCollectionEntryByTitle(string title)
        {
            return this._collection.FindOne(entry => ((ANotebookCollectionEntry)entry).Title == title) as ANotebookCollectionEntry;
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

        public bool AddEntry(ANotebookCollectedEntry entry)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntry(entry);
            if (this._collected.Insert(entry))
            {
                this.OnAddElement?.Invoke(collectionEntry);
                return true;
            }
            return false;
        }

        public bool AddEntryByTitle(string title)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntryByTitle(title);
            if (collectionEntry == null)
                return false;
            ANotebookCollectedEntry entry = (ANotebookCollectedEntry)Activator.CreateInstance(this._collected.EntryType);
            entry.CollectionEntryId = collectionEntry.Id;
            if (this._collected.Insert(entry))
            {
                this.OnAddElement?.Invoke(collectionEntry);
                return true;
            }
            return false;
        }

        public bool RemoveEntry(ANotebookCollectedEntry entry)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntry(entry);
            if (this._collected.RemoveOne(it => ((ANotebookCollectedEntry)it).CollectionEntryId == entry.CollectionEntryId))
            {
                this.Redraw();
                this.OnRemoveElement?.Invoke(collectionEntry);
                return true;
            }
            return false;
        }

        public bool RemoveEntryByTitle(string title)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntryByTitle(title);
            if (collectionEntry == null)
                return false;
            ANotebookCollectedEntry entry = (ANotebookCollectedEntry)Activator.CreateInstance(this._collected.EntryType);
            entry.CollectionEntryId = collectionEntry.Id;
            return this.RemoveEntry(entry);
        }

        public bool HasEntry(int id)
        {
            return this._collected.FindOne(it => ((ANotebookCollectedEntry)it).CollectionEntryId == id) != null;
        }

        public bool HasEntry(string title)
        {
            ANotebookCollectionEntry collectionEntry = this.GetCollectionEntryByTitle(title);
            if (collectionEntry == null)
                return false;
            return this.HasEntry(collectionEntry.Id);
        }
    }
}
