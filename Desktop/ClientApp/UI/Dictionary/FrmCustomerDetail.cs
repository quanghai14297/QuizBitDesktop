using Desktop.BL;
using Desktop.Entity;
using System.Data;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmCustomerDetail : BaseForm.FrmBaseDetail
    {
        public FrmCustomerDetail()
        {
            InitializeComponent();
        }

        private BLCustomer _oBL;

        public BLCustomer objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLCustomer();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtCustomerCode.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.CustomerCodeColumn.ColumnName, true));
            txtCustomerName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.CustomerNameColumn.ColumnName, true));
            dteBirthday.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Customer.BirthdayColumn.ColumnName, true));
            txtEmail.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.EmailColumn.ColumnName, true));
            txtMobile.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.MobileColumn.ColumnName, true));
            txtAddress.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.AddressColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Customer.DescriptionColumn.ColumnName, true));
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtCustomerCode.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtCustomerCode.Tag));
                txtCustomerCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtCustomerName.Tag));
                txtCustomerName.Focus();
                return false;
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.Customer);
            BsDetail.MoveFirst();
            txtCustomerCode.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.CustomerRow drCurrent = (DictionaryDataSet.CustomerRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.Customer, drCurrent);
            BsDetail.MoveFirst();
            txtCustomerCode.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.Customer.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.CustomerRow drObjectChange = (DictionaryDataSet.CustomerRow)tableChanged.Rows[0];
            if (drObjectChange != null)
            {
                result = objBLDetail.InsertUpdate(drObjectChange);
            }
            return result;
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.ControlTextEditor_Validate(sender);
        }
    }
}
