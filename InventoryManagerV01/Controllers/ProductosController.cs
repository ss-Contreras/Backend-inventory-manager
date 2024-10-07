using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;

namespace InventoryManagerV01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosRepositorio _productosRepo;
        private readonly IMapper _mapper;

        public ProductosController(IProductosRepositorio productosRepo, IMapper mapper)
        {
            _productosRepo = productosRepo;
            _mapper = mapper;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductosDto>))]
        public IActionResult GetProductos()
        {
            var listaProductos = _productosRepo.GetProductos();
            var listaProductosDto = listaProductos.Select(producto => _mapper.Map<ProductosDto>(producto)).ToList();
            return Ok(listaProductosDto);
        }

        // GET: api/Productos/{id}
        [HttpGet("{id:int}", Name = "GetProducto")]
        [ProducesResponseType(200, Type = typeof(ProductosDto))]
        [ProducesResponseType(404)]
        public IActionResult GetProducto(int id)
        {
            if (!_productosRepo.ExisteProducto(id))
            {
                return NotFound();
            }

            var producto = _productosRepo.GetProducto(id);
            var productoDto = _mapper.Map<ProductosDto>(producto);
            return Ok(productoDto);
        }

        // POST: api/Productos
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductosDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CrearProducto([FromForm] CrearProductosDto crearProductosDto)
        {
            if (crearProductosDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_productosRepo.ExisteProducto(crearProductosDto.Nombre))
            {
                ModelState.AddModelError("", "El producto ya existe");
                return StatusCode(409, ModelState);
            }

            var producto = _mapper.Map<Productos>(crearProductosDto);

            try
            {
                if (crearProductosDto.Imagen != null)
                {
                    string nombreArchivo = producto.ProductoID + System.Guid.NewGuid().ToString() + Path.GetExtension(crearProductosDto.Imagen.FileName);
                    string rutaArchivo = Path.Combine("wwwroot", "ImagenesProductos", nombreArchivo);

                    var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                    using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                    {
                        crearProductosDto.Imagen.CopyTo(fileStream);
                    }

                    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                    producto.RutaIMagen = $"{baseUrl}/ImagenesProductos/{nombreArchivo}";
                    producto.RutaLocalIMagen = rutaArchivo;
                }
                else
                {
                    producto.RutaIMagen = "https://placehold.co/600x400";
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al cargar la imagen");
                return StatusCode(500, ModelState);
            }

            if (!_productosRepo.CrearProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el producto {producto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProducto", new { id = producto.ProductoID }, producto);
        }

        // PUT: api/Productos/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ActualizarProducto(int id, [FromBody] ActualizarProductoDto actualizarProductoDto)
        {
            if (actualizarProductoDto == null || id != actualizarProductoDto.ProductoID)
            {
                return BadRequest(ModelState);
            }

            if (!_productosRepo.ExisteProducto(id))
            {
                return NotFound();
            }

            var producto = _mapper.Map<Productos>(actualizarProductoDto);

            if (!_productosRepo.ActualizarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el producto {producto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Productos/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult BorrarProducto(int id)
        {
            if (!_productosRepo.ExisteProducto(id))
            {
                return NotFound();
            }

            var producto = _productosRepo.GetProducto(id);

            if (!_productosRepo.BorrarProducto(producto))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el producto {producto.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // GET: api/Productos/buscar
        [HttpGet("buscar")]
        [ProducesResponseType(200, Type = typeof(List<ProductosDto>))]
        [ProducesResponseType(404)]
        public IActionResult BuscarProductos([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return BadRequest("El criterio de búsqueda no puede estar vacío.");
            }

            var listaProductos = _productosRepo.BuscarProductos(nombre);
            if (listaProductos == null || !listaProductos.Any())
            {
                return NotFound("No se encontraron productos con el criterio proporcionado.");
            }

            var listaProductosDto = listaProductos.Select(producto => _mapper.Map<ProductosDto>(producto)).ToList();
            return Ok(listaProductosDto);
        }

        // GET: api/Productos/categoria/{categoriaId}
        [HttpGet("categoria/{categoriaId:int}")]
        [ProducesResponseType(200, Type = typeof(List<ProductosDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetProductosEnCategoria(int categoriaId)
        {
            var listaProductos = _productosRepo.GetProductosEnCategoria(categoriaId);
            if (listaProductos == null || !listaProductos.Any())
            {
                return NotFound("No se encontraron productos para la categoría proporcionada.");
            }

            var listaProductosDto = listaProductos.Select(producto => _mapper.Map<ProductosDto>(producto)).ToList();
            return Ok(listaProductosDto);
        }
    }
}
