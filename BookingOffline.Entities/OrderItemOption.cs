namespace BookingOffline.Entities
{
    public class OrderItemOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Value { get; set; }

        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }
    }
}
