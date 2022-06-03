using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectWendigo
{
    public class ASavedDatabaseTable<EntryType> : DatabaseTable<EntryType>
        where EntryType : class, IDatabaseTableEntry
    {
        private string _savePath => Singletons.Main.Save.GetSavePath(this.name);

        [ContextMenu("Save")]
        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(this._savePath, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this.Entries);
            stream.Close();
        }

        public void Load()
        {
            if (File.Exists(this._savePath))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(this._savePath, FileMode.Open, FileAccess.Read);
                List<EntryType> entries = formatter.Deserialize(stream) as List<EntryType>;
                this.Clear();
                foreach (EntryType entry in entries)
                    this.Insert(entry);
                stream.Close();
            }
        }
    }
}
