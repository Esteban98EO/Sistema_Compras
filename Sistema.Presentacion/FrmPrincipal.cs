using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmPrincipal : Form
    {
        private int childFormNumber = 0;
        public int IdUsuario;
        public int IdRol;
        public string Nombre;
        public string Rol;
        public bool Estado;

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana" + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {

        }

        private void periodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario frm = new Presentacion.FrmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
           
            StBarraInferior.Text = "Made by. Esteban, Usuario:" + this.Nombre;
            MessageBox.Show("Bienvenido: " + this.Nombre, "Sistema de Compras", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (this.Rol.Equals("Administrador"))
            {
                MnuRegistro.Enabled = true;
                MnuCompras.Enabled = true;
                MnuAccesos.Enabled = true;
                MnuConsultas.Enabled = true;
                
            }
            else
            {
                if (this.Rol.Equals("Gerente"))
                {
                    MnuRegistro.Enabled = false;
                    MnuCompras.Enabled = false;
                    MnuAccesos.Enabled = false;
                    MnuConsultas.Enabled = true;
                    
                }
                else
                {
                    if (this.Rol.Equals("Gestor Tecnico"))
                    {
                        MnuRegistro.Enabled = true;
                        MnuCompras.Enabled = true;
                        MnuAccesos.Enabled = false;
                        MnuConsultas.Enabled = true;
                        
                    }
                    else
                    {
                        MnuRegistro.Enabled = false;
                        MnuCompras.Enabled = false;
                        MnuAccesos.Enabled = false;
                        MnuConsultas.Enabled = false;
                        
                    }
                }
            }
        }

        private void ususariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario frm = new FrmUsuario();
            frm.MdiParent = this;
            frm.Show();
        }

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRol frm = new FrmRol();
            frm.MdiParent = this;
            frm.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Opcion;
            Opcion = MessageBox.Show("Deseas salir del Sistema?", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (Opcion == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCategoria frm = new Presentacion.FrmCategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void articuloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticulo frm = new FrmArticulo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void proovedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProveedor frm = new FrmProveedor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRequisicion frm = new FrmRequisicion();
            frm.MdiParent = this;
            frm.Show();
        }

        private void ConsultaCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
