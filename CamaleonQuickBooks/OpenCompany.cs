using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CamaleonQuickBooks.Properties;


    public partial class OpenCompany : Form
    {
        public OpenCompany()
        {
            InitializeComponent();
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            Configuration.GetDataBases(databaseComboBox, serverTextBox.Text, portTextBox.Text, userTextBox.Text, PassTextBox.Text);
            databaseComboBox.Enabled = true;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            Settings.Default.Server = serverTextBox.Text;
            Settings.Default.Port = portTextBox.Text;
            Settings.Default.User = userTextBox.Text;
            Settings.Default.Password = PassTextBox.Text;
            Settings.Default.Database = databaseComboBox.Text;
            Settings.Default.Save();
            MessageBox.Show("Configuration Saved.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            this.Dispose();
        }

        private void OpenCompany_Load(object sender, EventArgs e)
        {
            serverTextBox.Text = Settings.Default.Server;
            portTextBox.Text=Settings.Default.Port;
             userTextBox.Text=Settings.Default.User;
             PassTextBox.Text=Settings.Default.Password ;
              databaseComboBox.Text=Settings.Default.Database;
        }
    }

