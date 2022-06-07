using UnityEngine;
using ProjectWendigo.Database.Extensions.Save;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewMapsCollectedTable", menuName = "Notebook/Maps/Table/Collected")]
    public class MapsCollectedTable : ANotebookCollectedTable<MapsCollectedEntry>
    {
        [ContextMenu("Save")]
        private void SaveToFile() => this.Save();
    }
}
