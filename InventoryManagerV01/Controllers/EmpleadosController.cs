using InventoryManagerV01.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;

namespace InventoryManagerV01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadosRepositorio _empleadosRepo;
        private readonly IMapper _mapper;

        public EmpleadosController(IEmpleadosRepositorio empleadosRepo, IMapper mapper)
        {
            _empleadosRepo = empleadosRepo;
            _mapper = mapper;
        }

        // GET: api/Empleados
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmpleados()
        {
            try
            {
                var listaEmpleados = _empleadosRepo.GetEmpleados();
                var listaEmpleadosDto = _mapper.Map<List<EmpleadoDto>>(listaEmpleados);
                return Ok(listaEmpleadosDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al recuperar los empleados.");
            }
        }

        // GET: api/Empleados/{id}
        [HttpGet("{id:int}", Name = "GetEmpleado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmpleado(int id)
        {
            try
            {
                var empleado = _empleadosRepo.GetEmpleado(id);
                if (empleado == null)
                {
                    return NotFound("Empleado no encontrado.");
                }
                var empleadoDto = _mapper.Map<EmpleadoDto>(empleado);
                return Ok(empleadoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al recuperar el empleado.");
            }
        }

        // POST: api/Empleados
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CrearEmpleado([FromBody] CrearEmpleadoDto crearEmpleadoDto)
        {
            if (crearEmpleadoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_empleadosRepo.ExisteEmpleado(crearEmpleadoDto.Email))
            {
                ModelState.AddModelError("", "Ya existe un empleado con el mismo email.");
                return StatusCode(400, ModelState);
            }

            var empleado = _mapper.Map<Empleados>(crearEmpleadoDto);

            if (!_empleadosRepo.CrearEmpleado(empleado))
            {
                ModelState.AddModelError("", "Algo salió mal al crear el empleado.");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetEmpleado", new { id = empleado.EmpleadoID }, empleado);
        }

        // PUT: api/Empleados/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ActualizarEmpleado(int id, [FromBody] ActualizarEmpleadoDto actualizarEmpleadoDto)
        {
            if (actualizarEmpleadoDto == null || id != actualizarEmpleadoDto.EmpleadoID)
            {
                return BadRequest(ModelState);
            }

            if (!_empleadosRepo.ExisteEmpleado(id))
            {
                return NotFound("Empleado no encontrado.");
            }

            var empleado = _mapper.Map<Empleados>(actualizarEmpleadoDto);
            empleado.EmpleadoID = id;

            if (!_empleadosRepo.ActualizarEmpleado(empleado))
            {
                ModelState.AddModelError("", "Algo salió mal al actualizar el empleado.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE: api/Empleados/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult BorrarEmpleado(int id)
        {
            if (!_empleadosRepo.ExisteEmpleado(id))
            {
                return NotFound("Empleado no encontrado.");
            }

            var empleado = _empleadosRepo.GetEmpleado(id);

            if (!_empleadosRepo.BorrarEmpleado(empleado))
            {
                ModelState.AddModelError("", "Algo salió mal al eliminar el empleado.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}