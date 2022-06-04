using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProjectWendigo.Database.Extensions.Save
{
    public static class SaveDatabaseExtension
    {
        public static void Save(this DatabaseTable table)
        {
            string path = Singletons.Main.Save.GetSavePath(table.name);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, table.FindMany(_ => true));
            stream.Close();
        }

        public static void Load(this DatabaseTable table)
        {
            string path = Singletons.Main.Save.GetSavePath(table.name);
            if (File.Exists(path))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                List<object> entries = formatter.Deserialize(stream) as List<object>;
                table.Clear();
                foreach (object entry in entries)
                    table.Insert(entry);
                stream.Close();
            }
        }
    }
}