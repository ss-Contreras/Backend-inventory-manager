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
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepositorio _clientesRepo;
        private readonly IMapper _mapper;

        public ClientesController(IClientesRepositorio clientesRepo, IMapper mapper)
        {
            _clientesRepo = clientesRepo;
            _mapper = mapper;
        }

        // GET: api/Clientes
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ClientesDto>))]
        public IActionResult GetClientes()
        {
            var listaClientes = _clientesRepo.GetClientes();
            var listaClientesDto = listaClientes.Select(cliente => _mapper.Map<ClientesDto>(cliente)).ToList();
            return Ok(listaClientesDto);
        }

        // GET: api/Clientes/{id}
        [HttpGet("{id:int}", Name = "GetCliente")]
        [ProducesResponseType(200, Type = typeof(ClientesDto))]
        [ProducesResponseType(404)]
        public IActionResult GetCliente(int id)
        {
            if (!_clientesRepo.ExisteCliente(id))
            {
                return NotFound();
            }

            var cliente = _clientesRepo.GetCliente(id);
            var clienteDto = _mapper.Map<ClientesDto>(cliente);
            return Ok(clienteDto);
        }

        // POST: api/Clientes
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClientesDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CrearCliente([FromBody] CrearClientesDto crearClientesDto)
        {
            if (crearClientesDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_clientesRepo.ExisteCliente(crearClientesDto.Email))
            {
                ModelState.AddModelError("", "El cliente ya existe");
                return StatusCode(404, ModelState);
            }

            var cliente = _mapper.Map<Clientes>(crearClientesDto);

            if (!_clientesRepo.CrearCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal al guardar el cliente {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCliente", new { id = cliente.ClienteID }, cliente);
        }

        // PUT: api/Clientes/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ActualizarCliente(int id, [FromBody] ClientesDto actualizarClientesDto)
        {
            if (actualizarClientesDto == null || id != actualizarClientesDto.ClienteID)
            {
                return BadRequest(ModelState);
            }

            if (!_clientesRepo.ExisteCliente(id))
            {
                return NotFound();
            }

            var cliente = _mapper.Map<Clientes>(actualizarClientesDto);

            if (!_clientesRepo.ActualizarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal al actualizar el cliente {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Clientes/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult BorrarCliente(int id)
        {
            if (!_clientesRepo.ExisteCliente(id))
            {
                return NotFound();
            }

            var cliente = _clientesRepo.GetCliente(id);

            if (!_clientesRepo.BorrarCliente(cliente))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar el cliente {cliente.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}