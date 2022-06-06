using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveEntryAdapterOptions", menuName = "Notebook/Archive/Adapter Options")]
    public class ArchiveEntryAdapterOptions : NotebookElementAdapterOptions
    {
        public GameObject EntryPrefab;
    }
}
