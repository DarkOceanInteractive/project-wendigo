using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTablePlugin : ScriptableObject
    {
        public virtual void OnInsert<EntryType>(ADatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseEntry
        { }

        public virtual void OnBeforeRemove<EntryType>(ADatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseEntry
        { }

        public virtual void OnBeforeClear<EntryType>(ADatabaseTable<EntryType> table)
            where EntryType : class, IDatabaseEntry
        { }

        public virtual void OnBeforeUpdate<EntryType>(ADatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseEntry
        { }

        public virtual void OnInspectorUpdate<EntryType>(ADatabaseTable<EntryType> table)
            where EntryType : class, IDatabaseEntry
        { }
    }
}
