using System;
using UnityEngine;

namespace ProjectWendigo
{
    public class NotebookManager : MonoBehaviour
    {
        public GameObject Notebook;
        private NotebookController _notebookController;
        private NotebookNavigation _notebookNavigation;
        private NotebookSection _journal;
        private NotebookSection _maps;
        private NotebookSection _archive;

        private void Awake()
        {
            Debug.Assert(this.Notebook != null);
            Debug.Assert(this.Notebook.TryGetComponent(out this._notebookController));
            Debug.Assert(this.Notebook.TryGetComponent(out this._notebookNavigation));
            this.Notebook.SetActive(true);
            this._journal = this.Notebook.transform.Find("Sections/Journal")?.GetComponentInChildren<NotebookSection>();
            this._journal.Load();
            this._maps = this.Notebook.transform.Find("Sections/Maps")?.GetComponentInChildren<NotebookSection>();
            this._maps.Load();
            this._archive = this.Notebook.transform.Find("Sections/Archive")?.GetComponentInChildren<NotebookSection>();
            this._archive.Load();
            this.Notebook.SetActive(false);
        }

        private void Update()
        {
            if (Singletons.Main.Input.PlayerToggledNotebookJournal)
                this.ToggleSection(this._journal);
            if (Singletons.Main.Input.PlayerToggledNotebookMaps)
                this.ToggleSection(this._maps);
            if (Singletons.Main.Input.PlayerToggledNotebookArchive)
                this.ToggleSection(this._archive);
            if (this.Notebook.activeSelf && Singletons.Main.Input.PlayerExittedUI)
                this.Close();
        }

        public void Save()
        {
            this._journal.Save();
            this._maps.Save();
            this._archive.Save();
        }

        public void Clear()
        {
            this._journal.Clear();
            this._maps.Clear();
            this._archive.Clear();
        }

        public void ToggleSection(NotebookSection section)
        {
            bool isNotebookOpen = this.Notebook.activeSelf;
            if (!isNotebookOpen || this._notebookNavigation.GetActiveSection() != section.transform)
            {
                if (!isNotebookOpen)
                    this.Open();
                this._notebookNavigation.GoToSection(section.gameObject, isNotebookOpen);
            }
            else
            {
                this.Close();
            }
        }

        public void ToggleSection(string name)
        {
            var sectionGameObject = this.Notebook.transform.Find(name);
            if (sectionGameObject == null)
            {
                Debug.LogWarning($"No section named {sectionGameObject}");
                return;
            }
            if (!sectionGameObject.TryGetComponent(out NotebookSection section))
            {
                Debug.LogWarning($"Object {sectionGameObject} does not contain a {typeof(NotebookSection).Name} component");
                return;
            }
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

        private void OpenNewEntryPopup(string text, Func<bool> until = null)
        {
            Singletons.Main.Sound.Play("notebook_popup");
            var options = new MessagePanelOptions
            {
                Text = text,
                Location = MessagePanelLocation.TopRight,
                Timeout = 10f,
                Until = until
            };
            Singletons.Main.Interface.OpenMessagePanel(options);
        }

        public void AddArchiveEntryById(int id)
        {
            if (this._archive.AddEntry(new ArchiveCollectedEntry { CollectionEntryId = id }))
            {
                this.OpenNewEntryPopup(
                    $"A new finding was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Findings")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookArchive);
            }
            else
            {
                Debug.LogWarning($"No archive entry found with id `{id}`");
            }
        }
        public void AddArchiveEntryByTitle(string title)
        {
            if (this._archive.AddEntryByTitle(title))
            {
                this.OpenNewEntryPopup(
                    $"A new finding was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Findings")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookArchive);
            }
            else
            {
                Debug.LogWarning($"No archive entry found with title `{title}`");
            }
        }
        public void RemoveArchiveEntryById(int id)
        {
            this._archive.RemoveEntry(new ArchiveCollectedEntry { CollectionEntryId = id });
        }
        public void RemoveArchiveEntryByTitle(string title)
        {
            this._archive.RemoveEntryByTitle(title);
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
            if (this._journal.AddEntry(new JournalCollectedEntry { CollectionEntryId = id }))
            {
                this.OpenNewEntryPopup(
                    $"A new journal entry was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Journal")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookJournal);
            }
            else
            {
                Debug.LogWarning($"No journal entry found with id `{id}`");
            }
        }
        public void AddJournalEntryByTitle(string title)
        {
            if (this._journal.AddEntryByTitle(title))
            {
                this.OpenNewEntryPopup(
                    $"A new journal entry was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Journal")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookJournal);
            }
            else
            {
                Debug.LogWarning($"No journal entry found with title `{title}`");
            }
        }
        public void RemoveJournalEntryById(int id)
        {
            this._journal.RemoveEntry(new JournalCollectedEntry { CollectionEntryId = id });
        }
        public void RemoveJournalEntryByTitle(string title)
        {
            this._journal.RemoveEntryByTitle(title);
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
            if (this._maps.AddEntry(new MapsCollectedEntry { CollectionEntryId = id }))
            {
                this.OpenNewEntryPopup(
                    $"A new map was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Maps")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookMaps);
            }
            else
            {
                Debug.LogWarning($"No maps entry found with id `{id}`");
            }
        }
        public void AddMapsEntryByTitle(string title)
        {
            if (this._maps.AddEntryByTitle(title))
            {
                this.OpenNewEntryPopup(
                    $"A new map was added to the notebook. Press {Singletons.Main.Input.GetBinding("Player/Toggle Notebook Maps")} to see.",
                    () => Singletons.Main.Input.PlayerToggledNotebookMaps);
            }
            else
            {
                Debug.LogWarning($"No maps entry found with title `{title}`");
            }
        }
        public void RemoveMapsEntryById(int id)
        {
            this._maps.RemoveEntry(new MapsCollectedEntry { CollectionEntryId = id });
        }
        public void RemoveMapsEntryByTitle(string title)
        {
            this._maps.RemoveEntryByTitle(title);
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
