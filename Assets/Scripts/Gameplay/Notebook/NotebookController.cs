using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookController : MonoBehaviour
    {
        public void Open()
        {
            this.gameObject.SetActive(true);
            Singletons.Main.Input.ShowCursor();
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            Singletons.Main.Input.HideCursor();
        }
    }
}