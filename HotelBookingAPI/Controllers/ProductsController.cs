using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase;
using DataBase.Models;
using Interfaces.Repositories;
using DataBase.Repositories;
using DTO.AutoMapper;
using DTO.Models.Products;
using System.Linq.Expressions;

namespace tabakaevAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductsRepository _repo = new ProductsRepository();
        private ProductsForBuyRepository _repoBuy = new ProductsForBuyRepository();
        public ProductsController()
        {
        }

        // Create/Edit
        [HttpPost]
        public async Task<JsonResult> Edit(ProductDTO model)
        {
            try 
            {
                 await _repo.Update(AutoMapperDTO.Mapper.Map<Product>(model));
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult(Ok());
        }

        [HttpPost]
        public async Task<JsonResult> Create(ProductDTO model)
        {
            var elem = AutoMapperDTO.Mapper.Map<Product>(model);
            var result = Guid.Empty;
            if (elem.Id == Guid.Empty) 
            {
                elem.Id = Guid.NewGuid();
            }
            var modelById = await _repo.Get(elem.Id);
            var modelByName = await _repo.GetByName(elem.Name ?? string.Empty);
            if (modelById == null && modelByName == null)
            {
                result = await _repo.Create(elem);
            }

            return new JsonResult(Ok(result));

        }

        // Get
        [HttpGet]
        public async Task<JsonResult> Get(Guid id)
        {
            var result = AutoMapperDTO.Mapper.Map<ProductDTO>(await _repo.Get(id));

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public async Task<JsonResult> Delete(Guid id)
        {
            var result = await _repo.Delete(id);
            var poductsForBuy = (await _repoBuy.GetModels()).Where(e => e.Product == id).ToList();
            if (poductsForBuy.Count > 0)
            {
                foreach(var elem in poductsForBuy)
                {
                    await _repoBuy.Delete(elem.Product);
                }
            }

            return new JsonResult(result);
        }

        // Get all
        [HttpGet()]
        public async Task<JsonResult> GetAll()
        {
            var productsForBuy = (await _repoBuy.GetModels()).Select(e => AutoMapperDTO.Mapper.Map<ProductForBuyDTO>(e)).ToList();
            var products = (await _repo.GetModels()).Select(e => AutoMapperDTO.Mapper.Map<ProductDTO>(e)).ToList();

            var result = products.GroupJoin(
                productsForBuy,
                e => e.Id,
                e => e.Product,
                (inner, outer) => outer.Any() 
                    ? default
                    : inner
                )
                .Where(e => e != default)
                .ToList();
                ;

            return new JsonResult(Ok(result));
        }

    }
}
