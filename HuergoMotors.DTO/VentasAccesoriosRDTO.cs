﻿namespace HuergoMotors.DTO
{
    public class VentasAccesoriosRDTO
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdAccesorio { get; set; }
        public decimal Precio { get; set; }
        public string NombreAccesorio { get; set; }
        public string TipoAccesorio { get; set; }


    }
}