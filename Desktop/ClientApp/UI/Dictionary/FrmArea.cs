using Desktop.BL;
using Desktop.Entity;
using Infragistics.Win;
using Infragistics.Win.UltraWinListView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmArea : BaseForm.FrmBaseList
    {
        public FrmArea()
        {
            InitializeComponent();
        }

        private BLArea oBL;

        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            //SetVisibleTabInfo(false);
            var table = oBL.Get();
            if (table != null && table.Rows.Count > 0)
            {
                dsDictionary.Area.Clear();
                dsDictionary.Merge(table);
                dsDictionary.AcceptChanges();
            }

            lvTest.DrawFilter = new NoFocusRectDrawFilter();
        }

        protected override void InitBusinessObject()
        {
            base.InitBusinessObject();
            oBL = new BLArea();
        }

        protected override void ShowFormDetail(ActionMode actionMode)
        {
            if (DateTime.Now.TimeOfDay > Session.StartDate.TimeOfDay && DateTime.Now.TimeOfDay < Session.EndDate.TimeOfDay)
            {
                MessageBoxCommon.ShowExclamation("Trong giờ làm việc không được thay đổi thiết lập khu vực");
                if (actionMode == ActionMode.AddNew) return;
                actionMode = ActionMode.View;
            }
            base.ShowFormDetail(actionMode);
            using (var fDetail = new FrmAreaDetail())
            {
                fDetail.FormActionMode = actionMode;
                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsList;
                fDetail.objBLDetail = oBL;
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.Area.RejectChanges();
                else dsDictionary.Area.AcceptChanges();
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
                    if (Guid.TryParse(row.Cells[ColumnName.AreaID].Value.ToString(), out id))
                    {
                        if (id == Guid.Empty) continue;
                        int result = oBL.Delete(id);
                        if (result == (int)EnumResultDelete.Success)
                            rowsDeleted.Add(id);
                        else if (result == (int)EnumResultDelete.ItemWasUsed)
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_HadReference, row.Cells[ColumnName.AreaName].Text));
                            break;
                        }
                        else
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_Error, row.Cells[ColumnName.AreaName].Text));
                            break;
                        }
                    }
                }

                foreach (var item in rowsDeleted)
                {
                    bsList.RemoveAt(bsList.Find(ColumnName.AreaID, item));
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

        protected override void GetDetailByID()
        {
            base.GetDetailByID();
            if (CurrentRow != null)
            {
                DictionaryDataSet.AreaRow dr = (DictionaryDataSet.AreaRow)CurrentRow;
                if (dr.NumberOfTable > 0)
                {
                    lvTest.Items.Clear();
                    var dt = oBL.GetTableMappingByAreaID(dr.AreaID, DateTime.Now);
                    foreach (DataRow item in dt.Rows)
                    {
                        var name = item[ColumnName.TableName];
                        //UltraListViewSubItem subItem;
                        //Color foreColor = Color.Black;
                        //switch (item[ColumnName.Inactive].ToString())
                        //{
                        //    case "1":
                        //        subItem = new UltraListViewSubItem("Ngừng sử dụng", null);
                        //        foreColor = Color.Red;
                        //        break;
                        //    default:
                        //        subItem = new UltraListViewSubItem("Có thể sử dụng", null);
                        //        break;
                        //}
                        //UltraListViewItem lvItem = new UltraListViewItem(name, new UltraListViewSubItem[] { subItem }, null);
                        //if (foreColor == Color.Red)
                        //    lvItem.Appearance.ForeColor = Color.Red;

                        UltraListViewItem lvItem = new UltraListViewItem(name, new UltraListViewSubItem[] { }, null);
                        lvTest.Items.Add(lvItem);
                    }
                }
            }
        }
    }
}
