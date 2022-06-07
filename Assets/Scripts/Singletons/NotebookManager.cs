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
                    this.Close();
                else
                    this.Open();
            }
        }

        public void Save()
        {
            this.Journal.Save();
        }

        public void Open()
        {
            this._notebook.SetActive(true);
            Singletons.Main.Input.ShowCursor();
        }

        public void Close()
        {
            this._notebook.SetActive(false);
            Singletons.Main.Input.HideCursor();
        }
    }
}