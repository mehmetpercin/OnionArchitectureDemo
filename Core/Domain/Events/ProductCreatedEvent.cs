namespace Domain.Events
{
    public class ProductCreatedEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
