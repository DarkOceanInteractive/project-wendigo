using NUnit.Framework;
using UnityEngine;

public class DatabaseTableTests
{
    [Test]
    public void InsertOne()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        table.Insert(new TestDatabaseEntry { Name = "Test" });
        Assert.AreEqual(1, table.Entries.Count);
        Assert.AreEqual("Test", table.Entries[0].Name);
    }

    [Test]
    public void InsertMany()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        table.Insert(new TestDatabaseEntry { Name = "Test1" });
        table.Insert(new TestDatabaseEntry { Name = "Test2" });
        table.Insert(new TestDatabaseEntry { Name = "Test3" });
        Assert.AreEqual(3, table.Entries.Count);
        Assert.AreEqual("Test1", table.Entries[0].Name);
        Assert.AreEqual("Test2", table.Entries[1].Name);
        Assert.AreEqual("Test3", table.Entries[2].Name);
    }

    [Test]
    public void FindOne()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        Assert.AreEqual(test1, table.FindOne(entry => entry.Name == "Test1"));
        Assert.AreEqual(test2, table.FindOne(entry => entry.Name == "Test2"));
        Assert.AreEqual(test3, table.FindOne(entry => entry.Name == "Test3"));
        Assert.AreEqual(null, table.FindOne(entry => false));
        Assert.AreEqual(test1, table.FindOne(entry => entry.Name == "Test1" || entry.Name == "Test3"));
    }

    [Test]
    public void FineMany()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        var subset = table.FindMany(entry => entry.Name == "Test1" || entry.Name == "Test3");
        Assert.AreEqual(2, subset.Count);
        Assert.AreEqual(test1, subset[0]);
        Assert.AreEqual(test3, subset[1]);
        Assert.IsEmpty(table.FindMany(entry => false));
    }

    [Test]
    public void RemoveOne()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        Assert.AreEqual(table.RemoveOne(entry => entry.Name == "Test1"), true);
        Assert.AreEqual(2, table.Entries.Count);
        Assert.AreEqual(test2, table.Entries[0]);
        Assert.AreEqual(test3, table.Entries[1]);
        Assert.IsFalse(table.RemoveOne(entry => false));
        Assert.AreEqual(2, table.Entries.Count);
    }

    [Test]
    public void RemoveMany()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        Assert.AreEqual(2, table.RemoveMany(entry => entry.Name == "Test1" || entry.Name == "Test3"));
        Assert.AreEqual(1, table.Entries.Count);
        Assert.AreEqual(test2, table.Entries[0]);
        Assert.AreEqual(0, table.RemoveMany(entry => false));
    }

    [Test]
    public void Clear()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        table.Clear();
        Assert.AreEqual(0, table.Count);
    }

    [Test]
    public void UpdateOne()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        Assert.IsTrue(table.UpdateOne(entry => entry.Name == "Test1", entry => entry.Name = "Test1U"));
        Assert.AreEqual("Test1U", test1.Name);
        Assert.IsFalse(table.UpdateOne(entry => false, entry => { }));
    }

    [Test]
    public void UpdateMany()
    {
        var table = ScriptableObject.CreateInstance<TestDatabaseTable>();
        var test1 = new TestDatabaseEntry { Name = "Test1" };
        var test2 = new TestDatabaseEntry { Name = "Test2" };
        var test3 = new TestDatabaseEntry { Name = "Test3" };
        table.Insert(test1);
        table.Insert(test2);
        table.Insert(test3);
        Assert.AreEqual(2, table.UpdateMany(entry => entry.Name == "Test1" || entry.Name == "Test3", entry => entry.Name = $"{entry.Name}U"));
        Assert.AreEqual(test1.Name, "Test1U");
        Assert.AreEqual(test3.Name, "Test3U");
        Assert.AreEqual(0, table.UpdateMany(entry => false, entry => { }));
    }
}
