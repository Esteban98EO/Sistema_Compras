﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Sistema.Negocio;
using System.Drawing.Imaging;
using System.IO;

namespace Sistema.Presentacion
{
    public partial class FrmArticulo : Form
    { 
        private string RutaOrigen;
        private string RutaDestino;
        private string Directorio = "C:\\Users\\acer\\Desktop\\SEXTO SEMESTRE\\Desarrolo e implementacion de sistemas de informacion\\SISTEMA DE COMPRAS POR REQUISICION\\SISTEMA\\Imagenes\\";
        private string NombreAnt;
        public FrmArticulo()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registro:" + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NArticulo.Buscar(TxtBuscar.Text);
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registro:" + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }

        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[1].Width = 50;
            DgvListado.Columns[3].Width = 100;
            DgvListado.Columns[3].HeaderText = "Categoria";
            DgvListado.Columns[4].Width = 100;
            DgvListado.Columns[4].HeaderText = "Codigo";
            DgvListado.Columns[5].Width = 150;
            DgvListado.Columns[6].Width = 100;
            DgvListado.Columns[6].HeaderText = "Precio Compra";
            DgvListado.Columns[7].Width = 60;
            DgvListado.Columns[8].Width = 200;
            DgvListado.Columns[8].HeaderText = "Descripcion";
            DgvListado.Columns[9].Width = 100;

        }

        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtNombre.Clear();
            TxtId.Clear();
            TxtCodigo.Clear();
            PanelCodigo.BackgroundImage = null;
            BtnGuardarCodigo.Enabled = true;
            TxtPrecioVenta.Clear();
            TxtStock.Clear();
            TxtImagen.Clear();
            PicImagen.Image = null;
            TxtDescripcion.Clear();
            BtnInsertar.Visible = true;
            BtnActualizar.Visible = false;
            ErrorIcono.Clear();
            this.RutaDestino = "";
            this.RutaOrigen = "";

            DgvListado.Columns[0].Visible = false;
            BtnActivar.Visible = false;
            BtnDesactivar.Visible = false;
            BtnEliminar.Visible = false;
            ChkSeleccionar.Checked = false;

        }

        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOK(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnCargarImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files(*.jpg,*.jpeg,*.jpe,*.jfif,*.png) | *.jpg;*.jpeg;*.jpe;*.jfif;*.png";
            if (file.ShowDialog() == DialogResult.OK)
            {
                PicImagen.Image = Image.FromFile(file.FileName);
                TxtImagen.Text = file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                this.RutaOrigen = file.FileName;
            }
        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DgvListado.Columns["Seleccionar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)DgvListado.Rows[e.RowIndex].Cells["Seleccionar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
           Reportes.FrmReporteRegistroProductos Reporte = new Reportes.FrmReporteRegistroProductos();
            Reporte.ShowDialog();
        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSeleccionar.Checked)
            {
                DgvListado.Columns[0].Visible = true;
                BtnActivar.Visible = true;
                BtnDesactivar.Visible = true;
                BtnEliminar.Visible = true;
            }
            else
            {
                DgvListado.Columns[0].Visible = false;
                BtnActivar.Visible = false;
                BtnDesactivar.Visible = false;
                BtnEliminar.Visible = false;
            }
        }

        private void BtnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas activar el producto?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.Activar(Codigo);


                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se activo el producto:" + Convert.ToString(row.Cells[5].Value));
                            }

                            else
                            {
                                this.MensajeOK(Rpta);
                            }


                        }
                    }

                    this.Listar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas Desactivar el producto?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Rpta = NArticulo.Desactivar(Codigo);


                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se desactivo el producto:" + Convert.ToString(row.Cells[5].Value));
                            }

                            else
                            {
                                this.MensajeOK(Rpta);
                            }


                        }
                    }

                    this.Listar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Realmente deseas eliminar el registro?", "Sistema de ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    int Codigo;
                    string Rpta = "";
                    string Imagen = "";

                    foreach (DataGridViewRow row in DgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToInt32(row.Cells[1].Value);
                            Imagen = Convert.ToString(row.Cells[9].Value);
                            Rpta = NArticulo.Eliminar(Codigo);


                            if (Rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino el registro:" + Convert.ToString(row.Cells[5].Value));
                                File.Delete(this.Directorio + Imagen);
                            }

                            else
                            {
                                this.MensajeOK(Rpta);
                            }


                        }
                    }

                    this.Listar();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                BtnActualizar.Visible = true;
                BtnInsertar.Visible = false;
                TxtId.Text = Convert.ToString(DgvListado.CurrentRow.Cells["ID"].Value);
                this.NombreAnt = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                TxtNombre.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Nombre"].Value);
                // CboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["idcategoria"].Value);
                CboCategoria.SelectedValue = Convert.ToString(DgvListado.CurrentRow.Cells["id_categoria"].Value);
                TxtCodigo.Text = Convert.ToString(DgvListado.CurrentRow.Cells["Codigo"].Value);
                
                TxtPrecioVenta.Text = Convert.ToString(DgvListado.CurrentRow.Cells["precio_venta"].Value);
                TxtStock.Text = Convert.ToString(DgvListado.CurrentRow.Cells["stock"].Value);
                TxtDescripcion.Text = Convert.ToString(DgvListado.CurrentRow.Cells["descripcion"].Value);
                string Imagen;
                Imagen = Convert.ToString(DgvListado.CurrentRow.Cells["imagen"].Value);
                if (Imagen != string.Empty)
                {
                    PicImagen.Image = Image.FromFile(this.Directorio + Imagen);
                    TxtImagen.Text = Imagen;
                }
                else
                {
                    PicImagen.Image = null;
                    TxtImagen.Text = "";
                }
                TapGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seleccione desde la celda nombre " + " | Error:" + ex.Message);
            }
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta agregar algunos datos, sera remarcados");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una clase.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese precio");
                    ErrorIcono.SetError(TxtStock, "Ingrese un Cantidad inicial");
                }

                else
                {
                    Rpta = NArticulo.Insertar(Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(), TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se inserto de forma correcta el registro");
                        if (TxtImagen.Text != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string Rpta = "";
                if (TxtId.Text == string.Empty || CboCategoria.Text == string.Empty || TxtNombre.Text == string.Empty || TxtPrecioVenta.Text == string.Empty || TxtStock.Text == string.Empty)
                {
                    this.MensajeError("Falta agregar algunos datos, sera remarcados");
                    ErrorIcono.SetError(CboCategoria, "Seleccione una categoria.");
                    ErrorIcono.SetError(TxtNombre, "Ingrese un nombre.");
                    ErrorIcono.SetError(TxtPrecioVenta, "Ingrese un precio");
                    ErrorIcono.SetError(TxtStock, "Ingrese un Stock inicial");
                }

                else
                {
                    Rpta = NArticulo.Actualizar(Convert.ToInt32(TxtId.Text), Convert.ToInt32(CboCategoria.SelectedValue), TxtCodigo.Text.Trim(), this.NombreAnt, TxtNombre.Text.Trim(), Convert.ToDecimal(TxtPrecioVenta.Text), Convert.ToInt32(TxtStock.Text), TxtDescripcion.Text.Trim(), TxtImagen.Text.Trim());
                    if (Rpta.Equals("OK"))
                    {
                        this.MensajeOK("Se actualizo de forma correcta el registro");
                        if (TxtImagen.Text != string.Empty && this.RutaOrigen != string.Empty)
                        {
                            this.RutaDestino = this.Directorio + TxtImagen.Text;
                            File.Copy(this.RutaOrigen, this.RutaDestino);
                        }
                        this.Listar();
                        TapGeneral.SelectedIndex = 0;
                    }
                    else
                    {
                        this.MensajeError(Rpta);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
            TapGeneral.SelectedIndex = 0;
        }

        private void BtnGuardarCodigo_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)PanelCodigo.BackgroundImage.Clone();

            SaveFileDialog DiaologoGuardar = new SaveFileDialog();
            DiaologoGuardar.AddExtension = true;
            DiaologoGuardar.Filter = "Image PNG(*.png)|*.png";
            DiaologoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(DiaologoGuardar.FileName))
            {
                imgFinal.Save(DiaologoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void BtnGenerar_Click(object sender, EventArgs e)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            PanelCodigo.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, TxtCodigo.Text, Color.Black, Color.White, 400, 100);
            BtnGuardarCodigo.Enabled = true;
        }

        private void CboCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CargarCategoria();
        }

       
        private void CargarCategoria()
        {
            try
            {
                CboCategoria.DataSource = NCategoria.Seleccionar();
                CboCategoria.ValueMember = "idcategoria";
                CboCategoria.DisplayMember = "nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


    }
       
}

