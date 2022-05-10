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

namespace EAuction.API.Read.Controllers
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
        /// Returns list of all Buyer
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Service.BuyerModels.BuyerInfo>),200)]
        [HttpGet]
        [Route("/getAllBuyer")]
        public async Task<IActionResult> GetAllBuyer()
        {
            return Ok(await _buyerService.GetAllBuyer());
        }
       
    }
}
