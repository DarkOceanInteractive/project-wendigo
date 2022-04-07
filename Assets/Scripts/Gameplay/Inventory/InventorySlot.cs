namespace ProjectWendigo
{
    [System.Serializable]
    public class InventorySlot
    {
        public int Id;
        public Item Item;
        public int Amount;

        public InventorySlot()
        {
            this.Reset();
        }

        public InventorySlot(int id, Item item, int amount)
        {
            this.Id = id;
            this.Item = item;
            this.Amount = amount;
        }

        public void UpdateSlot(int id, Item item, int amount)
        {
            this.Id = id;
            this.Item = item;
            this.Amount = amount;
        }

        public void Reset()
        {
            this.Id = -1;
            this.Item = null;
            this.Amount = 0;
        }

        public void AddAmount(int value)
        {
            this.Amount += value;
        }
    }
}
