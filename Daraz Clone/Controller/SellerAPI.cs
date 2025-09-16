using Daraz_Clone.Data;
using Daraz_Clone.DTOs;
using Daraz_Clone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Daraz_Clone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SellersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SellersController(AppDbContext context)
        {
            _context = context;
        }

        //  Get all sellers (public)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerResponse>>> GetSellers()
        {
            var sellers = await _context.Sellers
                .Include(s => s.User)
                .ToListAsync();

            return Ok(sellers.Select(s => new SellerResponse
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User.FullName,
                Email = s.User.Email,
                ShopName = s.ShopName,
                BusinessLicense = s.BusinessLicense,
                BankAccount = s.BankAccount,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            }));
        }

        //  Get seller by Id (public)
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerResponse>> GetSeller(int id)
        {
            var seller = await _context.Sellers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seller == null) return NotFound();

            return Ok(new SellerResponse
            {
                Id = seller.Id,
                UserId = seller.UserId,
                UserName = seller.User.FullName,
                Email = seller.User.Email,
                ShopName = seller.ShopName,
                BusinessLicense = seller.BusinessLicense,
                BankAccount = seller.BankAccount,
                CreatedAt = seller.CreatedAt,
                UpdatedAt = seller.UpdatedAt
            });
        }

        //  Create seller (only once per user)
        [HttpPost]
        [Authorize(Roles = "seller")]
        public async Task<ActionResult<SellerResponse>> CreateSeller(SellerRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // check if seller already exists
            if (await _context.Sellers.AnyAsync(s => s.UserId == userId))
                return BadRequest("You already have a seller profile.");

            var seller = new Seller
            {
                UserId = userId,
                ShopName = request.ShopName,
                BusinessLicense = request.BusinessLicense,
                BankAccount = request.BankAccount,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeller), new { id = seller.Id }, new SellerResponse
            {
                Id = seller.Id,
                UserId = seller.UserId,
                ShopName = seller.ShopName,
                BusinessLicense = seller.BusinessLicense,
                BankAccount = seller.BankAccount,
                CreatedAt = seller.CreatedAt,
                UpdatedAt = seller.UpdatedAt
            });
        }

        //  Update seller (only own profile)
        [HttpPut("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<IActionResult> UpdateSeller(int id, SellerRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return NotFound();

            if (seller.UserId != userId)
                return Forbid("You can only update your own seller profile.");

            seller.ShopName = request.ShopName;
            seller.BusinessLicense = request.BusinessLicense;
            seller.BankAccount = request.BankAccount;
            seller.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new SellerResponse
            {
                Id = seller.Id,
                UserId = seller.UserId,
                ShopName = seller.ShopName,
                BusinessLicense = seller.BusinessLicense,
                BankAccount = seller.BankAccount,
                CreatedAt = seller.CreatedAt,
                UpdatedAt = seller.UpdatedAt
            });
        }

        //  Delete seller (only own profile)
        [HttpDelete("{id}")]
        [Authorize(Roles = "seller")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null) return NotFound();

            if (seller.UserId != userId)
                return Forbid("You can only delete your own seller profile.");

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
