using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public class ArchiveCategoryEntry : IDatabaseEntry, IDatabaseEntryAutoIncrementId
    {
        public string Title;
        [field: ReadOnly, SerializeField] public int Id { get; set; }
    }
}
