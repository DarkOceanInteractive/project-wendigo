using UnityEngine;
using ProjectWendigo.Database.Extensions.Save;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveCollectedTable", menuName = "Notebook/Archive/Table/Collected")]
    public class ArchiveCollectedTable : ANotebookCollectedTable<ArchiveCollectedEntry>
    {
        [ContextMenu("Save")]
        private void SaveToFile() => this.Save();
    }
}
