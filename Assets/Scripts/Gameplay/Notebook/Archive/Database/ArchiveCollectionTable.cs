using System.Linq;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveCollectionTable", menuName = "Notebook/Archive/Table/Collection")]
    public class ArchiveCollectionTable : ANotebookCollectionTable<ArchiveCollectionEntry>
    {
        protected new void OnValidate()
        {
            base.OnValidate();
        }
    }
}
