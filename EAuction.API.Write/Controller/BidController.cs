using EAuction.Service.BidsService;
using EAuction.Service.BuyerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EAuction.API.Read.Controllers
{
    [Route("e-auction/api/v1/bid/[controller]")]
    public class BidController : Controller
    {
        
        private readonly BidService _bidService;

        public BidController(BuyerServ buyerService, BidService bidService)
        {
            
            _bidService = bidService;   
        }

        /// <summary>
        /// Show all bid
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/show-bids/{productId}")]
        public async Task<IActionResult> ShowAllBids(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            return Ok(await _bidService.ShowAllBids(productId));
        }
    }
}
