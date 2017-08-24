using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamaleonQuickBooks
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void createCVSButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "CSV|*.csv", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void openCompanyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCompany openCompany = new OpenCompany();
            openCompany.ShowDialog();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            double diferenceDateTime = endDateTimePicker.Value.Subtract(startDateTimePicker.Value).TotalDays;
            if (diferenceDateTime > 0)
            {
                ConnectionDataBase.GetItDetaMoveToDatagridview(mainDataGridView, startDateTimePicker.Value, endDateTimePicker.Value);
                //totalRowsLabel.Text = "" + mainDataGridView.RowCount;
            }
            else
            {
                MessageBox.Show("The start date can not be greater than the end date.", "Error",
                       MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }
    }
}