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

        private static bool GetReferenceResolverQuery(AReference reference, IDatabaseEntry entry, out Func<IDatabaseEntry, bool> query)
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

        public static IDatabaseEntry ResolveReference(this IDatabaseEntry entry, ReferenceToOne reference)
        {
            if (!ReferenceDatabaseExtension.GetReferenceResolverQuery(reference, entry, out Func<IDatabaseEntry, bool> query))
                return null;
            return reference.ForeignTable.FindOne(query);
        }

        public static List<IDatabaseEntry> ResolveReference(this IDatabaseEntry entry, ReferenceToMany reference)
        {
            if (!ReferenceDatabaseExtension.GetReferenceResolverQuery(reference, entry, out Func<IDatabaseEntry, bool> query))
                return default;
            return reference.ForeignTable.FindMany(query);
        }

        public static EntryType ResolveReference<EntryType>(this IDatabaseEntry entry, ReferenceToOne reference)
            where EntryType : class, IDatabaseEntry
        {
            return (entry as IDatabaseEntry).ResolveReference(reference) as EntryType;
        }

        public static List<EntryType> ResolveReference<EntryType>(this IDatabaseEntry entry, ReferenceToMany reference)
            where EntryType : class, IDatabaseEntry
        {
            return (entry as IDatabaseEntry).ResolveReference(reference).Cast<EntryType>().ToList();
        }
    }
}