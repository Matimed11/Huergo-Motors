﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HuergoMotorsVentas
{
    public partial class frmVentasAlta : Form

    {
        private DataTable dtAccesorios;
        private int idCliente;
        private decimal precioVehiculo;
        private decimal precioAccesorios;
        public frmVentasAlta()
        {
            InitializeComponent();
        }

        private void frmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                Helper.CargarCombo(cboVendedor, "SELECT Id, Nombre + ' ' + Apellido AS Vendedor FROM Vendedores", "Vendedor");
                Helper.CargarCombo(cboModelo, "SELECT  Id, Modelo FROM Vehiculos", "Modelo");
                gvAccesorios.AutoGenerateColumns = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void cboVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Helper.VerificarCombosCargados(cboVendedor))
                {
                    txtSucursal.Text = Helper.LeerDatoCombo(cboVendedor, "Sucursal", "Vendedores");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void cboModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Helper.VerificarCombosCargados(cboModelo))
                {
                    if (dtAccesorios != null)
                    {
                        precioAccesorios = 0;
                        dtAccesorios.Clear();
                        gvAccesorios.DataSource= dtAccesorios;
                    }
                    DataTable dtModelo = Helper.LeerCombo(cboModelo, "*", "Vehiculos");
                    txtTipo.Text = (string)dtModelo.Rows[0]["Tipo"];
                    precioVehiculo = (decimal)dtModelo.Rows[0]["PrecioVenta"];
                    txtPrecio.Text = Convert.ToString(Precio);
                    int idVehiculo = (int)dtModelo.Rows[0]["Id"];
                    Helper.CargarCombo(cboAccesorios, $"SELECT Nombre, Id FROM Accesorios WHERE idVehiculo = {idVehiculo}", "Nombre");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboAccesorios.Items.Count > 0)
                {
                    DataTable dtNuevosDatos = Helper.LeerCombo(cboAccesorios, "*", "Accesorios");
                    if (dtAccesorios != null && dtAccesorios.Rows.Count > 0)
                    {
                        dtAccesorios.Merge(dtNuevosDatos);
                    }
                    else
                    {
                        precioAccesorios = 0;
                        dtAccesorios = dtNuevosDatos;
                    }
                    gvAccesorios.DataSource = dtAccesorios;
                    precioAccesorios = precioAccesorios + (decimal)dtNuevosDatos.Rows[0]["Precio"];
                }
                else
                {
                    throw new Exception("No puede agregar un accesorio inexistente");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        
        } 

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            using (frmClientes f = new frmClientes())
            {
                f.Seleccionar();

                if (f.ShowDialog() == DialogResult.OK)
                {
                    txtNombreCliente.Text = f.ClienteSeleccionado.Nombre;
                    txtEmail.Text = f.ClienteSeleccionado.Email;
                    txtTelefono.Text = f.ClienteSeleccionado.Telefono;
                    idCliente = f.ClienteSeleccionado.Id;
                }
                else
                {
                    txtNombreCliente.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtTelefono.Text = string.Empty;
                }
            }
        }

        private void btnConfirmarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                Helper.ValidarTextosVacios(txtEmail, txtNombreCliente, txtTelefono, txtSucursal, txtTipo, txtPrecio);
                if (!Helper.VerificarCombosCargados(cboModelo, cboVendedor))
                {
                    throw new Exception("Es necesario que todos los ComboBox esten cargados");
                }
                if (Helper.LeerNumeroCombo(cboModelo, "Stock", "Vehiculos") < 1)
                {
                    throw new Exception("No hay stock del vehiculo seleccionado");
                }
                DateTime fecha = dtpFecha.Value;
                int idVehiculo = (int)cboModelo.SelectedValue;
                int idVendedor = (int)cboVendedor.SelectedValue;
                string observaciones = string.Empty;
                if (!string.IsNullOrEmpty(txtObservaciones.Text) &
                    (txtObservaciones.Text!= "Observaciones:"| txtObservaciones.Text != "Observaciones"))
                {
                    observaciones = txtObservaciones.Text;
                }
                try
                {
                    using (SqlConnection conexion = new SqlConnection(Helper.ConnectionString))
                    {
                        conexion.Open();
                        using (SqlTransaction transaction = conexion.BeginTransaction())
                        {
                            Helper.EditarDB($"INSERT INTO Ventas(Fecha, IdVehiculo, IdCliente, IdVendedor, Observaciones, Total) " +
                            $"VALUES ('{fecha}', '{idVehiculo}', '{idCliente}', '{idVendedor}', '{observaciones}','{precioVehiculo}')", transaction);
                            //Falta restar el stock y hacer la venta de los accesorios
                            transaction.Commit;
                        }
                    }
                }
                catch
                { 
                }
                DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void txtObservaciones_Enter(object sender, EventArgs e)
        {
            txtObservaciones.Text = "";
        }

        private void txtObservaciones_Validated(object sender, EventArgs e)
        {
            if (txtObservaciones.Text == "") txtObservaciones.Text = "Observaciones:"; 
        }

        private void gvAccesorios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) //El index 5 es el del boton eliminar
            {
                dtAccesorios.Rows.RemoveAt(gvAccesorios.CurrentRow.Index);
                gvAccesorios.DataSource = dtAccesorios;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}