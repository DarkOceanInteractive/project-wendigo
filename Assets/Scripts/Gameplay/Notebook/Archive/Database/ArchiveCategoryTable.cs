using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveCategoryTable", menuName = "Notebook/Archive/Table/Category")]
    public class ArchiveCategoryTable : DatabaseTable<ArchiveCategoryEntry>
    {
        public void OnEnable()
        {
            this.Plugins = new List<ADatabaseTablePlugin> {
                ScriptableObject.CreateInstance<DatabaseTablePluginAutoIncrementId>()
            };
        }
    }
}
