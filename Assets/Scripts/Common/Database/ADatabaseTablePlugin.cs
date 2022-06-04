using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTablePlugin : ScriptableObject
    {
        public virtual void OnInsert(ADatabaseTable table, object entry)
        { }

        public virtual void OnBeforeRemove(ADatabaseTable table, object entry)
        { }

        public virtual void OnBeforeClear(ADatabaseTable table)
        { }

        public virtual void OnBeforeUpdate(ADatabaseTable table, object entry)
        { }

        public virtual void OnInspectorUpdate(ADatabaseTable table)
        { }
    }
}
