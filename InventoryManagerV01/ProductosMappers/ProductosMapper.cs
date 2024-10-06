using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;

namespace InventoryManagerV01.ProductosMappers
{
    public class ProductosMapper : Profile
    {
        public ProductosMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
        }
    }
}
