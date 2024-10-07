using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagerV01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentasRepositorio _ventasRepo;
        private readonly IMapper _mapper;

        public VentasController(IVentasRepositorio ventasRepo, IMapper mapper)
        {
            _ventasRepo = ventasRepo;
            _mapper = mapper;
        }

        // GET: api/Ventas
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<VentasDto>))]
        public IActionResult GetVentas()
        {
            var listaVentas = _ventasRepo.GetVentas();
            var listaVentasDto = listaVentas.Select(venta => _mapper.Map<VentasDto>(venta)).ToList();
            return Ok(listaVentasDto);
        }

        // GET: api/Ventas/{id}
        [HttpGet("{id:int}", Name = "GetVenta")]
        [ProducesResponseType(200, Type = typeof(VentasDto))]
        [ProducesResponseType(404)]
        public IActionResult GetVenta(int id)
        {
            if (!_ventasRepo.ExisteVenta(id))
            {
                return NotFound();
            }

            var venta = _ventasRepo.GetVenta(id);
            var ventaDto = _mapper.Map<VentasDto>(venta);
            return Ok(ventaDto);
        }

        // POST: api/Ventas
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(VentasDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CrearVenta([FromBody] CrearVentaDto crearVentaDto)
        {
            if (crearVentaDto == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var venta = _mapper.Map<Ventas>(crearVentaDto);

            try
            {
                if (!_ventasRepo.CrearVenta(venta))
                {
                    ModelState.AddModelError("", $"Algo salió mal al guardar la venta.");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetVenta", new { id = venta.VentaID }, venta);
        }

        // PUT: api/Ventas/{id}
        // PUT: api/Ventas/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult ActualizarVenta(int id, [FromBody] ActualizarVentaDto actualizarVentaDto)
        {
            if (actualizarVentaDto == null || id != actualizarVentaDto.VentaID)
            {
                return BadRequest("La información proporcionada es inválida.");
            }

            if (!_ventasRepo.ExisteVenta(id))
            {
                return NotFound("No se encontró la venta especificada.");
            }

            var venta = _mapper.Map<Ventas>(actualizarVentaDto);
            venta.VentaID = id;

            try
            {
                if (!_ventasRepo.ActualizarVenta(venta))
                {
                    ModelState.AddModelError("", $"Algo salió mal al actualizar la venta.");
                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }


        // DELETE: api/Ventas/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult BorrarVenta(int id)
        {
            if (!_ventasRepo.ExisteVenta(id))
            {
                return NotFound();
            }

            var venta = _ventasRepo.GetVenta(id);

            if (!_ventasRepo.BorrarVenta(venta))
            {
                ModelState.AddModelError("", $"Algo salió mal al eliminar la venta.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // GET: api/Ventas/cliente/{clienteId}
        [HttpGet("cliente/{clienteId:int}")]
        [ProducesResponseType(200, Type = typeof(List<VentasDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetVentasPorCliente(int clienteId)
        {
            var listaVentas = _ventasRepo.GetVentasPorCliente(clienteId);
            if (listaVentas == null || !listaVentas.Any())
            {
                return NotFound("No se encontraron ventas para el cliente especificado.");
            }

            var listaVentasDto = listaVentas.Select(venta => _mapper.Map<VentasDto>(venta)).ToList();
            return Ok(listaVentasDto);
        }

        // GET: api/Ventas/fecha?inicio=YYYY-MM-DD&fin=YYYY-MM-DD
        [HttpGet("fecha")]
        [ProducesResponseType(200, Type = typeof(List<VentasDto>))]
        [ProducesResponseType(404)]
        public IActionResult GetVentasEntreFechas([FromQuery] DateTime inicio, [FromQuery] DateTime fin)
        {
            if (inicio > fin)
            {
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            var listaVentas = _ventasRepo.GetVentasEntreFechas(inicio, fin);
            if (listaVentas == null || !listaVentas.Any())
            {
                return NotFound("No se encontraron ventas en el rango de fechas especificado.");
            }

            var listaVentasDto = listaVentas.Select(venta => _mapper.Map<VentasDto>(venta)).ToList();
            return Ok(listaVentasDto);
        }
    }
}
