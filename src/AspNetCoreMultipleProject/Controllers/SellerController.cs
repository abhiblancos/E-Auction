using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AspNetCoreMultipleProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMultipleProject.Controllers
{
    [Route("api/[controller]")]
    public class SellerController : Controller
    {
        private readonly BusinessProvider _businessProvider;

        public SellerController(BusinessProvider businessProvider)
        {
            _businessProvider = businessProvider;
        }


        [HttpGet]
        [Route("/getAllSeller")]
        public async Task<IActionResult> GetAllSeller()
        {
            return Ok(await _businessProvider.GetAllSeller());
        }

        [HttpPost]
        [Route("/add-seller")]
        public async Task<IActionResult> AddSeller([FromBody] SellerInfoVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _businessProvider.AddSeller(value);

            return Created("/api/DataEventRecord", result);
        }

        [HttpGet]
        [Route("/getAllproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _businessProvider.GetAllProducts());
        }



        #region E-Auction Direct API
        [HttpPost]
        [Route("/add-products")]  //Direct use
        public async Task<IActionResult> AddProducts([FromBody] ProductInfoVM value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _businessProvider.AddProduct(value);

            return Created("/api/DataEventRecord", result);
        }

        [HttpGet]
        [Route("/show-bids/{productId}")]
        public async Task<IActionResult> ShowAllBids(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            return Ok(await _businessProvider.ShowAllBids(productId));
        }

        [HttpDelete("/delete/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            if (!await _businessProvider.ExistsProducts(id))
            {
                return NotFound($"Product with Id {id} does not exist");
            }

            await _businessProvider.DeleteProduct(id);

            return Ok();
        }
        #endregion


    }
}
