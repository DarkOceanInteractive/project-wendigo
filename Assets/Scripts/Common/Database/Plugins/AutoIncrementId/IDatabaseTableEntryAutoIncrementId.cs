namespace ProjectWendigo
{
    public interface IDatabaseTableEntryAutoIncrementId : IDatabaseTableEntry
    {
        public int Id { get; set; }
    }
}
