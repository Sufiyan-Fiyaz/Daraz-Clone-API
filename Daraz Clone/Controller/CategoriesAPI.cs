using Daraz_Clone.Data;
using Daraz_Clone.DTOs;
using Daraz_Clone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    // Get all categories
    // GET: /api/categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetCategories()
    {
        var categories = await _context.Categories.ToListAsync();

        return Ok(categories.Select(c => new CategoryResponse
        {
            Id = c.Id,
            Name = c.Name,
            ParentCategoryId = c.ParentCategoryId
        }));
    }

    // Create category (Admin only)
    // POST: /api/categories
    [HttpPost]
    [Authorize(Roles = "admin")] // keep consistent with update & delete
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            ParentCategoryId = request.ParentCategoryId
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId
        });
    }

    // Get single category by ID
    // GET: /api/categories/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        return Ok(new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId
        });
    }

    // Update category (Admin only)
    // PUT: /api/categories/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> UpdateCategory(int id, CategoryRequest request)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        category.Name = request.Name;
        category.ParentCategoryId = request.ParentCategoryId;

        await _context.SaveChangesAsync();
        return Ok(new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId
        });
    }

    //  Delete category (Admin only)
    // DELETE: /api/categories/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null)
            return NotFound();

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
