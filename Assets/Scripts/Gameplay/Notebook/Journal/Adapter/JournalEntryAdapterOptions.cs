using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewJournalEntryAdapterOptions", menuName = "Notebook/Journal/Adapter Options")]
    public class JournalEntryAdapterOptions : NotebookElementAdapterOptions
    {
        public GameObject EntryPrefab;
    }
}
