using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewDatabasePluginAutoIncrementId", menuName = "Database/Plugins/AutoIncrementId")]
    public class DatabaseTablePluginAutoIncrementId : ADatabaseTablePlugin
    {
        [SerializeField] [ReadOnly] private int _lastId = 0;

        public override void OnInsert(ADatabaseTable table, IDatabaseEntry entry)
        {
            ((IDatabaseEntryAutoIncrementId)entry).Id = this._lastId++;
        }

        public override void OnBeforeClear(ADatabaseTable table)
        {
            this._lastId = 0;
        }

        public override void OnInspectorUpdate(ADatabaseTable table)
        {
            this._lastId = 0;
            table.UpdateMany(
                _ => true,
                entry => ((IDatabaseEntryAutoIncrementId)entry).Id = this._lastId++
            );
        }
    }
}