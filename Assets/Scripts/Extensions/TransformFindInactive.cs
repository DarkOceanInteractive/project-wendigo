using UnityEngine;

public static class TransformFindInactive
{
    /// <summary>
    /// Behaves the same way Transform.Find does, but adds the possibility
    /// to find an inactive child via the `includeInactive` parameter.
    /// </summary>
    public static Transform Find(this Transform transform, string name, bool includeInactive)
    {
        string[] names = name.Split(new char[] { '/' }, 2, System.StringSplitOptions.RemoveEmptyEntries);
        string directChildName = names[0];
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in children)
        {
            if (child.name == directChildName)
            {
                if (names.Length > 1)
                    return transform.Find(names[1], includeInactive);
                return child;
            }
        }
        return null;
    }
}
