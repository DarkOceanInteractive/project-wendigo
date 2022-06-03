using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewArchiveDatabase", menuName = "Notebook/Archive/Database")]
    public class ArchiveDatabase : INotebookDatabase
    {
        [HideInInspector]
        public override List<object> Entries => this._entries.ToList<object>();

        [SerializeField]
        private List<ArchiveEntry> _entries;

        public override void AddEntry(object entry)
        {
            this._entries.Add(entry as ArchiveEntry);
        }

        public override void SetEntries(List<object> entries)
        {
            this._entries = entries.Cast<ArchiveEntry>().ToList();
        }
    }
}
