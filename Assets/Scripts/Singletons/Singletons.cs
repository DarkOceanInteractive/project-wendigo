using UnityEngine;

namespace ProjectWendigo
{
    public class Singletons : MonoBehaviour
    {
        public static Singletons Main { get; private set; }

        public SaveManager Save { get; private set; }
        public InputManager Input { get; private set; }
        public SoundManager Sound { get; private set; }
        public FadeManager Fade { get; private set; }
        public NotebookManager Notebook { get; private set; }
        public CameraManager Camera { get; private set; }
        public OptionsManager Option { get; private set; }
        public SceneSwitcher Scene { get; private set; }

        public void Awake()
        {
            if (Main != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Main = this;

            Main.Save = this.GetComponentInChildren<SaveManager>();
            Main.Input = this.GetComponentInChildren<InputManager>();
            Main.Sound = this.GetComponentInChildren<SoundManager>();
            Main.Fade = this.GetComponentInChildren<FadeManager>();
            Main.Notebook = this.GetComponentInChildren<NotebookManager>();
            Main.Camera = this.GetComponentInChildren<CameraManager>();
            Main.Option = this.GetComponentInChildren<OptionsManager>();
            Main.Scene = this.GetComponentInChildren<SceneSwitcher>();
        }
    }
}
