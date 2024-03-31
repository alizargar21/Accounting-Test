using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.Utility.Convertor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting.App.Reports
{
    public partial class frmReports : Form
    {
        public int TypeID = 0;
        public frmReports()
        {
            InitializeComponent();
        }

        private void frmReports_Load(object sender, EventArgs e)
        {
            if(TypeID == 1) {
                this.Text = "گزارش پرداخت ها";
            } else
            {
                this.Text = "گزارش دریافت ها";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void dgvReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        void Filter ()
        {
            using(UnitOfWork db = new UnitOfWork())
            {
                var res = db.AccountingRepository.Get(a => a.TypeID == TypeID);
              //  dgvReports.AutoGenerateColumns = false;
              //  dgvReports.DataSource = res;
              dgvReports.Rows.Clear();
                foreach(var item in res) {
                    string customerName = db.CustomerRepositoy.GetCustomerNameById(item.CustomerID);
                    dgvReports.Rows.Add( item.ID, customerName   , item.Amount , item.Description, item.Date.ToJalali());

                    
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvReports.CurrentRow != null )
            {
                int ID = int.Parse(dgvReports.CurrentRow.Cells[0].Value.ToString());
                if(RtlMessageBox.Show("آیا از حذف مطمن هستید","هشدار" ,MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using(UnitOfWork db = new UnitOfWork())
                    {
                        db.AccountingRepository.Delete(ID);
                        db.Save();
                        Filter();
                 
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }
    }
}
