﻿using HuergoMotors.DTO;
using System;
using System.Windows.Forms;

namespace HuergoMotorsForms
{
    public partial class frmVehiculosAlta : Form
    {
        HuergoMotors.Negocio.VehiculosNegocio vehiculosNegocio = new HuergoMotors.Negocio.VehiculosNegocio();
        public VehiculosDTO VehiculoSeleccionadoDTO {get; set;}
        public HelperForms.Modo Modo { get; private set; }

        public frmVehiculosAlta(HelperForms.Modo modo)
        {
            InitializeComponent();
            Modo = modo;
        }

        private void frmVehiculosAlta_Load(object sender, EventArgs e)
        {
            //Saca el focus del textbox y lo pone en el label por estetica
            this.ActiveControl = label1;

            if (Modo == HelperForms.Modo.Agregar)
            {
                // Vaciar todos los txtbox
                txtModelo.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtPrecio.Text = "0.00";
                txtStock.Text = "0";
                VehiculoSeleccionadoDTO = new HuergoMotors.DTO.VehiculosDTO();
            }
        }

        internal void CargarDatos()
        {
            try
            {
                txtPrecio.Text = VehiculoSeleccionadoDTO.PrecioVenta.ToString();
                txtModelo.Text = VehiculoSeleccionadoDTO.Modelo;
                txtTipo.Text = VehiculoSeleccionadoDTO.Tipo;
                txtStock.Text = VehiculoSeleccionadoDTO.Stock.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

       
        private void btAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                HelperForms.ValidarTextosVacios(txtModelo.Text, txtPrecio.Text, txtStock.Text, txtTipo.Text);
                HelperForms.ValidarID(VehiculoSeleccionadoDTO.Id);
                VehiculosDTO nuevoVehiculo = vehiculosNegocio.CargarDTO(VehiculoSeleccionadoDTO.Id,
                    txtModelo.Text, HelperForms.ConvertirNumeroRacional(txtPrecio.Text), 
                    HelperForms.ConvertirNumeroNatural(txtStock.Text), txtTipo.Text);
                switch (Modo)
                {
                    case HelperForms.Modo.Modificar:
                        if (HelperForms.ConfirmacionModificacion() == DialogResult.Yes)
                        {
                            HelperForms.OperacionExitosa(this, Modo, 
                                vehiculosNegocio.ModificarElemento(nuevoVehiculo));
                        }
                        break;
                    case HelperForms.Modo.Agregar:
                        HelperForms.OperacionExitosa(this, Modo,
                            vehiculosNegocio.AgregarElemento(nuevoVehiculo));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

