using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ANotebookCollectionTable<EntryType> : DatabaseTable<EntryType>
        where EntryType : ANotebookCollectionEntry
    {
        public void OnEnable()
        {
            this.Plugins = new List<ADatabaseTablePlugin> {
                ScriptableObject.CreateInstance<DatabaseTablePluginAutoIncrementId>()
            };
        }

        public EntryType FindOneByTitle(string title)
        {
            return this.FindOne(entry => entry.Title == title);
        }
    }
}
