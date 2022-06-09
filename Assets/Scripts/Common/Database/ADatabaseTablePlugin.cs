using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTablePlugin : ScriptableObject
    {
        public virtual bool ValidateInsert(ADatabaseTable table, IDatabaseEntry entry)
        {
            return true;
        }

        public virtual void OnInsert(ADatabaseTable table, IDatabaseEntry entry)
        { }

        public virtual void OnBeforeRemove(ADatabaseTable table, IDatabaseEntry entry)
        { }

        public virtual void OnBeforeClear(ADatabaseTable table)
        { }

        public virtual void OnBeforeUpdate(ADatabaseTable table, IDatabaseEntry entry)
        { }

        public virtual void OnInspectorUpdate(ADatabaseTable table)
        { }
    }
}
