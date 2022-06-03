using UnityEngine;

namespace ProjectWendigo
{
    public class DatabasePluginAutoIncrementId : ADatabaseTablePlugin
    {
        [SerializeField] [ReadOnly] private int _lastId = 0;

        public override void OnInsert<EntryType>(DatabaseTable<EntryType> table, EntryType entry)
        {
            ((IDatabaseTableEntryAutoIncrementId)entry).Id = this._lastId++;
        }

        public override void OnBeforeClear<EntryType>(DatabaseTable<EntryType> table)
        {
            this._lastId = 0;
        }

        public override void OnInspectorUpdate<EntryType>(DatabaseTable<EntryType> table)
        {
            this._lastId = 0;
            table.UpdateMany(
                _ => true,
                entry => ((IDatabaseTableEntryAutoIncrementId)entry).Id = this._lastId++
            );
        }
    }
}