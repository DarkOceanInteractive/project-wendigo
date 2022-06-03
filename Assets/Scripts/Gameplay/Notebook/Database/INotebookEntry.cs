namespace ProjectWendigo
{
    [System.Serializable]
    public abstract class INotebookEntry
    {
        public string Title;
        [ReadOnly] public int Id;
    }
}
