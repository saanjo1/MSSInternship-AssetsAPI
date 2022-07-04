using Microsoft.Azure.Cosmos.Table;

namespace Assets.Models
{
    public class AssetCategory
    {
        public string AssetCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
