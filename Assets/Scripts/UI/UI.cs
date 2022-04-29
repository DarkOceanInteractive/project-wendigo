using UnityEngine;

namespace ProjectWendigo
{
    public class UI : MonoBehaviour
    {
        public static UI Main { get; private set; }

        public void Awake()
        {
            if (Main != null)
            {
                Destroy(this.gameObject);
                return;
            }
            Main = this;

            DontDestroyOnLoad(this.gameObject);
            Singletons.Main.Input.ShowCursor();
        }
    }
}
