namespace ProjectWendigo
{
    [System.Serializable]
    public class Item
    {
        public string Name;
        public int Id;

        public Item(ItemObject item)
        {
            this.Name = item.name;
            this.Id = item.Id;
        }
    }
}
