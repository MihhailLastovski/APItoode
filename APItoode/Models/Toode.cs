namespace ToodeAPI.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public Toode() { }
        public Toode(string name, double price, bool isActive)
        {
            Name = name;
            Price = price;
            IsActive = isActive;
        }
    }
}
