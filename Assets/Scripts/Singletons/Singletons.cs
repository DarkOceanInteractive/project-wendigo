using UnityEngine;

namespace ProjectWendigo
{
    public class Singletons : MonoBehaviour
    {
        public static Singletons Main { get; private set; }

        public InputManager Input { get; private set; }
        public SoundManager Sound { get; private set; }

        public void Awake()
        {
            if (Main != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Main = this;

            DontDestroyOnLoad(this.gameObject);

            Main.Input = this.GetComponentInChildren<InputManager>();
            Main.Sound = this.GetComponentInChildren<SoundManager>();
        }
    }
}
