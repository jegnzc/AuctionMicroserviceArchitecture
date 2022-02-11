using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionPaymentsController : ControllerBase
    {
        private readonly PaymentServiceContext _context;

        public AuctionPaymentsController(PaymentServiceContext context)
        {
            _context = context;
        }

        // GET: api/AuctionPayments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionPayment>>> GetAuctionPayment()
        {
            Logger.Instance.LogMessage("Getting auction payment");
            return await _context.AuctionPayment.ToListAsync();
        }

        // GET: api/AuctionPayments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionPayment>> GetAuctionPayment(int id)
        {

            Logger.Instance.LogMessage($"Getting auction payment by ID where ID is {id}");
            var auctionPayment = await _context.AuctionPayment.FindAsync(id);

            if (auctionPayment == null)
            {
                return NotFound();
            }

            return auctionPayment;
        }

        // PUT: api/AuctionPayments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuctionPayment(int id, AuctionPayment auctionPayment)
        {

            Logger.Instance.LogMessage($"Update auction payment by ID where ID is {id}");
            if (id != auctionPayment.Id)
            {
                return BadRequest();
            }

            _context.Entry(auctionPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuctionPaymentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AuctionPayments
        [HttpPost]
        public async Task<ActionResult<AuctionPayment>> PostAuctionPayment(AuctionPayment auctionPayment)
        {
            Logger.Instance.LogMessage($"Insert auction payment where Auction ID is {auctionPayment.IdAuction}"); 
            _context.AuctionPayment.Add(auctionPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuctionPayment", new { id = auctionPayment.Id }, auctionPayment);
        }

        // DELETE: api/AuctionPayments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuctionPayment>> DeleteAuctionPayment(int id)
        {

            Logger.Instance.LogMessage($"Delete auction payment where Id is {id}");
            var auctionPayment = await _context.AuctionPayment.FindAsync(id);
            if (auctionPayment == null)
            {
                return NotFound();
            }

            _context.AuctionPayment.Remove(auctionPayment);
            await _context.SaveChangesAsync();

            return auctionPayment;
        }

        private bool AuctionPaymentExists(int id)
        {
            return _context.AuctionPayment.Any(e => e.Id == id);
        }
    }
}
