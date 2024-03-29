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
using Accounting.DataLayer;
using System.IO;

namespace Accounting.App
{
    public partial class frmAddOrEditCustomers : Form
    {
        public int customerId = 0;


        UnitOfWork db = new UnitOfWork();

        public frmAddOrEditCustomers()
        {
            InitializeComponent();
        }

        private void frmAddOrEditCustomers_Load(object sender, EventArgs e)
        {
            if(customerId != 0)
            {
                this.Text = "ویرایش شخص";
                btnSave.Text = "ویرایش";

                var customer = db.CustomerRepositoy.GetCustomerById(customerId);
                txtAddress.Text = customer.Address;
                txtEmail.Text = customer.Email;
                txtMobile.Text = customer.Mobile;
                txtName.Text = customer.FullName;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images" + customer.CustomerImage;
            }
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {

                pcCustomer.ImageLocation = ofd.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";

                if (!Directory.Exists(path)) {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path + imageName);

                Customers customer = new Customers()

                {
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    CustomerImage = imageName
                };
                if (customerId == 0)
                {
                db.CustomerRepositoy.InsertCustomer(customer);

                } else
                {
                    customer.CustomerID = customerId;
                    db.CustomerRepositoy.UpdateCustomer(customer);
                }

                db.Save();
                DialogResult = DialogResult.OK;
            }
        }


    }
}
