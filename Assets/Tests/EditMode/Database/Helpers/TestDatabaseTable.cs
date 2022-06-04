using ProjectWendigo;

public class TestDatabaseEntry : IDatabaseEntry
{
    public string Name;
}

public class TestDatabaseTable : ATestDatabaseTable<TestDatabaseEntry>
{
}