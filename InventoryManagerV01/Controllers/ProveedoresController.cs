using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagerV01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedoresRepositorio _proveedoresRepo;
        private readonly IMapper _mapper;

        public ProveedoresController(IProveedoresRepositorio proveedoresRepo, IMapper mapper)
        {
            _proveedoresRepo = proveedoresRepo;
            _mapper = mapper;
        }

        // GET: api/Proveedores
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProveedoresDto>))]
        public IActionResult GetProveedores()
        {
            var listaProveedores = _proveedoresRepo.GetProveedores();
            var listaProveedoresDto = new List<ProveedoresDto>();

            foreach (var proveedor in listaProveedores)
            {
                listaProveedoresDto.Add(_mapper.Map<ProveedoresDto>(proveedor));
            }

            return Ok(listaProveedoresDto);
        }

        // GET: api/Proveedores/{id}
        [HttpGet("{id:int}", Name = "GetProveedor")]
        [ProducesResponseType(200, Type = typeof(ProveedoresDto))]
        [ProducesResponseType(404)]
        public IActionResult GetProveedor(int id)
        {
            var proveedor = _proveedoresRepo.GetProveedor(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            var proveedorDto = _mapper.Map<ProveedoresDto>(proveedor);
            return Ok(proveedorDto);
        }

        // POST: api/Proveedores
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProveedoresDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CrearProveedor([FromBody] CrearProveedoresDto crearProveedoresDto)
        {
            if (crearProveedoresDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_proveedoresRepo.ExisteProveedor(crearProveedoresDto.Nombre))
            {
                ModelState.AddModelError("", "El proveedor ya existe");
                return StatusCode(404, ModelState);
            }

            var proveedor = _mapper.Map<Proveedores>(crearProveedoresDto);

            if (!_proveedoresRepo.CrearProveedor(proveedor))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el proveedor {proveedor.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProveedor", new { id = proveedor.ProveedorID }, proveedor);
        }

        // PUT: api/Proveedores/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ActualizarProveedor(int id, [FromBody] ProveedoresDto actualizarProveedoresDto)
        {
            if (actualizarProveedoresDto == null || id != actualizarProveedoresDto.ProveedorID)
            {
                return BadRequest(ModelState);
            }

            if (!_proveedoresRepo.ExisteProveedor(id))
            {
                return NotFound();
            }

            var proveedor = _mapper.Map<Proveedores>(actualizarProveedoresDto);

            if (!_proveedoresRepo.ActualizarProveedor(proveedor))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el proveedor {proveedor.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Proveedores/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult BorrarProveedor(int id)
        {
            if (!_proveedoresRepo.ExisteProveedor(id))
            {
                return NotFound();
            }

            var proveedor = _proveedoresRepo.GetProveedor(id);

            if (!_proveedoresRepo.BorrarProveedor(proveedor))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el proveedor {proveedor.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}