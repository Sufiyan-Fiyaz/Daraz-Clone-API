namespace Daraz_Clone.DTOs
{ 
public class CategoryRequest
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}
}