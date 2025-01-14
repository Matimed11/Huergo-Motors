﻿using System;
using System.Collections.Generic;
using HuergoMotors.DAO;
using HuergoMotors.DTO;

namespace HuergoMotors.Negocio
{
    public class ClientesNegocio
    {
        ClientesDAO clientesDAO = new ClientesDAO();

        public List<ClientesDTO> CargarTabla()
        {
            return clientesDAO.CargarDatos();
        }

        public List<ClientesDTO> Buscar(string filtro)
        {
            return clientesDAO.Buscar(filtro);
        }

        public ClientesDTO BuscarId(int id)
        {
            return clientesDAO.BuscarId(id);
        }

        public ClientesDTO BuscarCliente(string email, string clave)
        {
            return clientesDAO.BuscarCliente(email, clave);
        }

        public int EliminarElemento(int id)
        {
            clientesDAO.Referenciado(id);
            return clientesDAO.EliminarElemento(id);
        }
            
        public int ModificarElemento(ClientesDTO clienteDTO)
        {
            return clientesDAO.ModificarElemento(clienteDTO);
        }

        public int AgregarElemento(ClientesDTO clienteDTO)
        {
            return clientesDAO.AgregarElemento(clienteDTO);
        }
    }
}
