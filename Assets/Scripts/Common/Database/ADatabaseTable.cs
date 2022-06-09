using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTable : ScriptableObject
    {
        protected abstract IList Entries { get; set; }
        public Type EntryType => this.Entries.GetType().GetGenericArguments()[0];
        protected abstract List<ADatabaseTablePlugin> Plugins { get; set; }
        public abstract int Count { get; }

        public abstract bool Insert(IDatabaseEntry value);

        public abstract List<IDatabaseEntry> GetAll();
        public abstract IDatabaseEntry FindOne(Func<IDatabaseEntry, bool> query);

        public abstract List<IDatabaseEntry> FindMany(Func<IDatabaseEntry, bool> query);

        public abstract bool RemoveOne(Func<IDatabaseEntry, bool> query);

        public abstract int RemoveMany(Func<IDatabaseEntry, bool> query);

        public abstract void Clear();

        public abstract bool UpdateOne(Func<IDatabaseEntry, bool> query, Action<IDatabaseEntry> update);

        public abstract int UpdateMany(Func<IDatabaseEntry, bool> query, Action<IDatabaseEntry> update);
    }
}
