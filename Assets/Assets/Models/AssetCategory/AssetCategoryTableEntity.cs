using Microsoft.Azure.Cosmos.Table;

namespace Assets.Models
{
    public class AssetCategoryTableEntity : TableEntity
    {
        public AssetCategoryTableEntity()
        {

        }
        public AssetCategoryTableEntity(string assetCategoryId)
        {
            this.PartitionKey = assetCategoryId;
            this.RowKey = assetCategoryId;
        }

        public string AssetCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }



    }



}
