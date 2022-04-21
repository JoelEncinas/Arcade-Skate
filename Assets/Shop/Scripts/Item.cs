public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Item(int iD, string title, string description, int price)
    {
        ID = iD;
        Title = title;
        Description = description;
        Price = price;
    }
}
