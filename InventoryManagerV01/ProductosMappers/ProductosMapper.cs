using InventoryManagerV01.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
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
            CreateMap<Productos, ProductosDto>().ReverseMap();
            CreateMap<Productos, CrearProductosDto>().ReverseMap();
            CreateMap<Productos, ActualizarProductoDto>().ReverseMap();
            CreateMap<Proveedores, ProveedoresDto>().ReverseMap();
            CreateMap<Proveedores, CrearProveedoresDto>().ReverseMap();
            CreateMap<Clientes, ClientesDto>().ReverseMap();
            CreateMap<Clientes, CrearClientesDto>().ReverseMap();
            CreateMap<Ventas, VentasDto>().ReverseMap();
            CreateMap<Ventas, CrearVentaDto>().ReverseMap();
            CreateMap<Ventas, ActualizarVentaDto>().ReverseMap();
            CreateMap<Empleados, EmpleadoDto>().ReverseMap();
            CreateMap<Empleados, CrearEmpleadoDto>().ReverseMap();
            CreateMap<Empleados, ActualizarEmpleadoDto>().ReverseMap();
        }
    }
}