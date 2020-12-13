using Desktop.BL;
using Desktop.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmCompanyInfo : Form
    {
        public FrmCompanyInfo()
        {
            InitializeComponent();
        }

        private void FrmCompanyInfo_Load(object sender, EventArgs e)
        {
            try
            {
                var row = Session.CompanyInfo;
                txtName.Text = row.Name;
                txtEmail.Text = row.Email;
                txtHotline.Text = row.Hotline;
                txtWebsite.Text = row.Website;
                txtAddress.Text = row.Address;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    var table = new DictionaryDataSet.SystemInfoDataTable();
                    var row = table.NewSystemInfoRow();
                    row.Name = txtName.Text.Trim();
                    row.Email = txtEmail.Text.Trim();
                    row.Hotline = txtHotline.Text.Trim();
                    row.Website = txtWebsite.Text.Trim();
                    row.Address = txtAddress.Text.Trim();
                    Session.CompanyInfo = row;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtName.Tag));
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtEmail.Tag));
                txtEmail.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtHotline.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtHotline.Tag));
                txtHotline.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtWebsite.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtWebsite.Tag));
                txtWebsite.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtAddress.Tag));
                txtAddress.Focus();
                return false;
            }
            return true;
        }
    }
}
