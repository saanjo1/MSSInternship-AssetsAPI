using Microsoft.AspNetCore.Mvc;
using Assets.Models;
using Microsoft.Azure.Cosmos.Table;
using Assets.Repositories;
using Assets.Contracts;
using AutoMapper;

namespace Assets.Controllers
{
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IRepository<Asset> AssetRepo;


        public AssetController(IRepository<Asset> _assetRepo)
        {
            AssetRepo = _assetRepo;
        }

        [HttpGet]
        [Route("api/[action]")]

        public async Task<IActionResult> GetAssets()
        {
            var createResponse = await AssetRepo.Read();
            return Ok(createResponse);
        }

        [HttpPost]
        [Route("api/[action]")]

        public async Task<IActionResult> PostAssets(Asset model)
        {
            Console.WriteLine("POST action ..");
            var response = await AssetRepo.Post(model);

            return Ok(response);
        }


        [HttpPut]
        [Route("api/[action]")]
        public async Task<IActionResult> PutAssets(Asset model)
        {
            var createResponse = await AssetRepo.Update(model);
            return Ok(createResponse);
        }


        [HttpDelete]
        [Route("api/[action]")]
        public async Task<IActionResult> DeleteAssets(string ID)
        {

            var createResponse = await AssetRepo.Delete(ID);
            return Ok(createResponse);

        }

        [HttpGet]
        [Route("api/[action]")]
        public async Task<IActionResult> GetPurchaseType()
        {
            var createResponse = await AssetRepo.GetPurchaseType();
            return Ok(createResponse);
        }


    }

}
