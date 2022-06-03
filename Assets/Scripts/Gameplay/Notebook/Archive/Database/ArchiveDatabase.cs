using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveDatabase", menuName = "Notebook/Archive/Database")]
    public class ArchiveDatabase : ANotebookDatabase<ArchiveEntry>
    {
        protected override void OnValidate()
        {
            this._entries.Sort((lhs, rhs) => string.Compare(lhs.Category, rhs.Category));
            base.OnValidate();
        }
    }
}
