using Microsoft.Azure.Cosmos.Table;


namespace Assets.Models
{
    public class AssetTypeTableEntity : TableEntity
    {
        public AssetTypeTableEntity()
        {

        }
        public AssetTypeTableEntity(string AssetTypeId)
        {
            this.PartitionKey = AssetTypeId;
            this.RowKey = AssetTypeId;
        }

        public string AssetTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
