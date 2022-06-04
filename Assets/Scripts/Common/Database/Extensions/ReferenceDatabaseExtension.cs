using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectWendigo.Database.Extensions.Reference
{
    public abstract class AReference
    {
        public string KeyName;
        public DatabaseTable ForeignTable;
        public string ForeignKeyName;
    }

    public class ReferenceToOne : AReference
    { }
    public class ReferenceToMany : AReference
    { }

    public static class ReferenceDatabaseExtension
    {
        private static bool TryGetMemberValue(object entry, string member, out object value)
        {
            value = null;
            MemberInfo[] membersInfo = entry.GetType().GetMember(member);
            foreach (MemberInfo memberInfo in membersInfo)
            {
                switch (memberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        value = ((FieldInfo)memberInfo).GetValue(entry);
                        return true;
                    case MemberTypes.Property:
                        value = ((PropertyInfo)memberInfo).GetValue(entry);
                        return true;
                    default:
                        continue;
                }
            }
            return false;
        }

        private static bool GetReferenceResolverQuery(AReference reference, object entry, out Func<object, bool> query)
        {
            query = null;
            if (!ReferenceDatabaseExtension.TryGetMemberValue(entry, reference.KeyName, out object key))
                return false;
            query = foreignEntry =>
            {
                if (!ReferenceDatabaseExtension.TryGetMemberValue(foreignEntry, reference.ForeignKeyName, out object foreignKey))
                    return false;
                return key.Equals(foreignKey);
            };
            return true;
        }

        public static object ResolveReference(this DatabaseTable table, ReferenceToOne reference, object entry)
        {
            if (!ReferenceDatabaseExtension.GetReferenceResolverQuery(reference, entry, out Func<object, bool> query))
                return null;
            return reference.ForeignTable.FindOne(query);
        }

        public static List<object> ResolveReference(this DatabaseTable table, ReferenceToMany reference, object entry)
        {
            if (!ReferenceDatabaseExtension.GetReferenceResolverQuery(reference, entry, out Func<object, bool> query))
                return default;
            return reference.ForeignTable.FindMany(query);
        }

        public static EntryType ResolveReference<EntryType>(this DatabaseTable table, ReferenceToOne reference, EntryType entry)
            where EntryType : class, IDatabaseEntry
        {
            return table.ResolveReference(reference, entry as object) as EntryType;
        }

        public static List<EntryType> ResolveReference<EntryType>(this DatabaseTable table, ReferenceToMany reference, EntryType entry)
            where EntryType : class, IDatabaseEntry
        {
            return table.ResolveReference(reference, entry as object).Cast<EntryType>().ToList();
        }
    }
}