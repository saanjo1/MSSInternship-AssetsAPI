using Assets.Models;
using Assets.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assets.Contracts;

namespace Assets.Controllers
{
    [ApiController]
    public class AssetCategoryController : ControllerBase
    {
        private readonly IRepository<AssetCategory> AssetCategoryRepo;

        public AssetCategoryController(IRepository<AssetCategory> _assetRepo)
        {
            AssetCategoryRepo = _assetRepo;
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetCategories()
        {
            var createResponse = await AssetCategoryRepo.Read();
            return Ok(createResponse);
        }


        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetCategoryById(string id)
        {
            var createResponse = await AssetCategoryRepo.Read(id);
            return Ok(createResponse);
        }

        [HttpPost]
        [Route("api/[action]")]

        public async Task<IActionResult> PostCategories(AssetCategory model)
        {
            Console.WriteLine("POST action.");
            var response = await AssetCategoryRepo.Post(model);

            return Ok(response);
        }


        [HttpPut]
        [Route("api/[action]")]
        public async Task<IActionResult> PutCategories(AssetCategory model)
        {
            var createResponse = await AssetCategoryRepo.Update(model);
            return Ok(createResponse);
        }


        [HttpDelete]
        [Route("api/[action]")]
        public async Task<IActionResult> DeleteCategories(string ID)
        {

            var createResponse = await AssetCategoryRepo.Delete(ID);
            return Ok(createResponse);

        }



    }
}
