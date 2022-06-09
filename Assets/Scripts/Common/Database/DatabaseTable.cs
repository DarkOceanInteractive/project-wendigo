using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ProjectWendigo
{
    public abstract class DatabaseTable : ADatabaseTable
    {
        protected override List<ADatabaseTablePlugin> Plugins { get; set; } = new List<ADatabaseTablePlugin>();

        public override int Count => this.Entries.Count;

        protected void OnValidate()
        {
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnInspectorUpdate(this);
        }

        public override bool Insert(IDatabaseEntry value)
        {
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                if (plugin.ValidateInsert(this, value) == false)
                    return false;
            this.Entries.Add(value);
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnInsert(this, value);
            return true;
        }

        public override List<IDatabaseEntry> GetAll()
        {
            return this.Entries.Cast<IDatabaseEntry>().ToList();
        }

        public override IDatabaseEntry FindOne(Func<IDatabaseEntry, bool> query)
        {
            foreach (IDatabaseEntry entry in this.Entries)
            {
                if (query(entry))
                    return entry;
            }
            return null;
        }

        public override List<IDatabaseEntry> FindMany(Func<IDatabaseEntry, bool> query)
        {
            var result = new List<IDatabaseEntry>();
            foreach (IDatabaseEntry entry in this.Entries)
            {
                if (query(entry))
                    result.Add(entry);
            }
            return result;
        }

        public override bool RemoveOne(Func<IDatabaseEntry, bool> query)
        {
            foreach (IDatabaseEntry entry in this.Entries)
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeRemove(this, entry);
                    this.Entries.Remove(entry);
                    return true;
                }
            }
            return false;
        }

        public override int RemoveMany(Func<IDatabaseEntry, bool> query)
        {
            int removedCount = 0;
            foreach (IDatabaseEntry entry in this.Entries.Cast<IDatabaseEntry>().ToList())
            {
                if (query(entry))
                {
                    foreach (ADatabaseTablePlugin plugin in this.Plugins)
                        plugin.OnBeforeRemove(this, entry);
                    this.Entries.Remove(entry);
                    ++removedCount;
                }
            }
            return removedCount;
        }

        public override void Clear()
        {
            foreach (ADatabaseTablePlugin plugin in this.Plugins)
                plugin.OnBeforeClear(this);
            this.Entries = new List<IDatabaseEntry>();
        }

        public override bool UpdateOne(Func<IDatabaseEntry, bool> query, Action<IDatabaseEntry> update)
        {
            foreach (IDatabaseEntry entry in this.Entries)
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

        public override int UpdateMany(Func<IDatabaseEntry, bool> query, Action<IDatabaseEntry> update)
        {
            int updatedCount = 0;
            foreach (IDatabaseEntry entry in this.Entries)
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

    public abstract class DatabaseTable<EntryType> : DatabaseTable
        where EntryType : class, IDatabaseEntry
    {
        [SerializeField]
        private List<EntryType> _entries = new List<EntryType>();
        protected override IList Entries
        {
            get => this._entries;
            set => this._entries = value.Cast<EntryType>().ToList();
        }

        public void Insert(EntryType value)
        {
            base.Insert(value);
        }

        public new List<EntryType> GetAll()
        {
            return this.Entries.Cast<EntryType>().ToList();
        }

        public EntryType FindOne(Func<EntryType, bool> query)
        {
            return base.FindOne(obj => query(obj as EntryType)) as EntryType;
        }

        public List<EntryType> FindMany(Func<EntryType, bool> query)
        {
            return base.FindMany(obj => query(obj as EntryType)).Cast<EntryType>().ToList();
        }

        public bool RemoveOne(Func<EntryType, bool> query)
        {
            return base.RemoveOne(obj => query(obj as EntryType));
        }

        public int RemoveMany(Func<EntryType, bool> query)
        {
            return base.RemoveMany(obj => query(obj as EntryType));
        }

        public bool UpdateOne(Func<EntryType, bool> query, Action<EntryType> update)
        {
            return base.UpdateOne(obj => query(obj as EntryType), obj => update(obj as EntryType));
        }

        public int UpdateMany(Func<EntryType, bool> query, Action<EntryType> update)
        {
            return base.UpdateMany(obj => query(obj as EntryType), obj => update(obj as EntryType));
        }
    }
}