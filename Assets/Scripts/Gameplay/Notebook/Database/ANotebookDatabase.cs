using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectWendigo
{
    public abstract class ANotebookDatabase : ScriptableObject
    {
        public abstract List<object> Entries { get; }
        [SerializeField] private string _savePath;
        private string _fullSavePath => Singletons.Main.Save.GetSavePath(this._savePath);

        public abstract void AddEntry(object entry);

        public abstract void SetEntries(List<object> entries);

        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(this._fullSavePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this.Entries);
            stream.Close();
        }

        public void Load()
        {
            if (File.Exists(this._fullSavePath))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(this._fullSavePath, FileMode.Open, FileAccess.Read);
                List<object> entries = formatter.Deserialize(stream) as List<object>;
                this.SetEntries(entries);
                stream.Close();
            }
        }
    }

    public abstract class ANotebookDatabase<EntryType> : ANotebookDatabase
        where EntryType : INotebookEntry
    {
        [HideInInspector]
        public override List<object> Entries => this._entries.ToList<object>();

        [SerializeField]
        protected List<EntryType> _entries;

        protected virtual void OnValidate()
        {
            for (int i = 0; i < this._entries.Count; ++i)
            {
                this._entries[i].Id = i;
            }
        }

        public override void AddEntry(object entry)
        {
            this._entries.Add(entry as EntryType);
        }

        public override void SetEntries(List<object> entries)
        {
            this._entries = entries.Cast<EntryType>().ToList();
        }
    }
}
