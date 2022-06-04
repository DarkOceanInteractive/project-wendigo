using UnityEngine;
using ProjectWendigo;

public class TestAutoIncrementDatabaseEntry : IDatabaseEntryAutoIncrementId
{
    public string Name;
    public int Id { get; set; }
}

public class TestAutoIncrementDatabaseTable : ATestDatabaseTable<TestAutoIncrementDatabaseEntry>
{
    public void OnEnable()
    {
        base.Plugins.Add(ScriptableObject.CreateInstance<DatabaseTablePluginAutoIncrementId>());
    }
}