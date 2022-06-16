using UnityEngine;

namespace ProjectWendigo
{
    public class InteractableLoreItem : AInteractable
    {
        public string ArchiveEntryTitle;

        public override void OnLookAt(GameObject target)
        {
            Singletons.Main.Interface.OpenMessagePanel("- Press F to examine -");
        }

        public override void OnLookAway(GameObject target)
        {
            Singletons.Main.Interface.CloseMessagePanel();
        }

        public override void OnInteract(GameObject target)
        {
            Singletons.Main.Notebook.AddArchiveEntryByTitle(this.ArchiveEntryTitle);
            Singletons.Main.Save.Save();
        }
    }
}