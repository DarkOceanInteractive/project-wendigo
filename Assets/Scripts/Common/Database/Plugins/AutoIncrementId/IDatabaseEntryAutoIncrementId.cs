namespace ProjectWendigo
{
    public interface IDatabaseEntryAutoIncrementId : IDatabaseEntry
    {
        public int Id { get; set; }
    }
}
