using Desktop.BL;
using Desktop.Entity;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmAreaDetail : BaseForm.FrmBaseDetail
    {
        public FrmAreaDetail()
        {
            InitializeComponent();
        }

        private BLArea _oBL;

        public BLArea objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLArea();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtAreaName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Area.AreaNameColumn.ColumnName, true));
            txtNumberOfTable.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Area.NumberOfTableColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.Area.DescriptionColumn.ColumnName, true));
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtAreaName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtAreaName.Tag));
                txtAreaName.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNumberOfTable.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtNumberOfTable.Tag));
                txtNumberOfTable.Focus();
                return false;
            }
            else
            {
                int table = 0;
                if (int.TryParse(txtNumberOfTable.Value.ToString(), out table))
                {
                    if (table <= 0)
                    {
                        MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control_More, txtNumberOfTable.Tag, "0"));
                        txtNumberOfTable.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.Area);
           
            BsDetail.MoveFirst();
            txtAreaName.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.AreaRow drCurrent = (DictionaryDataSet.AreaRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.Area, drCurrent);
            BsDetail.MoveFirst();
            txtAreaName.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.Area.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.AreaRow drObjectChange = (DictionaryDataSet.AreaRow)tableChanged.Rows[0];
            if (drObjectChange != null)
            {
                var oldID = drObjectChange.AreaID;
                if (FormActionMode == ActionMode.AddNew || FormActionMode == ActionMode.Copy)
                {
                    drObjectChange.AreaID = Guid.NewGuid();
                   
                }
                result = objBLDetail.InsertUpdate(drObjectChange, oldID);
            }
            return result;
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.ControlTextEditor_Validate(sender);
        }
    }
}
