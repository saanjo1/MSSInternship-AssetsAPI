using Assets.Models;
using AutoMapper;

namespace Assets.Mapping_profiles
{
    public class AssetTypeProfile : Profile
    {
        public AssetTypeProfile()
        {
            CreateMap<AssetType, AssetTypeTableEntity>().ReverseMap();
        }
    }
}
