namespace Assets.Models
{
    public class Asset
    {
        public string AssetID { get; set; }
        public string AssetCategoryId { get; set; }
        public string AssetTypeId { get; set; }
        public string AssetName { get; set; }
        public Guid SerialNumber { get; init; }
        public double Price { get; set; }
        public string DepreciationType { get; set; }
        public string Location { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime WarrantyExpiryDate { get; set; }
        public string PurchaseType { get; set; }
    }
}
