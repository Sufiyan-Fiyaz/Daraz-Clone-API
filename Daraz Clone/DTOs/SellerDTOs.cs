namespace Daraz_Clone.DTOs
{
    public class SellerRequest
    {
        public string ShopName { get; set; }
        public string? BusinessLicense { get; set; }
        public string? BankAccount { get; set; }
    }
}
namespace Daraz_Clone.DTOs
{
    public class SellerResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string ShopName { get; set; }
        public string? BusinessLicense { get; set; }
        public string? BankAccount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
