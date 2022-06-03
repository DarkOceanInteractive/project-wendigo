using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public class DatabaseTable<EntryType> : ScriptableObject
        where EntryType : class, IDatabaseTableEntry
    {
        protected List<EntryType> Entries = new List<EntryType>();
        [SerializeField] protected List<ADatabaseTablePlugin> Plugins = new List<ADatabaseTablePlugin>();

        public int Count => this.Entries.Count;

        private void OnValidate()
        {
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnInspectorUpdate(this);
        }

        public void Insert(EntryType value)
        {
            this.Entries.Add(value);
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnInsert(this, value);
        }

        public EntryType FindOne(Func<EntryType, bool> query)
        {
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                    return entry;
            }
            return null;
        }

        public List<EntryType> FindMany(Func<EntryType, bool> query)
        {
            var result = new List<EntryType>();
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                    result.Add(entry);
            }
            return result;
        }

        public bool RemoveOne(Func<EntryType, bool> query)
        {
            int index = 0;
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeRemove(this, entry);
                    this.Entries.RemoveAt(index);
                    return true;
                }
                ++index;
            }
            return false;
        }

        public int RemoveMany(Func<EntryType, bool> query)
        {
            int removedCount = 0;
            int index = 0;
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeRemove(this, entry);
                    this.Entries.RemoveAt(index);
                    ++removedCount;
                }
                ++index;
            }
            return removedCount;
        }

        public void Clear()
        {
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnBeforeClear(this);
            this.Entries = new List<EntryType>();
        }

        public bool UpdateOne(Func<EntryType, bool> query, Action<EntryType> update)
        {
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeUpdate(this, entry);
                    update(entry);
                    return true;
                }
            }
            return false;
        }

        public int UpdateMany(Func<EntryType, bool> query, Action<EntryType> update)
        {
            int updatedCount = 0;
            foreach (EntryType entry in this.Entries)
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeUpdate(this, entry);
                    update(entry);
                    ++updatedCount;
                }
            }
            return updatedCount;
        }
    }
}