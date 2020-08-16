using System.Data.SqlClient;

namespace SistemaResturant
{
    class ClsBDatos
    {

        public SqlConnection cnn = new SqlConnection("SERVER=LEONOVO-LEGION-\\SQLEXPRESS;" +
                                                     "DATABASE=SistemaRestaurant;" +
                                                     "USER ID =sa;Password=123;" +
                                                     "Connect Timeout=30;");

        public SqlConnection AbriConexion()
        {
            cnn.Open();
            return cnn;
        }

        public SqlConnection CerrarConexion()
        {
            cnn.Close();
            return cnn;
        }


    }
}
