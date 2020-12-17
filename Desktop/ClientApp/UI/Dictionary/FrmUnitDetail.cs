using Desktop.BL;
using Desktop.Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmUnitDetail : BaseForm.FrmBaseDetail
    {
        public FrmUnitDetail()
        {
            InitializeComponent();
        }

        private BLUnit _oBL;

        public BLUnit objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLUnit();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtUnitName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Unit.UnitNameColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Unit.DescriptionColumn.ColumnName, true));
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtUnitName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtUnitName.Tag));
                txtUnitName.Focus();
                return false;
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.Unit);
            BsDetail.MoveFirst();
            txtUnitName.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.UnitRow drCurrent = (DictionaryDataSet.UnitRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.Unit, drCurrent);
            BsDetail.MoveFirst();
            txtUnitName.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.Unit.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.UnitRow drObjectChange = (DictionaryDataSet.UnitRow)tableChanged.Rows[0];
            if (drObjectChange != null)
            {

                var oldID = drObjectChange.UnitID;
                if (FormActionMode == ActionMode.AddNew || FormActionMode == ActionMode.Copy)
                {
                    drObjectChange.UnitID = Guid.NewGuid();
                   
                }

                result = objBLDetail.InsertUpdate(drObjectChange, oldID);
            }
            return result;
        }

        protected override bool CheckCodeIsExists()
        {
            return true;
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.ControlTextEditor_Validate(sender);
        }
    }
}
