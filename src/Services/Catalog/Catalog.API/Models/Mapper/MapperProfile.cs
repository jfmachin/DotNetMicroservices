using AutoMapper;
using Catalog.API.Models.DTOs;
using Catalog.API.Models.Entities;

namespace Catalog.API.Models.Mapper {
    public class MapperProfile: Profile {
        public MapperProfile() {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
