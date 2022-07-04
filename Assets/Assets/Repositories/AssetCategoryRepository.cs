using Assets.Contracts;
using Assets.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace Assets.Repositories
{
    public class AssetCategoryRepository : IRepository<AssetCategory>
    {
        private string _connectionString;
        private string _tableName;
        private CloudTableClient _tableClient;
        private CloudTable _table;
        private readonly IMapper _mapper;


        public AssetCategoryRepository(string connectionString, string tableNamePrefix, IMapper mapper)
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

        public async Task<string> Post(AssetCategory assetCategory)
        {
            assetCategory.AssetCategoryId = Guid.NewGuid().ToString();

            AssetCategoryTableEntity assetCategoryEntity = new AssetCategoryTableEntity(assetCategory.AssetCategoryId);

            _mapper.Map(assetCategory, assetCategoryEntity);


            TableOperation insertOperation = TableOperation.InsertOrReplace(assetCategoryEntity);
            var tableresult = _table.Execute(insertOperation);

            return await Task.FromResult(tableresult.HttpStatusCode == 204 ? "Asset category created successfully." : "An error occured while creating a asset category.");
        }

        public async Task<AssetCategory> Read(string assetId)
        {
            var filter = $"PartitionKey eq '{assetId}'";
            var query = new TableQuery<AssetCategoryTableEntity>().Where(filter);

            var lst = _table.ExecuteQuery(query);
            var result = lst.Select(x => _mapper.Map<AssetCategory>(x)).FirstOrDefault();

            return result;
        }


        public async Task<IEnumerable<AssetCategory>> Read()
        {
            var query = new TableQuery<AssetCategoryTableEntity>();
            var lst = _table.ExecuteQuery(query);

            var result = lst.Select(x => _mapper.Map<AssetCategory>(x));

            return result;

        }

        public async Task<bool> Update(AssetCategory AssetCategory)
        {
            AssetCategoryTableEntity assetEntity = new AssetCategoryTableEntity(AssetCategory.AssetCategoryId);

            _mapper.Map(AssetCategory, assetEntity);

            TableOperation insertOperation = TableOperation.InsertOrMerge(assetEntity);

            var operationResult = _table.Execute(insertOperation);
            return operationResult.HttpStatusCode == 204 ? true : false;
        }

        public async Task<bool> Delete(string assetId)
        {
            AssetCategoryTableEntity assetEntity = new AssetCategoryTableEntity
            {
                PartitionKey = assetId,
                RowKey = assetId,
                ETag = "*"
            };

            TableOperation deleteOperation = TableOperation.Delete(assetEntity);
            var tableResult = _table.Execute(deleteOperation);
            return tableResult.HttpStatusCode == 204 ? true : false;
        }

        public Task<List<string>> GetPurchaseType()
        {
            throw new NotImplementedException();
        }
    }
}
