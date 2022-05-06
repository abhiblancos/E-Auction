using AspNetCoreMultipleProject.ViewModels;
using DomainModel;
using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMultipleProject
{
    public class BusinessProvider
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public BusinessProvider(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

      
        //E-Auction Methods
        public async Task<SellerInfoVM> AddSeller(SellerInfoVM value)
        {
            var sellerRecord = new SellerInfo
            {
                Address = value.Address,
                City = value.City,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Phone = value.Phone,
                PinCode = value.PinCode,
                State = value.State
            };



            var der = await _dataAccessProvider.AddSeller(sellerRecord);

            var result = new SellerInfoVM
            {
                Address = der.Address,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                State = der.State,
                SellerId = der.SellerId
            };

            return result;
        }

        public async Task<IEnumerable<SellerInfoVM>> GetAllSeller()
        {
            var data = await _dataAccessProvider.GetAllSeller();

            var results = data.Select(der => new SellerInfoVM
            {
                Address = der.Address,
                State = der.State,
                SellerId = der.SellerId,
                City = der.City,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
            });

            return results;
        }

        public async Task<ProductInfoVM> AddProduct(ProductInfoVM value)
        {
            var productRecord = new ProductInfo
            {
                BidEndDate = value.BidEndDate,
                Category = value.Category,
                DetailedDescription = value.DetailedDescription,
                CreatedDate = System.DateTime.UtcNow,
                IsDeleted = false,
                ProductName = value.ProductName,
                SellerId = value.SellerId,
                ShortDescription = value.ShortDescription,
                StartingPrice = value.StartingPrice,
            };



            var der = await _dataAccessProvider.AddProduct(productRecord);

            var result = new ProductInfoVM
            {
                BidEndDate = der.BidEndDate,
                Category = der.Category,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                IsDeleted = der.IsDeleted,
                ProductName = der.ProductName,
                SellerId = der.SellerId,
                ShortDescription = der.ShortDescription,
                StartingPrice = der.StartingPrice,
                ProductId = der.ProductId,
            };

            return result;
        }


        public async Task<IEnumerable<ProductInfoVM>> GetAllProducts()
        {
            var data = await _dataAccessProvider.GetAllProducts();

            var results = data.Select(der => new ProductInfoVM
            {
                BidEndDate = der.BidEndDate,
                StartingPrice = der.StartingPrice,
                ShortDescription = der.ShortDescription,
                SellerId = der.SellerId,
                ProductName = der.ProductName,
                ProductId = der.ProductId,
                IsDeleted = der.IsDeleted,
                DetailedDescription = der.DetailedDescription,
                CreatedDate = der.CreatedDate,
                Category = der.Category,

            });

            return results;
        }

        public async Task<BuyerInfoVM> AddBuyer(BuyerInfoVM value)
        {
            var buyerRecord = new BuyerInfo
            {
                Address = value.Address,
                BidAmount = value.BidAmount,
                City = value.City,
                CreatedDate = System.DateTime.Now,
                Email = value.Email,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Phone = value.Phone,
                PinCode = value.PinCode,
                ProductId = value.ProductId,
                State = value.State,
            };



            var der = await _dataAccessProvider.AddBuyer(buyerRecord);

            var result = new BuyerInfoVM
            {
                Address = der.Address,
                BidAmount = der.BidAmount,
                City = der.City,
                CreatedDate = System.DateTime.Now,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                ProductId = der.ProductId,
                State = der.State,
                BuyerId = der.BuyerId
            };

            return result;
        }

        public async Task<IEnumerable<BuyerInfoVM>> GetAllBuyer()
        {
            var data = await _dataAccessProvider.GetAllBuyer();

            var results = data.Select(der => new BuyerInfoVM
            {
                Address = der.Address,
                BidAmount = der.BidAmount,
                City = der.City,
                CreatedDate = System.DateTime.Now,
                Email = der.Email,
                FirstName = der.FirstName,
                LastName = der.LastName,
                Phone = der.Phone,
                PinCode = der.PinCode,
                ProductId = der.ProductId,
                State = der.State,
                BuyerId = der.BuyerId
            });

            return results;
        }

        public async Task UpdateBid(int productId, string buyerEmailId, double newBidAmt)
        {
            var buyerExists = GetAllBuyer().Result.Where(a => a.ProductId == productId && a.Email == buyerEmailId).SingleOrDefault();
            var productInfo = GetAllProducts().Result.Where(a => a.ProductId == productId).SingleOrDefault();
            if (productInfo == null)
            {
                throw new Exception("Product is not exists.");
            }
            if (buyerExists == null)
            {
                throw new Exception("This buyer is not exists.");
            }
            if (productInfo != null && productInfo.StartingPrice > newBidAmt)
            {
                throw new Exception("Bid Amount is not less then the product starting price.");
            }
            if (productInfo != null && productInfo.BidEndDate < System.DateTime.Now)
            {
                throw new Exception("Bid end date is expired.");
            }


            if (buyerExists != null)
            {
                await _dataAccessProvider.UpdateBid(productId, buyerEmailId, newBidAmt);

            }

        }

        public async Task<BidsInfoVM> ShowAllBids(int productId)
        {
            BidsInfoVM result = new BidsInfoVM();
            ProductInfoVM productInfoVM = new ProductInfoVM();
            List<BuyerInfoVM> buyerInfoVM = new List<BuyerInfoVM>();
            var productInfo = await _dataAccessProvider.GetProductById(productId);
            var buyerInfo = await _dataAccessProvider.GetAllBidsByProductId(productId);

            if (productInfo!=null)
            {
                productInfoVM.BidEndDate = productInfo.BidEndDate;
                productInfoVM.Category = productInfo.Category;
                productInfoVM.CreatedDate = productInfo.CreatedDate;
                productInfoVM.DetailedDescription = productInfo.DetailedDescription;
                productInfoVM.IsDeleted = productInfo.IsDeleted;
                productInfoVM.ProductId = productInfo.ProductId;
                productInfoVM.ProductName = productInfo.ProductName;
                productInfoVM.SellerId = productInfo.SellerId;
                productInfoVM.ShortDescription = productInfo.ShortDescription;
                productInfoVM.StartingPrice = productInfo.StartingPrice;
              
            }

            if (buyerInfo!=null && buyerInfo.Count>0)
            {
                foreach (var item in buyerInfo)
                {
                    BuyerInfoVM buyer = new BuyerInfoVM();
                    buyer.Address = item.Address;
                    buyer.BidAmount = item.BidAmount;
                    buyer.BuyerId = item.BuyerId;
                    buyer.City = item.City;
                    buyer.CreatedDate = item.CreatedDate;
                    buyer.Email = item.Email;
                    buyer.FirstName = item.FirstName;
                    buyer.LastName = item.LastName;
                    buyer.Phone = item.Phone;
                    buyer.PinCode = item.PinCode;
                    buyer.ProductId = item.ProductId;
                    buyer.State = item.State;
                    buyerInfoVM.Add(buyer);
                }
            }

            result.buyerInfoVM = buyerInfoVM;
            result.productInfoVM = productInfoVM;
            return result;
        }

        public async Task<bool> ExistsProducts(long id)
        {
            return await _dataAccessProvider.ExistsProducts(id);
        }

        public async Task DeleteProduct(long productId)
        {
            await _dataAccessProvider.DeleteProduct(productId);
        }
    }
}