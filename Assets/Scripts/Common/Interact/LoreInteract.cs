using UnityEngine;

namespace ProjectWendigo
{
    public class LoreInteract : AInteractable
    {
        public string ArchiveEntryTitle;
        public string RelatedQuestName;

        [SerializeField] private bool _openArchive;

        public override void OnLookAt(GameObject target)
        {
            if (!Singletons.Main.Notebook.ArchiveHasEntry(this.ArchiveEntryTitle))
            {
                Singletons.Main.Interface.OpenMessagePanel("- Press F to examine -");
            }
        }

        public override void OnLookAway(GameObject target)
        {
            if (!Singletons.Main.Notebook.ArchiveHasEntry(this.ArchiveEntryTitle))
            {
                Singletons.Main.Interface.CloseMessagePanel();
            }
        }

        public override void OnInteract(GameObject target)
        {
            if (!Singletons.Main.Notebook.ArchiveHasEntry(this.ArchiveEntryTitle))
            {
                if (this.RelatedQuestName != "")
                    Singletons.Main.Quest.TryUpdateQuestProgress(this.RelatedQuestName, 1);
                Singletons.Main.Notebook.AddArchiveEntryByTitle(this.ArchiveEntryTitle);
                Singletons.Main.Interface.CloseMessagePanel();
                Singletons.Main.Save.Save();
                if (this._openArchive)
                {
                    Singletons.Main.Notebook.ToggleSection("Sections/Archive");
                }
            }
        }
    }
}
