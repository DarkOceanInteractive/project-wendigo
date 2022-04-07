using UnityEngine;

namespace ProjectWendigo
{
    public enum ItemType
    {
        Collectables,
        Equipment,
        Default
    }

    public abstract class ItemObject : ScriptableObject
    {
        public int Id;
        public Sprite UiDisplay;
        public ItemType Type;
        [TextArea(15, 20)]
        public string Description;
    }
}
