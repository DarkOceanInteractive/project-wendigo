using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewMapsEntryAdapterOptions", menuName = "Notebook/Maps/Adapter Options")]
    public class MapsEntryAdapterOptions : NotebookElementAdapterOptions
    {
        public GameObject EntryPrefab;
    }
}
