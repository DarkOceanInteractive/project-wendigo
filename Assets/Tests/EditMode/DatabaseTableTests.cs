using System.Collections.Generic;
using NUnit.Framework;
using ProjectWendigo;

class DatabaseTableEntryTest : IDatabaseTableEntry
{
    public string Name;
}

class PublicDatabaseTableTest<EntryType> : DatabaseTable<EntryType>
    where EntryType : class, IDatabaseTableEntry
{
    public List<EntryType> entries => this.Entries;
}

public class DatabaseTableTests
{
    [Test]
    public void InsertOne()
    {
        var table = new PublicDatabaseTableTest<DatabaseTableEntryTest>();
        table.Insert(new DatabaseTableEntryTest { Name = "Test" });
        Assert.AreEqual(table.entries.Count, 1);
        Assert.AreEqual(table.entries[0].Name, "Test");
    }
}
