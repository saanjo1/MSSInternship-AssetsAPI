using Assets.Contracts;
using Assets.Models;
using Assets.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assets.Controllers
{
    [ApiController]
    public class AssetTypeController : ControllerBase
    {
        private readonly IRepository<AssetType> AssetTypeRepo;

        public AssetTypeController(IRepository<AssetType> _assetRepo)
        {
            AssetTypeRepo = _assetRepo;
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetTypes()
        {
            var createResponse = await AssetTypeRepo.Read();
            return Ok(createResponse);
        }

        [HttpGet]
        [Route("api/[action]")]
        public async Task<IActionResult> GetById(string id)
        {
            var createResponse = await AssetTypeRepo.Read(id);
            return Ok(createResponse);
        }

        [HttpPost]
        [Route("api/[action]")]

        public async Task<IActionResult> PostTypes(AssetType model)
        {
            Console.WriteLine("POST action.");
            var response = await AssetTypeRepo.Post(model);

            return Ok(response);
        }


        [HttpPut]
        [Route("api/[action]")]
        public async Task<IActionResult> PutTypes(AssetType model)
        {
            var createResponse = await AssetTypeRepo.Update(model);
            return Ok(createResponse);
        }




        [HttpDelete]
        [Route("api/[action]")]
        public async Task<IActionResult> DeleteTypes(string ID)
        {

            var createResponse = await AssetTypeRepo.Delete(ID);
            return Ok(createResponse);

        }




    }
}
