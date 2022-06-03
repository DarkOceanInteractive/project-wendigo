using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTablePlugin : ScriptableObject
    {
        public virtual void OnInsert<EntryType>(DatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseTableEntry
        { }

        public virtual void OnBeforeRemove<EntryType>(DatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseTableEntry
        { }

        public virtual void OnBeforeClear<EntryType>(DatabaseTable<EntryType> table)
            where EntryType : class, IDatabaseTableEntry
        { }

        public virtual void OnBeforeUpdate<EntryType>(DatabaseTable<EntryType> table, EntryType entry)
            where EntryType : class, IDatabaseTableEntry
        { }

        public virtual void OnInspectorUpdate<EntryType>(DatabaseTable<EntryType> table)
            where EntryType : class, IDatabaseTableEntry
        { }
    }
}
