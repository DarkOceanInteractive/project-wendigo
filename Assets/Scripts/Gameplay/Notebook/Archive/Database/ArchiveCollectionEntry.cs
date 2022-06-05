using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public class ArchiveCollectionEntry : ANotebookCollectionEntry
    {
        public string CategoryTitle;
        public Sprite Image;
        [TextArea(3, 10)]
        public string Description;
    }
}
