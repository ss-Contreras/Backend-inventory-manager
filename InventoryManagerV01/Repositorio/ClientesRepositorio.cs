using AutoMapper;
using InventoryManagerV01.Models;
using InventoryManagerV01.Models.Dtos;
using InventoryManagerV01.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using InventoryManagerV01.Data;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagerV01.Repositorio
{
    public class ClientesRepositorio : IClientesRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public ClientesRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarCliente(Clientes cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.Nombre) || !IsValidCliente(cliente))
            {
                throw new ValidationException("Datos del cliente no son válidos.");
            }

            _bd.Clientes.Update(cliente);
            return Guardar();
        }

        public bool BorrarCliente(Clientes cliente)
        {
            if (cliente == null || cliente.ClienteID <= 0)
            {
                throw new ValidationException("Cliente no válido para eliminación.");
            }
            _bd.Clientes.Remove(cliente);
            return Guardar();
        }

        public bool CrearCliente(Clientes cliente)
        {
            if (cliente == null || string.IsNullOrEmpty(cliente.Nombre) || !IsValidCliente(cliente))
            {
                throw new ValidationException("Datos del cliente no son válidos.");
            }

            _bd.Clientes.Add(cliente);
            return Guardar();
        }

        public bool ExisteCliente(int clienteID)
        {
            if (clienteID <= 0)
            {
                throw new ValidationException("ID del cliente no válido.");
            }
            return _bd.Clientes.Any(c => c.ClienteID == clienteID);
        }

        public bool ExisteCliente(string email)
        {
            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                throw new ValidationException("Correo electrónico no válido.");
            }
            return _bd.Clientes.Any(c => c.Email.ToLower().Trim() == email.ToLower().Trim());
        }

        public Clientes GetCliente(int clienteID)
        {
            if (clienteID <= 0)
            {
                throw new ValidationException("ID del cliente no válido.");
            }
            return _bd.Clientes.FirstOrDefault(c => c.ClienteID == clienteID);
        }

        public ICollection<Clientes> GetClientes()
        {
            return _bd.Clientes.OrderBy(c => c.Nombre).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0;
        }

        private bool IsValidCliente(Clientes cliente)
        {
            if (string.IsNullOrEmpty(cliente.Nombre) || cliente.Nombre.Length > 100)
            {
                return false;
            }
            if (!string.IsNullOrEmpty(cliente.Telefono) && cliente.Telefono.Length > 15)
            {
                return false;
            }
            if (string.IsNullOrEmpty(cliente.Email) || !IsValidEmail(cliente.Email))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(cliente.Direccion) && cliente.Direccion.Length > 255)
            {
                return false;
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}