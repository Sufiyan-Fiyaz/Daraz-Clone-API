public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Self reference
    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }

    // Navigation property
    public ICollection<Category> SubCategories { get; set; }
}
