using System.Reflection;

namespace ProjectWendigo.Database.Extensions.Reflection
{
    public static class ReflectionDatabaseExtension
    {
        public static object GetMemberValue(this IDatabaseEntry entry, string member)
        {
            MemberInfo[] membersInfo = entry.GetType().GetMember(member);
            foreach (MemberInfo memberInfo in membersInfo)
            {
                switch (memberInfo.MemberType)
                {
                    case MemberTypes.Field:
                        return ((FieldInfo)memberInfo).GetValue(entry);
                    case MemberTypes.Property:
                        return ((PropertyInfo)memberInfo).GetValue(entry);
                    default:
                        continue;
                }
            }
            return null;
        }

        public static bool TryGetMemberValue(this IDatabaseEntry entry, string member, out object value)
        {
            value = entry.GetMemberValue(member);
            return value != null;
        }
    }
}