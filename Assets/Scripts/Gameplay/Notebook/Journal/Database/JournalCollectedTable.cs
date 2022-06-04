using UnityEngine;
using ProjectWendigo.Database.Extensions.Save;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewJournalCollectedTable", menuName = "Notebook/Journal/Table/Collected")]
    public class JournalCollectedTable : ANotebookCollectedTable<JournalCollectedEntry>
    {
        [ContextMenu("Save")]
        private void SaveToFile() => this.Save();
    }
}
