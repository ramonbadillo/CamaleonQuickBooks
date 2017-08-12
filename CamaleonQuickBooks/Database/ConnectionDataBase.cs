using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace BillboardManagerBeta
{
    public class ComboboxItem
    {
        public string Text
        {
            get; set;
        }

        public object Value
        {
            get; set;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    public class ConnectionDataBase
    {
        public static string connectionString = "datasource = localhost; port = 3306; username = root; password = antonio; database= smmorelos6;";

        public static List<ComboboxItem> GetSpecials(ComboBox comboBox1)
        {
            List<ComboboxItem> specialsList = new List<ComboboxItem>();
            int dayOfWeek = (int)DateTime.Now.DayOfWeek;
            dayOfWeek++;
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Id , descripcion , DateIn, DateOut FROM dates_special ;";
                MySqlDataReader Reader;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Reader.GetString("descripcion");
                    item.Value = Reader.GetInt32("Id");
                    specialsList.Add(item);
                }
                Reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

            return specialsList;
        }

        public static void GetSpecialItemsBySpecialId(int SpecialId, ListBox listBoxItems)
        {
            List<ItemBillboard> itemsBillboard = new List<ItemBillboard>();
            MySqlConnection connection = null;
            
            try
            {
                connection = new MySqlConnection(connectionString);
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT day_specials.item_id, day_specials.item_Price, it_titem.ITEM_Description  FROM day_specials INNER JOIN it_titem ON day_specials.item_id =it_titem.ITEM_ID WHERE day_specials.PR_ID = " + SpecialId + " ;";
                Console.WriteLine(command.CommandText);
                MySqlDataReader Reader;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    itemsBillboard.Add(new ItemBillboard(Reader.GetString("item_id"), Reader.GetString("ITEM_Description"),"","","", Reader.GetFloat("item_Price"),true));
                    //itemsBillboard.Add(Reader.GetString("item_id"));
                    //listBoxItems.Items.Add(Reader.GetString("item_id"));
                }
                listBoxItems.DataSource = itemsBillboard;
                Reader.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }


        public static List<ComboboxItem> GetItemClasses() {
            List<ComboboxItem> specialsList = new List<ComboboxItem>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Class_ID, Class_Name FROM it_titemclass;";
                MySqlDataReader Reader;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Reader.GetString("Class_Name");
                    item.Value = Reader.GetString("Class_ID");
                    specialsList.Add(item);
                }
                Reader.Close();
                connection.Close();
            }
            return specialsList;
        }

        public static List<ComboboxItem> GetItemCategories() {
            List<ComboboxItem> specialsList = new List<ComboboxItem>();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT Cate_ID, Cate_Name FROM it_tcategory;";
                MySqlDataReader Reader;
                connection.Open();
                Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Reader.GetString("Cate_Name");
                    item.Value = Reader.GetString("Cate_ID");
                    specialsList.Add(item);
                }
                Reader.Close();
                connection.Close();
            }
            return specialsList;
        }


        public static void SearchItem(DataGridView dataGridView1,string itemDescription)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                MySqlDataAdapter myDa = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT ITEM_ID, ITEM_Description, ITEM_Kitchen_Name, ITEM_Class_ID, ITEM_Cate_ID, Item_Sale_Price"+
                    " FROM it_titem "+
                    " WHERE ITEM_Description LIKE '%" + itemDescription + "%' "+
                    "AND ITEM_Inco_Account_ID = 1 AND Disc_item = 0 AND ITEM_Type_ID";
                sqlSelectAll += " ORDER BY Item_Sale_Price DESC ;";
                Console.WriteLine("---------------------------------------------------------------------+++++++");
                Console.WriteLine(sqlSelectAll);
                myDa.SelectCommand = new MySqlCommand(sqlSelectAll, connection);
                DataTable table = new DataTable();
                myDa.Fill(table);
                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;
                dataGridView1.DataSource = bSource;
            }
        }

    }
}