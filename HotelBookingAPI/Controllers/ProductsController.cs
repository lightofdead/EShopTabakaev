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
    public class ProductsController : ControllerBase
    {
        private ProductsRepository _repo = new ProductsRepository();
        public ProductsController()
        {
        }

        // Create/Edit
        [HttpPost]
        public async Task<JsonResult> Edit(ProductDTO model)
        {
            try 
            {
                 _repo.Update(AutoMapperDTO.Mapper.Map<Product>(model));
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

            return new JsonResult(result);
        }

        // Get all
        [HttpGet()]
        public async Task<JsonResult> GetAll()
        {
            var result = (await _repo.GetModels()).Select(e => AutoMapperDTO.Mapper.Map<ProductDTO>(e));

            return new JsonResult(Ok(result));
        }
    }
}
