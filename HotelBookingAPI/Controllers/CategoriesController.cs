using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataBase;
using DataBase.Models;
using Interfaces.Repositories;
using DataBase.Repositories;

namespace tabakaevAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private CategoriesRepository _repo = new CategoriesRepository();
        public CategoriesController()
        {
        }

        // Create/Edit
        [HttpPost]
        public JsonResult Edit(Category model)
        {
            try 
            {
                _repo.Update(model);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex);
            }
            return new JsonResult(Ok());
        }

        [HttpPost]
        public JsonResult Create(Category model)
        {
            var result = Guid.Empty;
            if (model.Id == Guid.Empty) 
            {
                model.Id = Guid.NewGuid();
            }
            var modelById = _repo.Get(model.Id);
            var modelByName = _repo.GetByName(model.Name ?? string.Empty);
            if (modelById == null && modelByName == null)
            {
                result = _repo.Create(model);
            }

            return new JsonResult(Ok(result));

        }

        // Get
        [HttpGet]
        public JsonResult Get(Guid id)
        {
            var result = _repo.Get(id);

            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(Guid id)
        {
            var result = _repo.Delete(id);

            return new JsonResult(result);
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _repo.GetModels();

            return new JsonResult(Ok(result));
        }
       
    }
}
