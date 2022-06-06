using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchivePageAdapterOptions", menuName = "Notebook/Archive/Page Adapter Options")]
    public class ArchivePageAdapterOptions : INotebookPageAdapterOptions
    {
        public ArchiveCategoryTable Categories;
        public ArchiveCategoryAdapter CategoryAdapter;
        public ArchiveCategoryAdapterOptions CategoryAdapterOptions;
        public GameObject CategoryTabPrefab;
    }
}