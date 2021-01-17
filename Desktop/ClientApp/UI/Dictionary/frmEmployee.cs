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
        private BLUser oBLUser;
        private BLRole oBLRole;
        protected override void LoadDataForm()
        {
            base.LoadDataForm();
            SetVisibleTabInfo(false);

            var table = oBL.Get();

            tbrFunction.Tools["mnuCopy"].SharedProps.Visible = false;
            if (table != null && table.Rows.Count > 0)
            {
                dsDictionary.Employee.Clear();
                foreach (DataRow dr in table.Rows) // search whole table
                {
                    if (dr["Gender"].ToString() == "0") // if id==2
                    {
                        dr["GenderDisplay"] = "Nam";
                    }
                    else
                    {
                        dr["GenderDisplay"] = "Nữ";
                    }
                    if (dr["RoleID"].ToString() == "0") // if id==2
                    {
                        dr["JobStatusDisplay"] = "Quản lý";
                    }
                    else if (dr["RoleID"].ToString() == "2") // if id==2
                    {
                        dr["JobStatusDisplay"] = "Nhân viên phục vụ / Order";
                    }
                    else if (dr["RoleID"].ToString() == "3")
                    {
                        dr["JobStatusDisplay"] = "Nhân viên lễ tân";
                    }
                    else if (dr["RoleID"].ToString() == "5")
                    {
                        dr["JobStatusDisplay"] = "Nhân viên bếp";
                    }
                }
                dsDictionary.Merge(table);

                dsDictionary.AcceptChanges();
            }
        }

        protected override void InitBusinessObject()
        {
            base.InitBusinessObject();
            oBL = new BLEmployee();
            oBLUser = new BLUser();
            oBLRole = new BLRole();
        }

        protected override void ShowFormDetail(ActionMode actionMode)
        {
            base.ShowFormDetail(actionMode);
            string UserID = null;
            if (actionMode != ActionMode.AddNew)
            {
                UserID = ((Desktop.Entity.DictionaryDataSet.EmployeeRow)((System.Data.DataRowView)bsList.Current).Row).UserID.ToString();
            }

            var tableUser = oBLUser.Get();
            var tableRole = oBLRole.Get();
            DictionaryDataSet.UserDataTable dt = new DictionaryDataSet.UserDataTable();
            DictionaryDataSet.UserJoinRoleDataTable dtRole = new DictionaryDataSet.UserJoinRoleDataTable();
            if (tableUser.Select("UserID = '" + UserID + "'").Count() > 0)
            {
                dt.ImportRow(tableUser.Select("UserID = '" + UserID + "'")[0]);
                dsDictionary.User.Clear();
                dsDictionary.Merge(dt);
            }
            DataRow[] dr = tableRole.Select("UserID = '" + UserID + "'");
            if (dr.Count() > 0)
            {
                for (int i = 0; i < dr.Count(); i++)
                {
                    dtRole.ImportRow(dr[i]);
                }
                dsDictionary.Role.Clear();
                dsDictionary.Merge(dtRole);
            }
            using (var fDetail = new FrmEmployeeDetail())
            {

                fDetail.DsDictionary = dsDictionary;
                fDetail.BsDetail = bsList;
                fDetail.objBLDetail = oBL;
                fDetail.FormActionMode = actionMode;
                if (fDetail.ShowDialog() != DialogResult.OK) dsDictionary.Employee.RejectChanges();
                else
                {
                    dsDictionary.Customer.AcceptChanges();
                    LoadDataForm();
                }
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
                    bsList.RemoveAt(bsList.Find(ColumnName.EmployeeID, item));
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
