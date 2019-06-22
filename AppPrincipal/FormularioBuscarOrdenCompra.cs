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
    public partial class FormularioBuscarOrdenCompra : Form
    {
        
        private FormularioRecepcion FrmRecepcion;
        private FormularioMantenedorOrdenCompra FrmOrdenCompra;
        
        //inicia en formulario mantenedor de recepcion
        public FormularioBuscarOrdenCompra(FormularioMantenedorOrdenCompra parametro)
        {
            InitializeComponent();
            

            FrmOrdenCompra = parametro;
           try
            {
               ServicioOrdenCompra serp = new ServicioOrdenCompra();
                DgMostrarOrdenCompra.DataSource = serp.ListarOrdenCompra();

                this.DgMostrarOrdenCompra.Columns["idEmpleado"].Visible = false;
                this.DgMostrarOrdenCompra.Columns["rutEmpleado"].Visible = false;

                //DA NOMBRE A LAS COLUMNAS
                this.DgMostrarOrdenCompra.Columns["idOrdenCompra"].HeaderText = "CODIGO";
                this.DgMostrarOrdenCompra.Columns["nombreEmpleado"].HeaderText = "EMPLEADO";
                this.DgMostrarOrdenCompra.Columns["estadoOrdenCompra"].HeaderText = "ESTADO";
                this.DgMostrarOrdenCompra.Columns["fechaSolicitudOrdenCompra"].HeaderText = "FECHA SOLICITUD";
                this.DgMostrarOrdenCompra.Columns["fechaRecepcionOrdenCompra"].HeaderText = "FECHA RECEPCION";
            }
            catch (Exception)
            {
                MessageBox.Show("NO SE PUEDE CARGAR LISTADO DE ORDEN DE COMPRA");
            }
        }

       
        public FormularioBuscarOrdenCompra(FormularioRecepcion parametro)
        {
            InitializeComponent();


            FrmRecepcion = parametro;
            try
            {
                ServicioOrdenCompra serp = new ServicioOrdenCompra();
                DgMostrarOrdenCompra.DataSource = serp.ListarOrdenCompra();
           }
            catch (Exception)
            {
                MessageBox.Show("NO SE PUEDE CARGAR LISTADO DE ORDEN DE COMPRA");
            }
        }

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
                DgMostrarOrdenCompra.CurrentCell = null;
                foreach (DataGridViewRow r in DgMostrarOrdenCompra.Rows)
                {
                    r.Visible = false;
                }
                foreach (DataGridViewRow r in DgMostrarOrdenCompra.Rows)
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
                 ServicioOrdenCompra ser = new ServicioOrdenCompra();
                DgMostrarOrdenCompra.DataSource = ser.ListarOrdenCompra();
            }
        }

        private void DgMostrarOrdenCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //detallar campos*******************************************
            try
             {
                 try
                 {
                    //CARGA LOS DATOS DE LA ORDEN DE COMPRA EN EL FORMULARIO DE MANTENEDOR DE ORDEN DE COMPRA
                    //SE CASTEA EL OBJETO ORDEN COMPRA
                    Orden_Compra oc = (Orden_Compra)DgMostrarOrdenCompra.CurrentRow.DataBoundItem;
                    ServicioOrdenCompra ser = new ServicioOrdenCompra();
                    BindingList<DetalleOrdenCVista> detCv = new BindingList<DetalleOrdenCVista>();
                    List<DetalleOrdenCompra> lista = ser.subdetalleOrdenCompra(oc);
  
                   foreach (DetalleOrdenCompra detoc in lista)
                  {
                     DetalleOrdenCVista ClaseDetalleCv = new DetalleOrdenCVista();
                     ClaseDetalleCv.CODIGO = detoc.codigoProducto.ToString();
                     ClaseDetalleCv.NOMBRE = detoc.nombreProducto;
                     ClaseDetalleCv.CANTIDAD = detoc.cantidadProducto;

                     detCv.Add(ClaseDetalleCv);
                   }

                    FrmOrdenCompra.DgListadoProductoOC.DataSource = detCv;
                    FrmOrdenCompra.detalleOC = lista;

                    FrmOrdenCompra.TxtNumero.Text = DgMostrarOrdenCompra.CurrentRow.Cells["idOrdenCompra"].Value.ToString();
                    FrmOrdenCompra.CbEmpleado.Text = DgMostrarOrdenCompra.CurrentRow.Cells["nombreEmpleado"].Value.ToString();
                    FrmOrdenCompra.CbEstado.Text = DgMostrarOrdenCompra.CurrentRow.Cells["estadoOrdenCompra"].Value.ToString();
                    FrmOrdenCompra.DPfechaInicio.Text = DgMostrarOrdenCompra.CurrentRow.Cells["fechaSolicitudOrdenCompra"].Value.ToString();
                    FrmOrdenCompra.DPfechaTermino.Text = DgMostrarOrdenCompra.CurrentRow.Cells["fechaRecepcionOrdenCompra"].Value.ToString();

                    this.Close();

           
               }
                catch (Exception)
                {

                    //CARGA LOS DATOS DE LA ORDEN DE COMPRA EN EL FORMULARIO RECEPCION ORDEN COMPRA 
                    //SE CASTEA EL OBJETO ORDEN COMPRA
                    Orden_Compra oc = (Orden_Compra)DgMostrarOrdenCompra.CurrentRow.DataBoundItem;
                    ServicioOrdenCompra ser = new ServicioOrdenCompra();
                    BindingList<DetalleOrdenCVista> detCv = new BindingList<DetalleOrdenCVista>();
                    List<DetalleOrdenCompra> lista = ser.subdetalleOrdenCompra(oc);


                    foreach (DetalleOrdenCompra detoc in lista)
                    {
                        DetalleOrdenCVista ClaseDetalleCv = new DetalleOrdenCVista();
                        ClaseDetalleCv.CODIGO = detoc.codigoProducto.ToString();
                        ClaseDetalleCv.NOMBRE = detoc.nombreProducto;
                        ClaseDetalleCv.CANTIDAD = detoc.cantidadProducto;

                        detCv.Add(ClaseDetalleCv);
                    }

                    FrmRecepcion.DgListadoRecepcion.DataSource = detCv;
                    FrmRecepcion.detalleOC = lista;

                    FrmRecepcion.TxtNumero.Text = DgMostrarOrdenCompra.CurrentRow.Cells["idOrdenCompra"].Value.ToString();
                    FrmRecepcion.TxtIdEmpleado.Text = DgMostrarOrdenCompra.CurrentRow.Cells["idEmpleado"].Value.ToString();
                    FrmRecepcion.TxtEmpleado.Text = DgMostrarOrdenCompra.CurrentRow.Cells["nombrePersonaEmpleado"].Value.ToString();
                    FrmRecepcion.CbxEstadoRecepcion.Text = DgMostrarOrdenCompra.CurrentRow.Cells["estadoOrdenCompra"].Value.ToString();
                    FrmRecepcion.DpFechaCreacion.Text = DgMostrarOrdenCompra.CurrentRow.Cells["fechaSolicitudOrdenCompra"].Value.ToString();
                    FrmRecepcion.DpFechaRecepcion.Text = DgMostrarOrdenCompra.CurrentRow.Cells["fechaRecepcionOrdenCompra"].Value.ToString();
                   // FrmRecepcion.DgListadoRecepcion.Text = DgMostrarOrdenCompra.CurrentRow.Cells[5].Value.ToString();

                    this.Close();
                }
            }
            catch (Exception)
            {
               MessageBox.Show("ERROR AL AGREGAR ORDEN COMPRA");
            }

        }

        //lista detalle orden compra
        private void ListaOrdenC()
        {

            try
            {
                DgMostrarOrdenCompra.DataSource = new BindingList<DetalleOrdenCompra>();
            }
            catch (Exception)
            {
                MessageBox.Show("NO SE PUEDE CARGAR LISTA");
            }

        }
    }
}
