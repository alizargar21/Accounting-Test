using Accounting.App.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accounting.App.Reports;

namespace Accounting.App
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            frmCustomers frmCustomers = new frmCustomers();
            frmCustomers.ShowDialog();
        }

        private void btnNewTransaction_Click(object sender, EventArgs e)
        {
            frmNewTransaction frmNewTransaction = new frmNewTransaction();
            frmNewTransaction.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            frmReports frmReports = new frmReports();
            frmReports.TypeID = 2;
            frmReports.ShowDialog();
        }

        private void btnReportRecived_Click(object sender, EventArgs e)
        {
            frmReports frmReports = new frmReports();
            frmReports.TypeID = 1;
            frmReports.ShowDialog();
        }
    }
}
