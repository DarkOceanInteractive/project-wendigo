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
            //var list = this.Entries.Cast<object>().ToList();
            //list.Sort((lhs, rhs) => string.Compare((lhs as ArchiveCollectionEntry).CategoryTitle, (rhs as ArchiveCollectionEntry).CategoryTitle));
            //this.Entries = list;
        }
    }
}
