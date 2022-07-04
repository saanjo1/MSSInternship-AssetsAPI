using Assets.Models;
using AutoMapper;

namespace Assets.Mapping_profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetTableEntity>().ReverseMap();
        }
    }
}
