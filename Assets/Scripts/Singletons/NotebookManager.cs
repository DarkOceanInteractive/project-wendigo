using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookManager : MonoBehaviour
    {
        [SerializeField] private GameObject _notebook;
        private NotebookController _notebookController;
        private NotebookNavigation _notebookNavigation;
        private NotebookSection _journal;
        private NotebookSection _maps;
        private NotebookSection _archive;

        private void Awake()
        {
            Debug.Assert(this._notebook != null);
            Debug.Assert(this._notebook.TryGetComponent(out this._notebookController));
            Debug.Assert(this._notebook.TryGetComponent(out this._notebookNavigation));
            this._journal = this._notebook.transform.Find("Sections/Journal")?.GetComponentInChildren<NotebookSection>();
            this._maps = this._notebook.transform.Find("Sections/Maps")?.GetComponentInChildren<NotebookSection>();
            this._archive = this._notebook.transform.Find("Sections/Archive")?.GetComponentInChildren<NotebookSection>();
            this._notebook.SetActive(false);
        }

        private void Update()
        {
            if (Singletons.Main.Input.PlayerToggledNotebookJournal)
                this.ToggleSection(this._journal);
            if (Singletons.Main.Input.PlayerToggledNotebookMaps)
                this.ToggleSection(this._maps);
            if (Singletons.Main.Input.PlayerToggledNotebookArchive)
                this.ToggleSection(this._archive);
            if (this._notebook.activeSelf && Singletons.Main.Input.PlayerExittedUI)
                this.Close();
        }

        public void Save()
        {
            this._journal.Save();
            this._maps.Save();
            this._archive.Save();
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

        public void AddArchiveEntryById(int id)
        {
            this._archive.AddEntry(new ArchiveCollectedEntry { CollectionEntryId = id });
        }
        public void AddArchiveEntryByTitle(string title)
        {
            this._archive.AddEntryByTitle(title);
        }
        public bool ArchiveHasEntry(int id)
        {
            return this._archive.HasEntry(id);
        }
        public bool ArchiveHasEntry(string title)
        {
            return this._archive.HasEntry(title);
        }

        public void AddJournalEntryById(int id)
        {
            this._journal.AddEntry(new JournalCollectedEntry { CollectionEntryId = id });
        }
        public void AddJournalEntryByTitle(string title)
        {
            this._journal.AddEntryByTitle(title);
        }
        public bool JournalHasEntry(int id)
        {
            return this._journal.HasEntry(id);
        }
        public bool JournalHasEntry(string title)
        {
            return this._journal.HasEntry(title);
        }

        public void AddMapsEntryById(int id)
        {
            this._maps.AddEntry(new MapsCollectedEntry { CollectionEntryId = id });
        }
        public void AddMapsEntryByTitle(string title)
        {
            this._maps.AddEntryByTitle(title);
        }
        public bool MapsHasEntry(int id)
        {
            return this._maps.HasEntry(id);
        }
        public bool MapsHasEntry(string title)
        {
            return this._maps.HasEntry(title);
        }
    }
}
