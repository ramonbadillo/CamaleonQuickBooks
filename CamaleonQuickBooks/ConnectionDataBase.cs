using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

public class ConnectionDataBase
{
    public static string connectionString = "datasource = localhost; port = 3306; username = root; password = antonio; database= spicindian;";

    public static void GetItDetaMoveToDatagridview(DataGridView dataGridView1, DateTime startingDate, DateTime endDate)
    {
        MySqlConnection mysqlCon = new MySqlConnection(connectionString);
        mysqlCon.Open();
        MySqlDataAdapter myDa = new MySqlDataAdapter();
        string sqlSelectAll =
           "SELECT Move_ID, Move_Date, Move_Credit_Value, Move_Cash_Value, Move_Debit_Value, Move_Check_Value, Move_Stamp_Value, " +
           "(Move_Credit_Value+Move_Cash_Value+Move_Debit_Value+Move_Check_Value+Move_Stamp_Value) AS 'Total' " +
           "FROM it_tmove " +
            "WHERE DATE(Move_Date) BETWEEN '" + startingDate.ToString("yyyy-MM-dd") + "' AND '" +
            endDate.ToString("yyyy-MM-dd") + "' AND it_tmove.Move_Oper_ID = 2 ";
       
        sqlSelectAll += "ORDER BY it_tmove.Move_ID";
        Console.WriteLine("---------------------------------------------------------------------+++++++");
        Console.WriteLine(sqlSelectAll);
        myDa.SelectCommand = new MySqlCommand(sqlSelectAll, mysqlCon);
        DataTable table = new DataTable();
        myDa.Fill(table);
        BindingSource bSource = new BindingSource();
        bSource.DataSource = table;
        dataGridView1.DataSource = bSource;
    }
}