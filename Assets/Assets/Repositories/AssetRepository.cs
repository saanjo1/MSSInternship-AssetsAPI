using Microsoft.Azure.Cosmos.Table;
using Assets.Contracts;
using Assets.Models;
using AutoMapper;

namespace Assets.Repositories
{
    public class AssetRepository : IRepository<Asset>
    {
        private string _connectionString;
        private string _tableName;
        private CloudTableClient _tableClient;
        private CloudTable _table;
        private readonly IMapper _mapper;


        public AssetRepository(string connectionString, string tableNamePrefix, IMapper mapper)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException("connectionString", "connectionString is null");
            _tableName = tableNamePrefix ?? throw new ArgumentNullException("tablePrefix", "tablePrefix is null");
            CloudStorageAccount storageAccount =
            CloudStorageAccount.Parse(connectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _table = _tableClient.GetTableReference(tableNamePrefix);
            _table.CreateIfNotExists();
            _mapper = mapper;

        }

        public async Task<bool> Delete(string assetId)
        {
            AssetTableEntity assetEntity = new AssetTableEntity
            {
                PartitionKey = assetId,
                RowKey = assetId,
                ETag = "*"
            };

            TableOperation deleteOperation = TableOperation.Delete(assetEntity);
            var tableResult = _table.Execute(deleteOperation);
            return tableResult.HttpStatusCode == 204 ? true : false;
        }

        public async Task<string> Post(Asset asset)
        {
            asset.AssetID = Guid.NewGuid().ToString();

            AssetTableEntity assetEntity = new AssetTableEntity(asset.AssetID);

            _mapper.Map(asset, assetEntity);

            TableOperation insertOperation = TableOperation.InsertOrReplace(assetEntity);
            var tableresult = _table.Execute(insertOperation);

            return await Task.FromResult(tableresult.HttpStatusCode == 204 ? "Assets created successfully." : "An error occured while creating a asset.");
        }

        public async Task<Asset> Read(string assetId)
        {
            var filter = $"PartitionKey eq '{assetId}'";
            var query = new TableQuery<AssetTableEntity>().Where(filter);

            var lst = _table.ExecuteQuery(query);
            var result = lst.Select(x => _mapper.Map<Asset>(x)).FirstOrDefault();

            return result;
        }

        public async Task<IEnumerable<Asset>> Read()
        {
            var query = new TableQuery<AssetTableEntity>();
            var lst = _table.ExecuteQuery(query);

            var result = lst.Select(x => _mapper.Map<Asset>(x));

            return result;
        }

        public async Task<bool> Update(Asset asset)
        {
            AssetTableEntity assetEntity = new AssetTableEntity(asset.AssetID);

            _mapper.Map(asset, assetEntity);

            TableOperation insertOperation = TableOperation.InsertOrMerge(assetEntity);
            
            var operationResult = _table.Execute(insertOperation);
            return operationResult.HttpStatusCode == 204 ? true : false;
        }

        public Task<List<string>> GetPurchaseType()
        {
            var result = new List<string> { "Owned", "Rented", "Leased", "Subscription" };
            return Task.FromResult(result);
        }
        
    }
}
