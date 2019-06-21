﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Biblioteca;
using ServiciosConexionFerme;

namespace AppPrincipal
{
    public partial class FormularioBuscarVenta : Form
    {
        private FromularioVentas frmventas;
        public FormularioBuscarVenta(FromularioVentas parametro)
        {
            InitializeComponent();


            frmventas = parametro;
            try
            {
                ServicioVentas ser = new ServicioVentas();
                DgMostrarVentasRealizadas.DataSource = ser.ListarVenta();
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL CARGAR VENTA");
            }
        }


        //BOTON CANCELAR
        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show("DESEA CERRAR LA APLICACION ? ","",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Exclamation);

                MessageBoxButtons botones = MessageBoxButtons.YesNoCancel;
                DialogResult dr = MessageBox.Show("¿Está seguro que desea salir?", "", botones, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }

            }
            catch
            {
                MessageBox.Show("Error al cerrar Aplicacion");
            }
        }

        private void TxtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (TxtBuscar.Text != "")
            {
                DgMostrarVentasRealizadas.CurrentCell = null;
                foreach (DataGridViewRow r in DgMostrarVentasRealizadas.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in DgMostrarVentasRealizadas.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if ((c.Value.ToString().ToUpper()).IndexOf(TxtBuscar.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }

                }
            }
            else
            {
                 ServicioVentas ser = new ServicioVentas();
                 DgMostrarVentasRealizadas.DataSource = ser.ListarVenta();
            }
        }


        //SELECCIONAR UNA FILA Y LA ENVIA AL FORMULARIO VENTAS PARA VISUALIZARLA
        private void DgMostrarVentasRealizadas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          /*  try
            {*/

                //CARGA LOS DATOS DE LA ORDEN DE COMPRA EN EL FORMULARIO DE MANTENEDOR DE ORDEN DE COMPRA
                //SE CASTEA EL OBJETO ORDEN COMPRA
                Venta oc = (Venta)DgMostrarVentasRealizadas.CurrentRow.DataBoundItem;
                ServicioVentas ser = new ServicioVentas();
                BindingList<DetalleVentaVista> detCv = new BindingList<DetalleVentaVista>();
                List<Detalle_Venta> lista = ser.subdetalleventa(oc);

                foreach (Detalle_Venta detVenta in lista)
                {
                    DetalleVentaVista ClaseDetalleV = new DetalleVentaVista();
                    ClaseDetalleV.CODIGO = detVenta.codigoProducto;
                    ClaseDetalleV.NOMBRE = detVenta.nombreProducto;
                    ClaseDetalleV.CANTIDAD = detVenta.unidadesProducto;
                    ClaseDetalleV.MONTO = detVenta.montoDetalleVenta;
                    ClaseDetalleV.SUBTOTAL = detVenta.Subtotal;

                    detCv.Add(ClaseDetalleV);
                }

                frmventas.DgVentaProducto.DataSource = detCv;
                frmventas.detalleVen = lista;


                frmventas.TxtNumeroDocumento.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[0].Value.ToString();
                frmventas.CbTipoDocto.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[1].Value.ToString();
                frmventas.DpFecha.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[2].Value.ToString();
                frmventas.CbEmpleado.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[3].Value.ToString();
                frmventas.TxtRut.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[4].Value.ToString();
                frmventas.TxtTotal.Text = DgMostrarVentasRealizadas.CurrentRow.Cells[5].Value.ToString();


                this.Close();
           /* }
            catch (Exception)
            {
                MessageBox.Show("ERROR AL SELECCIONAR VENTA");
            }*/
        }

        //lista detalle orden compra
        private void ListaVentas()
        {

            try
            {
                DgMostrarVentasRealizadas.DataSource = new BindingList<Detalle_Venta>();
            }
            catch (Exception)
            {
                MessageBox.Show("NO SE PUEDE CARGAR LISTA");
            }

        }
    }
}
