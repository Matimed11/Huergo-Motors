﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HuergoMotors.Negocio;
using System.Web.UI.WebControls;

namespace HuergoMotors.Web
{
    public partial class webVentas : System.Web.UI.Page
    {
        VentasNegocio ventasNegocio = new VentasNegocio();
        private const string AM = "/AMS/webVentasAlta.aspx";
        private const string Detalle = "/webVentasAccesorios.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lbMsg.Text = string.Empty;
                if (!Page.IsPostBack)   //Esta es solo la primera vez que entra a la pagina.
                {
                    ucBuscador.Filtro.Text = string.Empty;
                    CargarTabla();
                }
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }
       
        protected void btnRecargar_Click(object sender, EventArgs e)
        {
            CargarTabla();
            ucBuscador.Filtro.Text = string.Empty;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                gv.DataSource = ventasNegocio.Buscar(ucBuscador.Filtro.Text);
                gv.DataBind();
            }
            catch (Exception ex)
            {
                lbMsg.Text = ex.Message;
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect(AM);
        }

        protected void CargarTabla()
        {
            gv.DataSource = ventasNegocio.CargarTablaVentas();
            gv.DataBind();
        }

        protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["Id"]);

            if (e.CommandName == "Detalle")
            {
                Response.Redirect(Detalle + "?id=" + id);
            }
        }
    }
}