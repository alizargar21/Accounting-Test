using Accounting.App.Transaction;
using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.Utility.Convertor;
using Accounting.ViewModels.Customers;

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
            using(UnitOfWork db =  new UnitOfWork())
            {
            List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
                list.Add(new ListCustomerViewModel()
                {
                    CustomerID = 0,
                    FullName = "انتخاب کنید"
                });
                list.AddRange(db.CustomerRepositoy.GetNamesCustomers());
                cbCustomer.DataSource = list;
                cbCustomer.DisplayMember = "FullName";
                cbCustomer.ValueMember = "CustomerID";
            }



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
            using (UnitOfWork db = new UnitOfWork())
            {
                List<DataLayer.Accounting> resAccountingList = new List<DataLayer.Accounting>();
                DateTime? startDate;
                DateTime? endDate;
                if ((int)cbCustomer.SelectedValue != 0)
                {
                    int customerID = int.Parse(cbCustomer.SelectedValue.ToString());
                    resAccountingList.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID && a.CustomerID == customerID));
                }
                else
                {
                    resAccountingList.AddRange(db.AccountingRepository.Get(a => a.TypeID == TypeID));
                }
                if (txtFromDate.Text != "    /  /")
                {
                    startDate = Convert.ToDateTime(txtFromDate.Text);
                    startDate = DateConvertor.ToMiladi(startDate.Value);
                    resAccountingList = resAccountingList.Where(r =>r.Date >= startDate.Value).ToList();
                }
                if(txtToDate.Text != "    /  /")
                {
                    endDate = Convert.ToDateTime(txtToDate.Text);
                    endDate = DateConvertor.ToMiladi(endDate.Value);
                    resAccountingList = resAccountingList.Where(r => r.Date <= endDate.Value).ToList();

                }



              
               
          
              dgvReports.Rows.Clear();
                foreach(var item in resAccountingList) {
                   
                    string customerName = db.CustomerRepositoy.GetCustomerNameById(item.CustomerID);
                    dgvReports.Rows.Add( item.ID, customerName   , item.Amount , item.Description, item.Date.ToJalali() , item.CustomerID);

                    
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvReports.CurrentRow != null)
            {
                int ID = int.Parse(dgvReports.CurrentRow.Cells[0].Value.ToString());
                frmNewTransaction frmNewTransaction = new frmNewTransaction();
                frmNewTransaction.AccountID = ID;
                if(frmNewTransaction.ShowDialog() == DialogResult.OK)
                {
                    Filter();
                }
            }
        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
