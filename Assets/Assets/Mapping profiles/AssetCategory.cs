using Assets.Models;
using AutoMapper;

namespace Assets.Mapping_profiles
{
    public class AssetCategoryProfile : Profile
    {
        public AssetCategoryProfile()
        {
            CreateMap<AssetCategory, AssetCategoryTableEntity>().ReverseMap();
        }
    }
}
