using UnityEngine;

namespace ProjectWendigo
{
    public class GlobalOptions : MonoBehaviour
    {
        [SerializeField] private GlobalOptionsStore _store;
        public static GlobalOptions Main { get; private set; }

        public float Volume
        {
            get => this._store.Volume;
            set => this._store.Volume = value;
        }

        public float Brightness
        {
            get => this._store.Brightness;
            set => this._store.Brightness = value;
        }

        public float Sensitivity
        {
            get => this._store.Sensitivity;
            set => this._store.Sensitivity = value;
        }

        public bool Headbobbing
        {
            get => this._store.Headbobbing;
            set => this._store.Headbobbing = value;
        }

        public bool InvertY
        {
            get => this._store.InvertY;
            set => this._store.InvertY = value;
        }

        public void Save()
        {
            this._store.Save();
        }

        public void Awake()
        {
            if (Main != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Main = this;
            Main._store.Load();
        }
    }
}
