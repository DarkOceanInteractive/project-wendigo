using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveEntryAdapterOptions", menuName = "Notebook/Adapters/Archive Entry Adapter Options")]
    public class ArchiveEntryAdapterOptions : NotebookElementAdapterOptions
    {
        public GameObject TextElementPrefab;
    }
}