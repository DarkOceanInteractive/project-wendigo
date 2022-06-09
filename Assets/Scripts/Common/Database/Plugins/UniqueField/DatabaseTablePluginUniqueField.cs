using UnityEngine;
using ProjectWendigo.Database.Extensions.Reflection;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewDatabasePluginUniqueField", menuName = "Database/Plugins/UniqueField")]
    public class DatabaseTablePluginUniqueField : ADatabaseTablePlugin
    {
        public string FieldName;

        public override bool ValidateInsert(ADatabaseTable table, IDatabaseEntry entry)
        {
            if (!entry.TryGetMemberValue(this.FieldName, out object entryFieldValue))
            {
                Debug.LogWarning($"No field named {this.FieldName} found in entry of type {entry.GetType().Name}");
                return true;
            }
            return table.FindOne(it => it.GetMemberValue(this.FieldName).Equals(entryFieldValue)) == null;
        }
    }
}