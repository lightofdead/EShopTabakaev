using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase;
using DataBase.Models;
using Interfaces.Repositories;
using DataBase.Repositories;
using DTO.AutoMapper;
using DTO.Models.Products;

namespace tabakaevAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsForBuyController : ControllerBase
    {
        private ProductsRepository _repo = new ProductsRepository();
        private ProductsForBuyRepository _repoBuy = new ProductsForBuyRepository();
        public ProductsForBuyController()
        {
        }

        // Delete
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _repoBuy.Delete(id);

            return new JsonResult(result);
        }

        // Get all
        [HttpGet()]
        public async Task<JsonResult> GetAll()
        {
            var productsForBuy = (await _repoBuy.GetModels())
                .Select(e => AutoMapperDTO.Mapper.Map<ProductForBuyDTO>(e))
                .ToList();

            var result = (await _repo.GetModels())
                .ToList()
                .Join(productsForBuy, e => e.Id, e => e.Product, (inner, outer) => inner)
                .Select(e => AutoMapperDTO.Mapper.Map<Product>(e))
                .ToList();

            return new JsonResult(Ok(result));
        }

        [HttpPost]
        public async Task<JsonResult> Create(ProductForBuyDTO model)
        {
            var elem = AutoMapperDTO.Mapper.Map<ProductForBuy>(model);
            if (elem.Id == Guid.Empty)
            {
                elem.Id = Guid.NewGuid();
            }

            var result = await _repoBuy.Create(elem);

            return new JsonResult(Ok(result));
        }

    }
}
