using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notebook;
        public NotebookSection Journal;

        private void Awake()
        {
            Debug.Assert(this._notebook != null);
            this.Journal = this._notebook.transform.Find("Journal")?.GetComponentInChildren<NotebookSection>();
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

        public void Save()
        {
            this.Journal.Save();
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
