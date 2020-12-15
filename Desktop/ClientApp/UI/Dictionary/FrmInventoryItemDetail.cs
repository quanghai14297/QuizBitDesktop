using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmInventoryItemDetail : BaseForm.FrmBaseDetail
    {
        public FrmInventoryItemDetail()
        {
            InitializeComponent();
        }

        private BLInventoryItem _oBL;

        public BLInventoryItem objBLDetail
        {
            get
            {
                if (_oBL == null) _oBL = new BLInventoryItem();
                return _oBL;
            }
            set { _oBL = value; }
        }

        protected override void BindingData()
        {
            base.BindingData();
            txtInventoryItemCode.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItem.InventoryItemCodeColumn.ColumnName, true));
            txtInventoryItemName.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItem.InventoryItemNameColumn.ColumnName, true));
            cboInventoryItemCategory.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.InventoryItem.InventoryItemCategoryIDColumn.ColumnName, true));
            cboUnit.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.InventoryItem.UnitIDColumn.ColumnName, true));
            cboInventoryItemType.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.InventoryItem.InventoryItemTypeColumn.ColumnName, true));
            cboCourseType.DataBindings.Add(new Binding("Value", BsDetail, DsDictionary.InventoryItem.CourseTypeColumn.ColumnName, true));
            txtUnitPrice.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItem.UnitPriceColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", BsDetail, DsDictionary.InventoryItem.DescriptionColumn.ColumnName, true));
            imgFileResource.DataBindings.Add(new Binding("Image", BsDetail, DsDictionary.InventoryItem.FileResourceColumn.ColumnName, true));

            DictionaryDataSet.InventoryItemRow dr = (DictionaryDataSet.InventoryItemRow)CurrentRow;
            if (dr != null && !dr.IsFileResourceNull())
            {
                MemoryStream mem = new MemoryStream(dr.FileResource);
                imgFileResource.Image = Image.FromStream(mem);
            }
        }

        protected override void LoadDataForeignKey()
        {
            base.LoadDataForeignKey();
            var vlInventoryItemType = new ValueList();
            vlInventoryItemType.ValueListItems.Add(new ValueListItem((int)EnumInventoryItemType.Other, CommonFunction.GetDescription(EnumInventoryItemType.Other)));
            vlInventoryItemType.ValueListItems.Add(new ValueListItem((int)EnumInventoryItemType.Food, CommonFunction.GetDescription(EnumInventoryItemType.Food)));
            vlInventoryItemType.ValueListItems.Add(new ValueListItem((int)EnumInventoryItemType.Drink, CommonFunction.GetDescription(EnumInventoryItemType.Drink)));
            // drpInventoryItemType.DataSource = vlInventoryItemType;
            cboInventoryItemType.ValueList = vlInventoryItemType;

            var vlCourseType = new ValueList();
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Other, CommonFunction.GetDescription(EnumCourseType.Other)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Starter, CommonFunction.GetDescription(EnumCourseType.Starter)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.MainCourse, CommonFunction.GetDescription(EnumCourseType.MainCourse)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Desserts, CommonFunction.GetDescription(EnumCourseType.Desserts)));
            // drpCourseType.DataSource = vlCourseType;
            cboCourseType.ValueList = vlCourseType;
            cboCourseType.SelectedIndex = 0;

            bsUnit.DataSource = DsDictionary.Unit;
            bsInventoryItemCategory.DataSource = DsDictionary.InventoryItemCategory;
        }

        protected override bool ValidateForm()
        {
            if (!base.ValidateForm()) return false;
            if (string.IsNullOrEmpty(txtInventoryItemCode.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtInventoryItemCode.Tag));
                txtInventoryItemCode.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtInventoryItemName.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtInventoryItemName.Tag));
                txtInventoryItemName.Focus();
                return false;
            }
            return true;
        }

        protected override void InitNewRow()
        {
            base.InitNewRow();
            objBLDetail.InitNewRow(DsDictionary.InventoryItem);
            BsDetail.MoveFirst();
            txtInventoryItemCode.Focus();
        }

        protected override void InitCopyRow()
        {
            base.InitNewRow();
            DictionaryDataSet.InventoryItemRow drCurrent = (DictionaryDataSet.InventoryItemRow)((DataRowView)bsDetail.Current).Row;
            objBLDetail.InitCopyRow(DsDictionary.InventoryItem, drCurrent);
            BsDetail.MoveFirst();
            txtInventoryItemCode.Focus();
        }

        protected override int SaveData()
        {
            int result = 1;
            BsDetail.EndEdit();

            var tableChanged = dsDictionary.InventoryItem.GetChanges();
            if (tableChanged == null)
            {
                return (int)EnumResultInsertUpdate.Success;
            }
            else
            {
                if (tableChanged.Rows.Count == 0)
                    return (int)EnumResultInsertUpdate.Success;
            }
            DictionaryDataSet.InventoryItemRow drObjectChange = (DictionaryDataSet.InventoryItemRow)tableChanged.Rows[0];
            if (drObjectChange != null)
            {
                var oldID = drObjectChange.InventoryItemID;
                if (FormActionMode == ActionMode.Edit)
                {
                    drObjectChange.InventoryItemID = Guid.NewGuid();
                    drObjectChange.OldIDs += string.IsNullOrEmpty(drObjectChange.OldIDs) ? "" : ";" + oldID;
                }
                    
                result = objBLDetail.InsertUpdate(drObjectChange, oldID);
            }
            return result;
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.ControlTextEditor_Validate(sender);
        }

        private void Control_ItemNotInList(object sender, Infragistics.Win.UltraWinEditors.ValidationErrorEventArgs e)
        {
            base.ControlCombo_ItemNotInList(sender, e);
        }

        private void TxtInventoryItemName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //txtInventoryItemNameNonUnicode.Text = CommonFunction.RemoveSignVietnameseString(txtInventoryItemName.Text);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            try
            {
                // image filters  
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // display image in picture box  
                    imgFileResource.Image = new Bitmap(openFileDialog.FileName);

                    try
                    {
                        using (Image img = Image.FromFile(openFileDialog.FileName))
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            ms.Close();
                            ((DictionaryDataSet.InventoryItemRow)CurrentRow).FileResource = ms.ToArray();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBoxCommon.ShowException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
