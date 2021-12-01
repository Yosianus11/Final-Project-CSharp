using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentsApi.Data;
using PaymentsApi.Models;



namespace PaymentsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public PaymentController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMovie()
        {
            var Movie = await _context.Payments.ToListAsync();
            return Ok(Movie);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(PaymentData data)
        {
            if (ModelState.IsValid)
            {
                await _context.Payments.AddAsync(data);
                await _context.SaveChangesAsync();
                var existItem = await _context.Payments.FirstOrDefaultAsync(x => x.paymentDetailId == data.paymentDetailId);
                return Ok(existItem);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Payments.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, PaymentData item)
        {
            if (id != item.paymentDetailId)
                return BadRequest();

            var existItem = await _context.Payments.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (existItem == null)
                return BadRequest();

            existItem.cardOwnerName = item.cardOwnerName;
            existItem.cardNumber = item.cardNumber;
            existItem.expirationDate = item.expirationDate;
            existItem.securityCode = item.securityCode;

            await _context.SaveChangesAsync();

            return Ok(existItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            var existItem = await _context.Payments.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (existItem == null)
                return NotFound();

            _context.Payments.Remove(existItem);
            await _context.SaveChangesAsync();

            return Ok(existItem);
        }


    }
}
