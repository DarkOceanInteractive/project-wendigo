using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public class JournalCollectionEntry : ANotebookCollectionEntry
    {
        [TextArea(3, 10)]
        public string Content;
    }
}
