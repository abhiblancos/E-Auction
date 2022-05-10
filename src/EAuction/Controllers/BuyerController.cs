using EAuction.Domain.Buyer;
using EAuction.Service.BidsService;
using EAuction.Service.BuyerModels;
using EAuction.Service.BuyerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EAuction.API.Write.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly BuyerServ _buyerService;
        private readonly BidService _bidService;

        public BuyerController(BuyerServ buyerService, BidService bidService)
        {
            _buyerService = buyerService;
            _bidService = bidService;   
        }

        /// <summary>
        /// Place Bid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [HttpPost]
        [Route("/buyer")]
        public async Task<IActionResult> PlaceBid([FromBody] Service.BuyerModels.BuyerInfo value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _buyerService.AddBuyer(value);

            return Created("/api/DataEventRecord", result);
        }
       
    }
}
