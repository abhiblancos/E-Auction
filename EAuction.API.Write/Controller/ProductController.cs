using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EAuction.Domain.Seller;
using EAuction.Service.BidsService;
using EAuction.Service.Model;
using EAuction.Service.ProductService;
using EAuction.Service.SellerService;
using Microsoft.AspNetCore.Mvc;

namespace EAuction.API.Read.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductServ _productService;        
        public ProductController(ProductServ productService)
        {            
            _productService = productService;            
        }

        /// <summary>
        /// Get list of all product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/getAllproducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

   
    }
}
