namespace ToodeAPI.Models
{
    public class Toode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public int Kogus { get; set; }
        public Toode() { }
        public Toode(string name, double price, bool isActive, int kogus)
        {
            Name = name;
            Price = price;
            IsActive = isActive;
            Kogus=kogus;
        }
    }
}
