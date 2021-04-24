﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HuergoMotorsForms
{
    public partial class frmAccesoriosAlta : Form
    {
        
        HuergoMotors.Negocio.AccesoriosAltaNegocio accesoriosAltaNegocio = 
            new HuergoMotors.Negocio.AccesoriosAltaNegocio();

        public HuergoMotors.DTO.AccesorioDTO AccesorioSeleccionadoDTO { get; set; }

        public int Id { get; set; }
        public HelperForms.Modo Modo { get; private set; }
        

        private void frmVehiculosAlta_Load(object sender, EventArgs e)
        {
            //Saca el focus del textbox y lo pone en el label por estetica
            ActiveControl = label1;
            cboModelos.DisplayMember = "Modelo";
            cboModelos.ValueMember = "Id";
            cboModelos.DataSource = accesoriosAltaNegocio.CargarCombo();
            if (Modo == HelperForms.Modo.Agregar)
            {
                txtNombre.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtPrecio.Text = "0.00";
                AccesorioSeleccionadoDTO = new HuergoMotors.DTO.AccesorioDTO();
            }
        }


        public frmAccesoriosAlta(HelperForms.Modo modo)
        {
            InitializeComponent();
            Modo = modo;
        }



        internal void CargarDatos()
        {
            try
            {
                AccesorioSeleccionadoDTO = accesoriosAltaNegocio.BDCargarDTO(Id);
                
                txtPrecio.Text = AccesorioSeleccionadoDTO.Precio.ToString(HuergoMotors.Negocio.HelperNegocio.nfi());
                txtTipo.Text = AccesorioSeleccionadoDTO.Tipo;
                txtNombre.Text = AccesorioSeleccionadoDTO.Nombre;
                int index = cboModelos.FindString(AccesorioSeleccionadoDTO.ModeloVehiculo);
                cboModelos.SelectedIndex = index;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
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
                accesoriosAltaNegocio.CargarDTO(AccesorioSeleccionadoDTO, (int)cboModelos.SelectedValue, 
                    (string)cboModelos.SelectedItem, txtPrecio.Text, txtNombre.Text, txtTipo.Text);

                switch (Modo)
                {
                    case HelperForms.Modo.Modificar:
                        if (HelperForms.ConfirmacionModificacion() == DialogResult.Yes)
                        {
                            HelperForms.MostrarOperacionExitosa
                                (this, Modo, accesoriosAltaNegocio.ModificarElemento (AccesorioSeleccionadoDTO));
                        }
                        break;
                    case HelperForms.Modo.Agregar:
                       
                        HelperForms.MostrarOperacionExitosa
                            (this, Modo, accesoriosAltaNegocio.AgregarElemento(AccesorioSeleccionadoDTO));
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
    }
}
