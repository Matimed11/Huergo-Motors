﻿using System.Collections.Generic;
using System.Data;
using HuergoMotors.DTO;

namespace HuergoMotors.DAO
{
    public class AccesoriosDAO
    {
        HelperDAO helperDAO = new HelperDAO();
        public static string Campos = "a.Id, a.Nombre, a.Tipo, a.Precio, a.IdVehiculo, b.Modelo";
        public static string Tablas = "Accesorios a JOIN Vehiculos b ON a.IdVehiculo = b.Id";

        //public List<AccesoriosDTO> CargarListaDTOs(DataTable dataTable)
        //{
        //    List<AccesoriosDTO> listaAccesorios = new List<AccesoriosDTO>();
        //    foreach (DataRow dataRow in dataTable.Rows)
        //    {
        //        AccesoriosDTO accesorioDTO = new AccesoriosDTO();
        //        accesorioDTO.Id = (int)dataRow["Id"];
        //        accesorioDTO.Tipo = (string)dataRow["Tipo"];
        //        accesorioDTO.Modelo = (string)dataRow["Modelo"];
        //        accesorioDTO.Nombre = (string)dataRow["Nombre"];
        //        accesorioDTO.Precio = (decimal)dataRow["Precio"];
        //        accesorioDTO.IdVehiculo = (int)dataRow["IdVehiculo"];
        //        listaAccesorios.Add(accesorioDTO);
        //    }
        //    return listaAccesorios;
        //}

        public List<AccesoriosDTO> CargarTabla()
        {
            return helperDAO.CargarDatos<AccesoriosDTO>(Campos, Tablas);
            //return CargarListaDTOs(HelperDAO.CargarDataTable("SELECT a.Id, a.Nombre, a.Tipo, a.Precio, a.IdVehiculo, b.Modelo " +
            //"FROM Accesorios a JOIN Vehiculos b ON a.IdVehiculo = b.Id;"));
        }

        public List<AccesoriosDTO> Buscar(string filtro)
        {
            return helperDAO.CargarDatos<AccesoriosDTO>(Campos, Tablas,
                $"a.Tipo LIKE '%{filtro}%' OR a.Nombre LIKE '%{filtro}%' OR a.Precio LIKE '%{filtro}%' OR b.Modelo LIKE '%{filtro}%'");

            //return CargarListaDTOs(HelperDAO.CargarDataTable($"SELECT a.Id, a.Nombre, a.Tipo, a.Precio, a.IdVehiculo, b.Modelo " +
            //$"FROM Accesorios a JOIN Vehiculos b ON a.IdVehiculo = b.Id " +
            //$"WHERE a.Tipo LIKE '%{filtro}%' or a.Nombre LIKE '%{filtro}%' or a.Precio " +
            //$"LIKE '%{filtro}%' or b.Modelo LIKE '%{filtro}%'"));
        }

        public AccesoriosDTO CargarTabla(int id)
        {
            return helperDAO.CargarDatos<AccesoriosDTO>(Campos, Tablas, $"a.Id={id}")[0];
            //return CargarListaDTOs(HelperDAO.CargarDataTable($"SELECT a.Id, a.Nombre, a.Tipo, a.Precio, a.IdVehiculo, b.Modelo " +
            //        $"FROM Accesorios a JOIN Vehiculos b ON a.IdVehiculo = b.Id WHERE a.Id={id}"))[0];
        }

        public List<AccesoriosDTO> CargarTablaIdVehiculo(int id)
        {
            return helperDAO.CargarDatos<AccesoriosDTO>(Campos, Tablas, $"a.IdVehiculo={id}");
            //return CargarListaDTOs(HelperDAO.CargarDataTable($"SELECT a.Id, a.Nombre, a.Tipo, a.Precio, a.IdVehiculo, b.Modelo " +
            //        $"FROM Accesorios a JOIN Vehiculos b ON a.IdVehiculo = b.Id WHERE a.IdVehiculo={id}"));
        }

        public int EliminarElemento(int id)
        {
            return HelperDAO.EditarDB($"DELETE FROM Accesorios Where Id={id} ");
        }

        public int ModificarElemento(AccesoriosDTO accesorioDTO)
        {
            return HelperDAO.EditarDB($"UPDATE Accesorios SET Nombre='{accesorioDTO.Nombre}', Tipo='{accesorioDTO.Tipo}'," +
                                $" Precio='{accesorioDTO.Precio}', IdVehiculo= '{accesorioDTO.IdVehiculo}' WHERE Id={accesorioDTO.Id}");
        }

        public int AgregarElemento(AccesoriosDTO accesorioDTO)
        {
            return HelperDAO.EditarDB($"INSERT INTO Accesorios (Nombre, Tipo, Precio, IdVehiculo)" +
                        $" VALUES ('{accesorioDTO.Nombre}', '{accesorioDTO.Tipo}', '{accesorioDTO.Precio}', '{accesorioDTO.IdVehiculo}')");
        }
    }
  
}
