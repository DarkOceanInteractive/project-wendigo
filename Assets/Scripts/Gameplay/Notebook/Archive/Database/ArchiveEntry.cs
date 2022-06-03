using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public class ArchiveEntry : INotebookEntry
    {
        public string Category;
        public Sprite Image;
        [TextArea(3, 10)]
        public string Description;
    }
}
