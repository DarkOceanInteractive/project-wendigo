using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewCursorSettings", menuName = "Cursor Settings")]
    public class CursorSettings : ScriptableObject
    {
        public Texture2D texture;
        public Vector2 hotspot;
    }
}