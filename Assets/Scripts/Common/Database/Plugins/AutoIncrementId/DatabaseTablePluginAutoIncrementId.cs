using UnityEngine;

namespace ProjectWendigo
{
    public class DatabasePluginAutoIncrementId : ADatabaseTablePlugin
    {
        [SerializeField] [ReadOnly] private int _lastId = 0;

        public override void OnInsert<EntryType>(ADatabaseTable<EntryType> table, EntryType entry)
        {
            ((IDatabaseEntryAutoIncrementId)entry).Id = this._lastId++;
        }

        public override void OnBeforeClear<EntryType>(ADatabaseTable<EntryType> table)
        {
            this._lastId = 0;
        }

        public override void OnInspectorUpdate<EntryType>(ADatabaseTable<EntryType> table)
        {
            this._lastId = 0;
            table.UpdateMany(
                _ => true,
                entry => ((IDatabaseEntryAutoIncrementId)entry).Id = this._lastId++
            );
        }
    }
}