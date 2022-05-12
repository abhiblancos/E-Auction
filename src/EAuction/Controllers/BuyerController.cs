using Confluent.Kafka;
using EAuction.Domain.Buyer;
using EAuction.Service.BidsService;
using EAuction.Service.BuyerModels;
using EAuction.Service.BuyerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAuction.API.Write.Controllers
{
    [Route("e-auction/api/v1/buyer/[controller]")]
    public class BuyerController : Controller
    {
        private readonly BuyerServ _buyerService;
        private readonly BidService _bidService;
        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "test";

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

            string message = JsonSerializer.Serialize(result);

            await SendBuyerRequest(topic, message);

            return Created("/api/DataEventRecord", result);
        }

        /// <summary>
        /// Returns list of all Buyer
        /// </summary>
        /// <returns></returns>
        //[ProducesResponseType(typeof(IEnumerable<Service.BuyerModels.BuyerInfo>), 200)]
        //[HttpGet]
        //[Route("/getAllBuyer")]
        //public async Task<IActionResult> GetAllBuyer()
        //{
        //    return Ok(await _buyerService.GetAllBuyer());
        //}


        private async Task<bool> SendBuyerRequest
        (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });


                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
