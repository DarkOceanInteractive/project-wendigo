using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableLoreItem : AInteractable
    {
        public string ArchiveEntryTitle;

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
                Singletons.Main.Notebook.AddArchiveEntryByTitle(this.ArchiveEntryTitle);
                Singletons.Main.Interface.CloseMessagePanel();
                Singletons.Main.Save.Save();
            }
        }
    }
}