using Desktop.BL;
using Desktop.Entity;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmInventoryItemCategory : BaseForm.FrmBaseList
    {
        public FrmInventoryItemCategory()
        {
            InitializeComponent();
        }
        private BLInventoryItemCategory oBL = new BLInventoryItemCategory();

        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            SetVisibleTabInfo(false);
            var table = oBL.Get();
            if (table != null && table.Rows.Count > 0)
            {
                dsDictionary.InventoryItemCategory.Clear();
                dsDictionary.Merge(table);
                dsDictionary.AcceptChanges();
            }
        }

        protected override void InitBusinessObject()
        {
            base.InitBusinessObject();
            oBL = new BLInventoryItemCategory();
        }

        protected override void ShowFormDetail(ActionMode actionMode)
        {
            base.ShowFormDetail(actionMode);
            using (var fDetail = new FrmInventoryItemCategoryDetail())
            {
               
                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsList;
                fDetail.objBLDetail = oBL;
                fDetail.FormActionMode = actionMode;
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.InventoryItemCategory.RejectChanges();
                else dsDictionary.InventoryItemCategory.AcceptChanges();
                ActiveAndSelectRow();
            }
        }

        protected bool CheckBeforeDelete(Guid id)
        {
            return oBL.CheckBeforeDelete(id);
        }

        protected override void Delete()
        {
            base.Delete();
            if (grdList.Selected.Rows.Count == 0) return;
            if (MessageBoxCommon.ShowYesNoQuestion(Properties.Resources.Message_Question_Delete) == DialogResult.Yes)
            {
                var rowsDeleted = new List<Guid>();
                var positionGrid = grdList.Selected.Rows[0].Index;
                foreach (var row in grdList.Selected.Rows)
                {
                    positionGrid = positionGrid > row.Index ? row.Index : positionGrid;
                    Guid id = Guid.Empty;
                    if (Guid.TryParse(row.Cells[ColumnName.InventoryItemCategoryID].Value.ToString(), out id))
                    {
                        if (id == Guid.Empty) continue;
                        int result = oBL.Delete(id);
                        if (result == (int)EnumResultDelete.Success)
                            rowsDeleted.Add(id);
                        else if (result == (int)EnumResultDelete.ItemWasUsed)
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_HadReference, row.Cells[ColumnName.InventoryItemCategoryName].Text));
                            break;
                        }
                        else
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_Error, row.Cells[ColumnName.InventoryItemCategoryName].Text));
                            break;
                        }
                    }
                }

                foreach (var item in rowsDeleted)
                {
                    bsList.RemoveAt(bsList.Find(ColumnName.InventoryItemCategoryID, item));
                }

                if (rowsDeleted.Count > 0)
                {
                    if (bsList.Count == 0)
                    {
                        ShowHideFunctionByData();
                        return;
                    }
                    else if (positionGrid == 0 && bsList.Count > 0)
                        bsList.MoveFirst();
                    else
                        positionGrid -= 1;

                    grdList.Rows[positionGrid].Activated = true;
                    grdList.Rows[positionGrid].Selected = true;
                }
            }
        }
    }
}
