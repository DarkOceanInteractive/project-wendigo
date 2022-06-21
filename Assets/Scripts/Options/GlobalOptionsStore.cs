using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewGlobalOptions", menuName = "Global Options")]
    public class GlobalOptionsStore : ScriptableObject
    {
        public float Volume = 0.5f;
        public float Brightness = 0f;
        public bool Headbobbing = true;
        public bool InvertY = false;

        [System.Serializable]
        private class SerializableOptions
        {
            public float Volume;
            public float Brightness;
            public bool Headbobbing;
            public bool InvertY;
        }

        private SerializableOptions ToSerializableOptions()
        {
            return new SerializableOptions
            {
                Volume = this.Volume,
                Brightness = this.Brightness,
                Headbobbing = this.Headbobbing,
                InvertY = this.InvertY
            };
        }

        private void FromSerializableOptions(SerializableOptions options)
        {
            this.Volume = options.Volume;
            this.Brightness = options.Brightness;
            this.Headbobbing = options.Headbobbing;
            this.InvertY = options.InvertY;
        }

        public void Save()
        {
            string path = Singletons.Main.Save.GetSavePath(this.name);
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this.ToSerializableOptions());
            stream.Close();
        }

        public void Load()
        {
            string path = Singletons.Main.Save.GetSavePath(this.name);
            if (File.Exists(path))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                SerializableOptions options = formatter.Deserialize(stream) as SerializableOptions;
                this.FromSerializableOptions(options);
                stream.Close();
            }
        }
    }
}
