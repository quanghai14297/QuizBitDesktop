using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmInventoryItem : BaseForm.FrmBaseList
    {
        public FrmInventoryItem()
        {
            InitializeComponent();
        }

        private BLInventoryItem oBL;

        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            SetVisibleTabInfo(false);
            var table = oBL.Get();
            if (table != null && table.Rows.Count > 0)
            {
                dsDictionary.InventoryItem.Clear();
                dsDictionary.Merge(table);
                dsDictionary.AcceptChanges();
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
            grdList.DisplayLayout.Bands[0].Columns[ColumnName.InventoryItemType].ValueList = vlInventoryItemType;

            var vlCourseType = new ValueList();
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Other, CommonFunction.GetDescription(EnumCourseType.Other)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Starter, CommonFunction.GetDescription(EnumCourseType.Starter)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.MainCourse, CommonFunction.GetDescription(EnumCourseType.MainCourse)));
            vlCourseType.ValueListItems.Add(new ValueListItem((int)EnumCourseType.Desserts, CommonFunction.GetDescription(EnumCourseType.Desserts)));
            // drpCourseType.DataSource = vlCourseType;
            grdList.DisplayLayout.Bands[0].Columns[ColumnName.CourseType].ValueList = vlCourseType;

            dsDictionary.Unit.Clear();
            dsDictionary.Unit.Merge(new BLUnit().Get());
            dsDictionary.InventoryItemCategory.Clear();
            dsDictionary.InventoryItemCategory.Merge(new BLInventoryItemCategory().Get());
        }

        protected override void InitBusinessObject()
        {
            base.InitBusinessObject();
            oBL = new BLInventoryItem();
        }

        protected override void ShowFormDetail(ActionMode actionMode)
        {
            base.ShowFormDetail(actionMode);
            using (var fDetail = new FrmInventoryItemDetail())
            {
                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsList;
                fDetail.objBLDetail = oBL;
                fDetail.FormActionMode = actionMode;
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.InventoryItem.RejectChanges();
                else dsDictionary.InventoryItem.AcceptChanges();
                ActiveAndSelectRow();
            }
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
                    if (Guid.TryParse(row.Cells[ColumnName.InventoryItemID].Value.ToString(), out id))
                    {
                        if (id == Guid.Empty) continue;
                        int result = oBL.Delete(id);
                        if (result == (int)EnumResultDelete.Success)
                            rowsDeleted.Add(id);
                        else if (result == (int)EnumResultDelete.ItemWasUsed)
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_HadReference, row.Cells[ColumnName.InventoryItemName].Text));
                            break;
                        }
                        else
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_Error, row.Cells[ColumnName.InventoryItemName].Text));
                            break;
                        }
                    }
                }

                foreach (var item in rowsDeleted)
                {
                    bsList.RemoveAt(bsList.Find(ColumnName.InventoryItemID, item));
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
