using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaResturant
{
    public partial class MenuAdministrador : Form
    {
        DataTable tabla;
        public MenuAdministrador()
        {
            InitializeComponent();

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            InsertarProducto();
        }


        public void InsertarProducto()
        {
            try
            {
                ClsBDatos conexion = new ClsBDatos();
                SqlConnection cnn = conexion.AbriConexion();
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "Rest_InsertaProducto_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@in_producto", txtProducto.Text);
                cmd.Parameters.AddWithValue("@in_descripcion", txtDescripcion.Text);
                cmd.Parameters.AddWithValue("@in_precio", txtPrecio.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Producto Insertado Correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conexion.CerrarConexion();
            }
            catch (Exception)
            {
                MessageBox.Show("Error de conexion", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }



        }



        public void ModificarProducto()
        {

            if (txtId.Text.Equals("") || txtProducto.Text.Equals("") || txtDescripcion.Text.Equals("") || txtPrecio.Text.Equals(""))
            {
                MessageBox.Show("No puede modificar sin ingresar ID de producto y los nuevos datos", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {



                try
                {



                    ClsBDatos conexion = new ClsBDatos();
                    SqlConnection cnn = conexion.AbriConexion();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = "Rest_ModificaProducto_SP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@in_id", txtId.Text);
                    cmd.Parameters.AddWithValue("@in_producto", txtProducto.Text);
                    cmd.Parameters.AddWithValue("@in_descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@in_precio", txtPrecio.Text);
                    DialogResult opcion;
                    opcion = MessageBox.Show("Realmente desea Modificar el registro ", "Sistema Restaurant", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (opcion == DialogResult.Yes)
                    {
                        int consulta = cmd.ExecuteNonQuery();
                        if (consulta < 1)
                        {
                            MessageBox.Show("ID no existe", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registro Modificado con exito", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Error no se pudo realizar la operacion", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public void EliminarProducto()
        {

            if (txtId.Text.Equals(""))
            {
                MessageBox.Show("Debe ingresar Id de Producto que desea \"Eliminar\"", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {



                try
                {
                    ClsBDatos conexion = new ClsBDatos();
                    SqlConnection cnn = conexion.AbriConexion();
                    SqlCommand cmd = cnn.CreateCommand();
                    cmd.CommandText = "Rest_EliminaProducto_SP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@in_id", txtId.Text);
                    DialogResult opcion;
                    opcion = MessageBox.Show("Realmente desea eliminar el resgitro", "Sistema Restaurant", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (opcion == DialogResult.Yes)
                    {
                        int con = cmd.ExecuteNonQuery();
                        if (con < 1)
                        {
                            MessageBox.Show("ID no existe", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registro Eliminado con exito", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }

                }
                catch (Exception)
                {

                    MessageBox.Show("Error no se pudo realizar la operacion", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void MenuAdministrador_Load(object sender, EventArgs e)
        {

            try
            {

                ClsBDatos conexion = new ClsBDatos();

                SqlConnection cnn = conexion.AbriConexion();
                string consulta = "select * from TBL_RestProductos";
                SqlCommand cmd = new SqlCommand(consulta, cnn);
                SqlDataReader leerDatos = cmd.ExecuteReader();
                tabla = new DataTable();
                tabla.Columns.Add("ID");
                tabla.Columns.Add("Producto");
                tabla.Columns.Add("Descripcion");
                tabla.Columns.Add("Precio", typeof(int));

                while (leerDatos.Read())
                {
                    string id = leerDatos["id"].ToString();
                    string producto = leerDatos["producto"].ToString();
                    string descripcion = leerDatos["descripcion"].ToString();
                    string precio = leerDatos["precio"].ToString();
                    tabla.Rows.Add(id, producto, descripcion, precio);
                }
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C2";
                DataGridViewColumn columnaPrecio = dataGridView1.Columns[3];
                columnaPrecio.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DataGridViewColumn columnaID = dataGridView1.Columns[0];
                columnaID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DataGridViewColumn columnaDesc = dataGridView1.Columns[2];
                columnaDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                DataGridViewColumn columnaProd = dataGridView1.Columns[1];
                columnaProd.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                MessageBox.Show("Error en conexion a base de datos", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarProducto();

        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            DataView dv = tabla.DefaultView;
            dv.RowFilter = "ID LIKE '" + txtId.Text + "%'";
            dataGridView1.DataSource = dv;
            dataGridView1.Visible = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ClsBDatos conexion = new ClsBDatos();
                SqlConnection cnn = conexion.AbriConexion();
                string consulta = "select * from TBL_RestProductos";
                SqlCommand cmd = new SqlCommand(consulta, cnn);
                SqlDataReader leerDatos = cmd.ExecuteReader();
                tabla = new DataTable();
                tabla.Columns.Add("ID");
                tabla.Columns.Add("Producto");
                tabla.Columns.Add("Descripcion");
                tabla.Columns.Add("Precio", typeof(int));
                while (leerDatos.Read())
                {
                    string id = leerDatos["id"].ToString();
                    string producto = leerDatos["producto"].ToString();
                    string descripcion = leerDatos["descripcion"].ToString();
                    string precio = leerDatos["precio"].ToString();
                    tabla.Rows.Add(id, producto, descripcion, precio);
                }
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.DataSource = tabla;
                dataGridView1.Columns["Precio"].DefaultCellStyle.Format = "C2";
                conexion.CerrarConexion();

            }
            catch (Exception)
            {

                MessageBox.Show("Error en conexion a base de datos", "Sistema Restaurant", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarTextBoxes(this);
            txtId.Focus();

        }
        private void limpiarTextBoxes(Control parent)
        {
            //Limpiar de manera rapida
            foreach (Control c in parent.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }

        }



        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("Realmente desea salir de la aplicación", "Sistema Restaurant", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarProducto();

        }

        private void MenuAdministrador_Activated(object sender, EventArgs e)
        {
            txtId.Focus();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            SoloNumeros(e);
        }

        private void SoloNumeros(KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo Puede ingresar Numeros", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
