using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notebook;
        private NotebookController _notebookController;
        private NotebookNavigation _notebookNavigation;
        [HideInInspector] public NotebookSection Journal;
        [HideInInspector] public NotebookSection Maps;
        [HideInInspector] public NotebookSection Archive;

        private void Awake()
        {
            Debug.Assert(this._notebook != null);
            Debug.Assert(this._notebook.TryGetComponent(out this._notebookController));
            Debug.Assert(this._notebook.TryGetComponent(out this._notebookNavigation));
            this.Journal = this._notebook.transform.Find("Sections/Journal")?.GetComponentInChildren<NotebookSection>();
            this.Maps = this._notebook.transform.Find("Sections/Maps")?.GetComponentInChildren<NotebookSection>();
            this.Archive = this._notebook.transform.Find("Sections/Archive")?.GetComponentInChildren<NotebookSection>();
            this._notebook.SetActive(false);
        }

        private void Update()
        {
            if (Singletons.Main.Input.PlayerToggledNotebookJournal)
                this.ToggleSection(this.Journal);
            if (Singletons.Main.Input.PlayerToggledNotebookMaps)
                this.ToggleSection(this.Maps);
            if (Singletons.Main.Input.PlayerToggledNotebookArchive)
                this.ToggleSection(this.Archive);
            if (this._notebook.activeSelf && Singletons.Main.Input.PlayerExittedUI)
                this.Close();
        }

        public void Save()
        {
            this.Journal.Save();
            this.Maps.Save();
            this.Archive.Save();
        }

        public void ToggleSection(NotebookSection section)
        {
            if (!this._notebook.activeSelf || this._notebookNavigation.GetActiveSection() != section.transform)
            {
                this.Open();
                this._notebookNavigation.GoToSection(section.gameObject);
            }
            else
            {
                this.Close();
            }
        }

        public void ToggleSection(string name)
        {
            var sectionGameObject = this._notebook.transform.Find(name);
            Debug.Assert(sectionGameObject != null, $"No section named {sectionGameObject}");
            Debug.Assert(sectionGameObject.TryGetComponent(out NotebookSection section),
                $"Object {sectionGameObject} does not contain a {typeof(NotebookSection).Name} component");
            this.ToggleSection(section);
        }

        public void Open()
        {
            this._notebookController.Open();
        }

        public void Close()
        {
            this._notebookController.Close();
        }
    }
}
