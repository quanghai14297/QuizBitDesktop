using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.BL;
using Desktop.Entity;

namespace ClientApp.UI.Dictionary
{
    public partial class frmEmployee : BaseForm.FrmBaseList
    {
        public frmEmployee()
        {
            InitializeComponent();
        }
        private BLEmployee oBL;

        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            SetVisibleTabInfo(false);
            var table = oBL.Get();
            if (table != null && table.Rows.Count > 0)
            {
                dsDictionary.Employee.Clear();
                dsDictionary.Merge(table);
                dsDictionary.AcceptChanges();
            }
        }

        protected override void InitBusinessObject()
        {
            base.InitBusinessObject();
            oBL = new BLEmployee();
        }

        protected override void ShowFormDetail(ActionMode actionMode)
        {
            base.ShowFormDetail(actionMode);
            using (var fDetail = new FrmEmployeeDetail())
            {

                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsList;
                fDetail.objBLDetail = oBL;
                fDetail.FormActionMode = actionMode;
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.Employee.RejectChanges();
                else dsDictionary.Customer.AcceptChanges();
            }
            ActiveAndSelectRow();
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
                    if (Guid.TryParse(row.Cells[ColumnName.EmployeeID].Value.ToString(), out id))
                    {
                        if (id == Guid.Empty) continue;
                        int result = oBL.Delete(id);
                        if (result == (int)EnumResultDelete.Success)
                            rowsDeleted.Add(id);
                        else if (result == (int)EnumResultDelete.ItemWasUsed)
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_HadReference, row.Cells[ColumnName.EmployeeName].Text));
                            break;
                        }
                        else
                        {
                            MessageBoxCommon.ShowMessageError(String.Format(Properties.Resources.Message_DeleteData_Error, row.Cells[ColumnName.EmployeeName].Text));
                            break;
                        }
                    }
                }

                foreach (var item in rowsDeleted)
                {
                    bsList.RemoveAt(bsList.Find(ColumnName.CustomerID, item));
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
