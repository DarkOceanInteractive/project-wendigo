using UnityEngine;

namespace ProjectWendigo
{
    [System.Serializable]
    public abstract class ANotebookCollectionEntry : IDatabaseEntry, IDatabaseEntryAutoIncrementId
    {
        public string Title;
        [field: ReadOnly, SerializeField] public int Id { get; set; }
    }
}
