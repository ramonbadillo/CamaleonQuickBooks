
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace BillboardManagerBeta
{
    class Configuration
    {
        public static void GetDataBases(ComboBox comboBox1, string server, string port, string username, string password)
        {
            string conectionString = "datasource = " + server + " ; Port = " + port + " ; User Id = " + username + " ; password = " + password + " ;";
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(conectionString);
                connection.Close();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SHOW DATABASES;";
                MySqlDataReader reader;
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string row = "";
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row += reader.GetValue(i).ToString() + "";
                    }
                    comboBox1.Items.Add(row);
                }
                connection.Close();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
