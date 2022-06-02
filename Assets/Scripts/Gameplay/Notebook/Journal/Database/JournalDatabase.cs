using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectWendigo
{
    [CreateAssetMenu(fileName = "NewJournalDatabase", menuName = "Notebook/Journal/Database")]
    public class JournalDatabase : INotebookDatabase
    {
        [HideInInspector]
        public override List<object> Entries => this._entries.ToList<object>();

        [SerializeField]
        private List<JournalEntry> _entries;

        public override void AddEntry(object entry)
        {
            this._entries.Add(entry as JournalEntry);
        }

        public override void SetEntries(List<object> entries)
        {
            this._entries = entries.Cast<JournalEntry>().ToList();
        }
    }
}
