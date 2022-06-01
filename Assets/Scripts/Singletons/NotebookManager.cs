using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notebook;

        private void Awake()
        {
            this._notebook.SetActive(false);
        }

        private void Update()
        {
            if (Singletons.Main.Input.PlayerToggledNotebook)
            {
                if (this._notebook.activeSelf)
                    this.CloseNotebook();
                else
                    this.OpenNotebook();
            }
        }

        private void OpenNotebook()
        {
            this._notebook.SetActive(true);
            Singletons.Main.Input.ShowCursor();
        }

        private void CloseNotebook()
        {
            this._notebook.SetActive(false);
            Singletons.Main.Input.HideCursor();
        }
    }
}