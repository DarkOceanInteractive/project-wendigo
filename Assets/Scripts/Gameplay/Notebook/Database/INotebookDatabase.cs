using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectWendigo
{
    public abstract class INotebookDatabase : ScriptableObject
    {
        public abstract List<object> Entries { get; }
        [SerializeField] private string _savePath;
        private string _fullSavePath => string.Concat(Application.persistentDataPath, this._savePath);

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
}
