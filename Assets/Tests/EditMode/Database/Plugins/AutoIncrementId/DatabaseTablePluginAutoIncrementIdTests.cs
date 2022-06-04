using UnityEngine;
using NUnit.Framework;

public class DatabaseTablePluginAutoIncrementIdTests
{
    [Test]
    public void AutoIncrementIdOnInsert()
    {
        var table = ScriptableObject.CreateInstance<TestAutoIncrementDatabaseTable>();
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test0" });
        Assert.AreEqual(0, table.Entries[0].Id);
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test1" });
        Assert.AreEqual(1, table.Entries[1].Id);
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test2" });
        Assert.AreEqual(2, table.Entries[2].Id);
    }

    [Test]
    public void KeepIdAfterRemoval()
    {
        var table = ScriptableObject.CreateInstance<TestAutoIncrementDatabaseTable>();
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test0" });
        Assert.AreEqual(0, table.Entries[0].Id);
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test1" });
        Assert.AreEqual(1, table.Entries[1].Id);
        table.RemoveOne(entry => entry.Name == "Test1");
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test2" });
        Assert.AreEqual(2, table.Entries[1].Id);
    }

    [Test]
    public void ResetOnClear()
    {
        var table = ScriptableObject.CreateInstance<TestAutoIncrementDatabaseTable>();
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test0" });
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test1" });
        Assert.AreEqual(1, table.Entries[1].Id);
        table.Clear();
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test2" });
        Assert.AreEqual(0, table.Entries[0].Id);
    }

    [Test]
    public void ReassignOnInspectorUpdate()
    {
        var table = ScriptableObject.CreateInstance<TestAutoIncrementDatabaseTable>();
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test0" });
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test1" });
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test2" });
        table.RemoveOne(entry => entry.Name == "Test1");
        table.Insert(new TestAutoIncrementDatabaseEntry { Name = "Test4" });
        Assert.AreEqual(3, table.Entries[2].Id);
        table.Plugins[0].OnInspectorUpdate(table);
        Assert.AreEqual(0, table.Entries[0].Id);
        Assert.AreEqual(1, table.Entries[1].Id);
        Assert.AreEqual(2, table.Entries[2].Id);
    }
}
