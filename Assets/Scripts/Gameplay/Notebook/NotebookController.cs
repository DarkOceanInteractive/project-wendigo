using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookController : MonoBehaviour
    {
        public void Open()
        {
            this.gameObject.SetActive(true);
            Singletons.Main.Input.ShowCursor();
            Singletons.Main.Sound.Play("notebook_open");
            Singletons.Main.Camera.LockCamera();
        }

        public void Close()
        {
            this.gameObject.SetActive(false);
            Singletons.Main.Input.HideCursor();
            Singletons.Main.Sound.Play("notebook_close");
            Singletons.Main.Camera.UnlockCamera();
        }
    }
}