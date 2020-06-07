using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pruebaImagen
{
    public partial class frmRegistrar : Form
    {
        SqlConnection cn;
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;

        public frmRegistrar()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //tamaño de las celdas auto size
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //        Aqui iria el nombre de tu servidor               nombre de la base de datos a cargar
            string nombreServidor = "LAPTOP-L0KF26J2\\SQLEXPRESS", nombreBasedatos = "pruebaImagen";
            // creamos la conexion a nuestro servidor
            cn = new SqlConnection("Data Source = " + nombreServidor + "; Initial Catalog = " + nombreBasedatos + "; Integrated Security = True");
            try
            {
                // mandamos un select para actualizarlo y poder verlo
                da = new SqlDataAdapter("SELECT *FROM empleado", cn);
                dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido mostrar la tabla, código de error: \n" + ex);
            }
        }
        //Region para diseño
        #region Efecto Hover
        private void txtId_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtId.Text == "ID del Empleado")
            {
                txtId.Clear();
                txtId.ForeColor = Color.FromArgb(56, 84, 123);
            }
        }
        private void txtId_MouseLeave(object sender, EventArgs e)
        {
            if (txtId.Text == String.Empty)
            {
                txtId.Text = "ID del Empleado";
                txtId.ForeColor = Color.FromArgb(171, 174, 205);
            }
        }
        private void txtNombre_MouseLeave(object sender, EventArgs e)
        {
            if (txtNombre.Text == String.Empty)
            {
                txtNombre.Text = "Nombre del empleado";
                txtNombre.ForeColor = Color.FromArgb(171, 174, 205);
            }
        }
        private void txtNombre_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (txtNombre.Text == "Nombre del empleado")
            {
                txtNombre.Clear();
                txtNombre.ForeColor = Color.FromArgb(56, 84, 123);
            }
        }
        #endregion
        private void cmdCargarFoto_Click(object sender, EventArgs e)
        {
            //textodeformato (*.formato)|*.formato| otroformato (*.formato2*)|*.formato2
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "C:\\";
            ofd.Filter = "Archivos jpg(*.jpg)|*.jpg| Archivos png(*.png)|*.png";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbImagen.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void cmdRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.Connection = cn;
                //Insertamos
                cmd.CommandText = "INSERT INTO empleado VALUES(" + txtId.Text + ",'" + txtNombre.Text + "',@foto)";
                MemoryStream ms = new MemoryStream();
                pbImagen.Image.Save(ms, ImageFormat.Jpeg);
                cmd.Parameters["@foto"].Value = ms.GetBuffer();
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                da = new SqlDataAdapter("SELECT *FROM empleado", cn);
                dt = new DataTable();
                da.Fill(dt);
                dgv.DataSource = dt;

                txtId.Text = "ID del Empleado";
                txtNombre.Text = "Nombre del empleado";
                pbImagen.Image = Properties.Resources.usuario1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido mostrar la tabla, código de error: \n" + ex);
            }
        }
    }
}