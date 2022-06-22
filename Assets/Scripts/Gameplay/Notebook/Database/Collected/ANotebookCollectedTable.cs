using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ANotebookCollectedTable<EntryType> : DatabaseTable<EntryType>
        where EntryType : ANotebookCollectedEntry
    {
        public void OnEnable()
        {
            var uniqueIdPlugin = ScriptableObject.CreateInstance<DatabaseTablePluginUniqueField>();
            uniqueIdPlugin.FieldName = "CollectionEntryId";

            this.Plugins = new List<ADatabaseTablePlugin>
            {
                uniqueIdPlugin
            };
        }
    }
}
