using Desktop.BL;
using Desktop.Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmInventoryItemCategoryDetail : BaseForm.FrmBaseDetail
    {
        public FrmInventoryItemCategoryDetail()
        {
            InitializeComponent();
        }

        private BLInventoryItemCategory _oBL;

        public BLInventoryItemCategory objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLInventoryItemCategory();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtInventoryItemCategoryCode.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItemCategory.InventoryItemCategoryCodeColumn.ColumnName, true));
            txtInventoryItemCategoryName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItemCategory.InventoryItemCategoryNameColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItemCategory.DescriptionColumn.ColumnName, true));
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtInventoryItemCategoryCode.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtInventoryItemCategoryCode.Tag));
                txtInventoryItemCategoryCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtInventoryItemCategoryName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtInventoryItemCategoryName.Tag));
                txtInventoryItemCategoryName.Focus();
                return false;
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.InventoryItemCategory);
            BsDetail.MoveFirst();
            txtInventoryItemCategoryCode.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.InventoryItemCategoryRow drCurrent = (DictionaryDataSet.InventoryItemCategoryRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.InventoryItemCategory, drCurrent);
            BsDetail.MoveFirst();
            txtInventoryItemCategoryCode.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.InventoryItemCategory.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.InventoryItemCategoryRow drObjectChange = (DictionaryDataSet.InventoryItemCategoryRow)tableChanged.Rows[0];
            if (drObjectChange != null)
            {
                var oldID = drObjectChange.InventoryItemCategoryID;
                if (FormActionMode == ActionMode.AddNew || FormActionMode == ActionMode.Copy)
                {
                    drObjectChange.InventoryItemCategoryID = Guid.NewGuid();
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
