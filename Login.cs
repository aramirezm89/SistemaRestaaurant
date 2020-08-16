using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SistemaResturant
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();


        }




        public void Ingresar()
        {
            try
            {

                ClsBDatos conexion = new ClsBDatos();
                SqlConnection cnn = conexion.AbriConexion();
                SqlCommand cmd = cnn.CreateCommand();
                cmd.CommandText = "Rest_BuscaUsuario_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@in_usuario", txtUsuario.Text);
                cmd.Parameters.Add("@in_pass", txtPass.Text);
                SqlDataReader LeerDatos = cmd.ExecuteReader(); //lee lo datos que hay en la tabla SQL

                if ((LeerDatos.Read() == true && LeerDatos["tipo"].Equals("administrador")))
                {
                    MessageBox.Show("Bienvenido(a)" + " " + LeerDatos.GetString(1), "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MenuAdministrador frm = new MenuAdministrador();
                    frm.Show();
                    conexion.CerrarConexion();
                    this.Hide();

                }
                else if (LeerDatos["tipo"].Equals("usuario"))
                {
                    MessageBox.Show("Bienvenido(a)" + " " + LeerDatos.GetString(1), "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MenuUsuario frm = new MenuUsuario();
                    frm.Show();
                    conexion.CerrarConexion();
                    this.Hide();
                }


            }
            catch (Exception)
            {


                MessageBox.Show("Usuario no existe", "Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Clear();
                txtPass.Clear();
                txtUsuario.Focus();
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Equals("") || txtPass.Text.Equals(""))
            {
                MessageBox.Show("Debe ingresar usuario y contraseña", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsuario.Focus();
            }
            else
            {
                Ingresar();

            }



        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtUsuario.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult opcion;
            opcion = MessageBox.Show("realmente desea salir de la aplicación", "Sistema Restaurant", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (opcion == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUsuario.Text.Equals("") || txtPass.Text.Equals(""))
                {
                    MessageBox.Show("Debe ingresar usuario y contraseña", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsuario.Focus();
                }
                else
                {
                    Ingresar();

                }
            }
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPass.Focus();
            }
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
    }

}
