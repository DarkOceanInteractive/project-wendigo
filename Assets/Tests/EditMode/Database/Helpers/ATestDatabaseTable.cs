using System.Collections.Generic;
using ProjectWendigo;

public abstract class ATestDatabaseTable<EntryType> : ADatabaseTable<EntryType>
    where EntryType : class, IDatabaseEntry
{
    public new List<EntryType> Entries => base.Entries;
    public new List<ADatabaseTablePlugin> Plugins => base.Plugins;
}
