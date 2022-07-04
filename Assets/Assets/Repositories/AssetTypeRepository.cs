using Assets.Contracts;
using Assets.Models;
using AutoMapper;
using Microsoft.Azure.Cosmos.Table;

namespace Assets.Repositories
{
    public class AssetTypeRepository : IRepository<AssetType>
    {
        private string _connectionString;
        private string _tableName;
        private CloudTableClient _tableClient;
        private CloudTable _table;
        private readonly IMapper _mapper;


        public AssetTypeRepository(string connectionString, string tableNamePrefix, IMapper mapper)
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

        public async Task<string> Post(AssetType assetType)
            {
                assetType.AssetTypeId = Guid.NewGuid().ToString();
                AssetTypeTableEntity assetTypeEntity = new AssetTypeTableEntity(assetType.AssetTypeId);

                _mapper.Map(assetType, assetTypeEntity);

            TableOperation insertOperation = TableOperation.InsertOrReplace(assetTypeEntity);
                var tableresult = _table.Execute(insertOperation);

                return await Task.FromResult(tableresult.HttpStatusCode == 204 ? "Asset type created successfully." : "An error occured while creating a asset type.");
            }
    
    public async Task<AssetType> Read(string assetId)
        {
            var filter = $"PartitionKey eq '{assetId}'";
            var query = new TableQuery<AssetTypeTableEntity>().Where(filter);

            var lst = _table.ExecuteQuery(query);
            var result = lst.Select(x => _mapper.Map<AssetType>(x)).FirstOrDefault();

            return result;
        }

        public async Task<IEnumerable<AssetType>> Read()
        {
            var query = new TableQuery<AssetTypeTableEntity>();
            var lst = _table.ExecuteQuery(query);

            var result = lst.Select(x => _mapper.Map<AssetType>(x));

            return result;
        }

        public async Task<bool> Update(AssetType assetType)
        {
            AssetTypeTableEntity assetEntity = new AssetTypeTableEntity(assetType.AssetTypeId);

            _mapper.Map(assetType, assetEntity);

            TableOperation insertOperation = TableOperation.InsertOrMerge(assetEntity);

            var operationResult = _table.Execute(insertOperation);
            return operationResult.HttpStatusCode == 204 ? true : false;
        }

        public async Task<bool> Delete(string assetTypeId)
        {
            AssetTypeTableEntity assetTypeEntity = new AssetTypeTableEntity
            {
                PartitionKey = assetTypeId,
                RowKey = assetTypeId,
                ETag = "*"
            };

            TableOperation deleteOperation = TableOperation.Delete(assetTypeEntity);
            var tableResult = _table.Execute(deleteOperation);
            return tableResult.HttpStatusCode == 204 ? true : false;
        }

        public Task<List<string>> GetPurchaseType()
        {
            throw new NotImplementedException();
        }
    }
}
