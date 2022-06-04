using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo
{
    public abstract class ADatabaseTable : ScriptableObject
    {
        protected abstract IList Entries { get; set; }
        protected abstract List<ADatabaseTablePlugin> Plugins { get; set; }
        public abstract int Count { get; }

        public abstract void Insert(object value);

        public abstract List<object> GetAll();
        public abstract object FindOne(Func<object, bool> query);

        public abstract List<object> FindMany(Func<object, bool> query);

        public abstract bool RemoveOne(Func<object, bool> query);

        public abstract int RemoveMany(Func<object, bool> query);

        public abstract void Clear();

        public abstract bool UpdateOne(Func<object, bool> query, Action<object> update);

        public abstract int UpdateMany(Func<object, bool> query, Action<object> update);
    }
}
