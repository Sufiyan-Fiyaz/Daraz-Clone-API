using Daraz_Clone.Models;
using Microsoft.AspNetCore.Mvc;
using Daraz_Clone.Services;

namespace Daraz_Clone.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _emailService;

        public EmailController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-thankyou")]
        public async Task<IActionResult> SendThankYou([FromBody] EmailRequest request)
        {
            try
            {
                string subject = "Thank You for Signing Up!";
                string body = $"Hello {request.FullName},\n\nThank you for signing up with us!\n\n- Your Store Team";

                // 👇 Ab sirf 3 arguments
                await _emailService.SendEmailAsync(request.Email, subject, body);

                return Ok(new { message = "Email sent successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error sending email", error = ex.Message });
            }
        }
    }

    public class EmailRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
