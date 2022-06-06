using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveCategoryAdapterOptions", menuName = "Notebook/Archive/Category Adapter Options")]
    public class ArchiveCategoryAdapterOptions : NotebookElementAdapterOptions
    {
        public GameObject CategoryPrefab;
    }
}
