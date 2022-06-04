using System.Linq;
using System.Collections.Generic;
using ProjectWendigo;

public abstract class ATestDatabaseTable<EntryType> : DatabaseTable<EntryType>
    where EntryType : class, IDatabaseEntry
{
    public new List<EntryType> Entries => base.Entries.Cast<EntryType>().ToList();
    public new List<ADatabaseTablePlugin> Plugins => base.Plugins;
}
