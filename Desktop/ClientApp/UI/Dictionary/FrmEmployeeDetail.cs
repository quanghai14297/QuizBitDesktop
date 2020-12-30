using Desktop.BL;
using Desktop.Entity;
using System.Data;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmEmployeeDetail : BaseForm.FrmBaseDetail
    {
        public FrmEmployeeDetail()
        {
            InitializeComponent();
        }

        private BLEmployee _oBL;

        public BLEmployee objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLEmployee();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtEmployeeCode.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.EmployeeCodeColumn.ColumnName, true));
            txtEmployeeName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.EmployeeNameColumn.ColumnName, true));
            dteBirthday.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.Employee.BirthdayColumn.ColumnName, true));
            txtEmail.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.EmailColumn.ColumnName, true));
            txtMobile.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.MobileColumn.ColumnName, true));
            txtAddress.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.AddressColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Employee.DescriptionColumn.ColumnName, true));
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtEmployeeCode.Tag));
                txtEmployeeCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtEmployeeName.Tag));
                txtEmployeeName.Focus();
                return false;
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.Employee);
            BsDetail.MoveFirst();
            txtEmployeeCode.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.EmployeeRow drCurrent = (DictionaryDataSet.EmployeeRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.Employee, drCurrent);
            BsDetail.MoveFirst();
            txtEmployeeCode.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.Employee.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.EmployeeRow drObjectChange = (DictionaryDataSet.EmployeeRow)tableChanged.Rows[0];
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
