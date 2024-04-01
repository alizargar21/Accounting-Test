using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accounting.App.Transaction
{
    public partial class frmNewTransaction : Form
    {

        private UnitOfWork db;

        public int AccountID = 0;
        public frmNewTransaction()
        {
            InitializeComponent();
        }

        private void frmNewTransaction_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepositoy.GetNamesCustomers();

            if (AccountID != 0)
            {
                var account = db.AccountingRepository.GetById(AccountID);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description.ToString();
                txtName.Text = db.CustomerRepositoy.GetCustomerNameById(account.CustomerID);




                if (account.TypeID == 1)
                {
                    rbRecived.Checked = true;
                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
                db.Dispose();

            }


        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers.AutoGenerateColumns = false;
            dgvCustomers.DataSource = db.CustomerRepositoy.GetNamesCustomers(txtFilter.Text);
        }

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (BaseValidator.IsFormValid(this.components))
                {
                    using (UnitOfWork db = new UnitOfWork())
                        if (rbPay.Checked || rbRecived.Checked)
                        {
                            DataLayer.Accounting accounting = new DataLayer.Accounting()
                            {
                                Amount = int.Parse(txtAmount.Value.ToString()),
                                Description = txtDescription.Text.ToString(),
                                CustomerID = db.CustomerRepositoy.GetCustomerIdByName(txtName.Text),
                                TypeID = (rbRecived.Checked ? 1 : 2),
                                Date = DateTime.Now

                            };


                            if (AccountID == 0)
                            {
                                db.AccountingRepository.Insert(accounting);
                                db.Save();
                            }
                            else
                            {
                                accounting.ID = AccountID;
                                db.AccountingRepository.Update(accounting);
                                db.Save();

                            }

                            DialogResult = DialogResult.OK;

                        }
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
                }
            }
        }
    }
}
