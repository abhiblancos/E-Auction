using AspNetCoreMultipleProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly BusinessProvider _businessProvider;

        public BuyerController(BusinessProvider businessProvider)
        {
            _businessProvider = businessProvider;
        }

        [HttpGet]
        [Route("/getAllBuyer")]
        public async Task<IActionResult> GetAllBuyer()
        {
            return Ok(await _businessProvider.GetAllBuyer());
        }

        [HttpPost]
        [Route("/place-bid")]
        public async Task<IActionResult> PlaceBid([FromBody] BuyerInfoVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _businessProvider.AddBuyer(value);

            return Created("/api/DataEventRecord", result);
        }


        [HttpPut("/update-bid/{productId}/{buyerEmailId}/{newBidAmt}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            try
            {
                if (productId == 0)
                {
                    return BadRequest();
                }
                await _businessProvider.UpdateBid(productId, buyerEmailId, newBidAmt);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
           
        }

       
    }
}
