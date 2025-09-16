namespace Daraz_Clone.DTOs
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderUpdateDto
    {
        public string Status { get; set; }
    }

    public class OrderResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
