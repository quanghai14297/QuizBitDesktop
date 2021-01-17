using Desktop.BL;
using Desktop.Entity;
using System;
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
        BindingSource bsUser = new BindingSource();
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
            txtEmployeeCode.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.EmployeeCodeColumn.ColumnName, true));
            txtEmployeeName.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.EmployeeNameColumn.ColumnName, true));
            dteBirthday.DataBindings.Add(new Binding("Value", bsDetail, DsDictionary.Employee.BirthdayColumn.ColumnName, true));
            txtEmail.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.EmailColumn.ColumnName, true));
            txtMobile.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.MobileColumn.ColumnName, true));
            txtAddress.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.AddressColumn.ColumnName, true));
            txtDescription.DataBindings.Add(new Binding("Text", bsDetail, DsDictionary.Employee.DescriptionColumn.ColumnName, true));
            if (FormActionMode == ActionMode.AddNew)
            {
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
            }
            else
            {
                txtUserName.Text = DsDictionary.User.Rows.Count > 0 ? ((Desktop.Entity.DictionaryDataSet.UserRow)DsDictionary.User.Rows[0]).UserName : "";
                txtPassword.Text = DsDictionary.User.Rows.Count > 0 ? ((Desktop.Entity.DictionaryDataSet.UserRow)DsDictionary.User.Rows[0]).Password : "";
                txtCofirmPassword.Text = txtPassword.Text;
            }

            if (DsDictionary.UserJoinRole.Rows.Count > 0 )
            {
                if((((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "Admin" || ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "QL"))
                {
                    rbAdmin.Checked = true;
                }
                else if ((((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "Chạy bàn" || ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "CB"))
                {
                    rbPV.Checked = true;
                }
                else if ((((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "Lễ tân" || ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "LT"))
                {
                    rbLeTan.Checked = true;
                }
                else if ((((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "Nhân viên bếp" || ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)DsDictionary.UserJoinRole.Rows[0]).RoleCode == "BEP"))
                {
                    rbBep.Checked = true;
                }
            }
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
            int resultAccount = 0;
            bool checkAccount = false;
            BsDetail.EndEdit();
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtCofirmPassword.Text))
            {
                if (txtPassword.Text != txtCofirmPassword.Text)
                {
                    MessageBoxCommon.ShowExclamation("Mật khẩu xác nhận không khớp. Vui lòng kiểm tra lại.");
                    return 0;
                }
                checkAccount = true;
            }
            //var tableChanged = dsDictionary.Employee.GetChanges();
            //if (tableChanged == null)
            //{
            //    return (int)EnumResultInsertUpdate.Success;
            //}
            //else
            //{
            //    if (tableChanged.Rows.Count == 0)
            //        return (int)EnumResultInsertUpdate.Success;
            //}
            System.Data.DataRow drObjectChange = ((System.Data.DataRowView)bsDetail.Current).Row;
            if (drObjectChange != null)
            {
                ((Desktop.Entity.DictionaryDataSet.EmployeeRow)drObjectChange).IdentifyNumberIssuedDate = DateTime.Now;
                result = objBLDetail.InsertUpdate(drObjectChange);
               
                
                if (result == 1 && checkAccount)
                {
                    DictionaryDataSet.UserDataTable table = new DictionaryDataSet.UserDataTable();
                    Guid userID = Guid.Empty;
                    Guid UserJoinRoleID = Guid.NewGuid();
                    if (FormActionMode == ActionMode.AddNew)
                    {
                        DictionaryDataSet.UserRow drUser  = table.NewUserRow();
                        drUser.UserID = Guid.NewGuid();
                        drUser.UserName = txtUserName.Text;
                        drUser.Password = EncryptUtil.MD5Hash(txtPassword.Text);
                        drUser.DisplayName = txtEmployeeName.Text;
                        drUser.ChangedPasswordTime = DateTime.Now;
                        drUser.Inactive = false;
                        drUser.EmployeeID = ((Desktop.Entity.DictionaryDataSet.EmployeeRow)drObjectChange).EmployeeID;
                        resultAccount = objBLDetail.InsertUpdateUser(drUser);
                        userID = drUser.UserID;
                        UserJoinRoleID = drUser.UserJoinRoleID;
                    }

                    else
                    {
                       DataRow drUser = dsDictionary.User.FindByUserID(userID);
                        resultAccount = objBLDetail.InsertUpdateUser(drUser);
                        userID = ((Desktop.Entity.DictionaryDataSet.UserRow)drUser).UserID;
                        UserJoinRoleID = ((Desktop.Entity.DictionaryDataSet.UserRow)drUser).UserJoinRoleID ;
                    }

                    
                    if (resultAccount==1)
                    {
                        DictionaryDataSet.UserJoinRoleDataTable tableRole = new DictionaryDataSet.UserJoinRoleDataTable();
                        if (FormActionMode == ActionMode.AddNew)
                        {
                            DictionaryDataSet.UserJoinRoleRow drRole = tableRole.NewUserJoinRoleRow();
                            drRole.UserID = userID;
                            drRole.UserJoinRoleID = Guid.NewGuid();
                            if (rbAdmin.Checked)
                            {
                                drRole.RoleID = 0;
                            }
                            else if (rbBep.Checked)
                            {
                                drRole.RoleID = 5;

                            }
                            else if (rbLeTan.Checked)
                            {
                                drRole.RoleID = 3;

                            }
                            else if (rbPV.Checked)
                            {
                                drRole.RoleID = 2;

                            }
                            objBLDetail.InsertUpdateRole(drRole);
                        }

                        else
                        {
                            DataRow drRole = dsDictionary.UserJoinRole.FindByUserJoinRoleID(UserJoinRoleID);
                            if (rbAdmin.Checked)
                            {
                                ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)drRole).RoleID = 0;
                            }
                            else if (rbBep.Checked)
                            {
                                ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)drRole).RoleID = 5;

                            }
                            else if (rbLeTan.Checked)
                            {
                                ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)drRole).RoleID = 3;

                            }
                            else if (rbPV.Checked)
                            {
                                ((Desktop.Entity.DictionaryDataSet.UserJoinRoleRow)drRole).RoleID = 2;

                            }
                            resultAccount = objBLDetail.InsertUpdateUser(drRole);
                        }

                       
                        
                    }
                }
            }
            return result;
        }

        private void Control_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            base.ControlTextEditor_Validate(sender);
        }

        private void tabGeneralInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabInfo_SelectedTabChanged(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs e)
        {
            try
            {
                if (e.Tab.Key == "tabGeneralInfo")
                {
                    this.Size = new System.Drawing.Size(465, 333);
                }
                else if (e.Tab.Key == "tabAccount")
                {
                    this.Size = new System.Drawing.Size(465, 190);
                }
                else if (e.Tab.Key == "tabRole")
                {
                    this.Size = new System.Drawing.Size(465, 195);
                   
                }
            }
            catch (System.Exception)
            {

            }


        }

        private void rbAdmin_CheckedChanged(object sender, System.EventArgs e)
        {
          
        }

        private void rbEmployee_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void txtEmployeeCode_ValueChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text) && !string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                tabInfo.Tabs[1].Enabled = true;
            }
            else
            {
                tabInfo.Tabs[1].Enabled = false;
                tabInfo.Tabs[2].Enabled = false;
            }
        }

        private void txtEmployeeName_ValueChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeCode.Text) && !string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                tabInfo.Tabs[1].Enabled = true;
            }
            else
            {
                tabInfo.Tabs[1].Enabled = false;
                tabInfo.Tabs[2].Enabled = false;
            }
        }

        private void txtUserName_ValueChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtCofirmPassword.Text))
            {
                tabInfo.Tabs[2].Enabled = true;
            }
            else
            {
                tabInfo.Tabs[2].Enabled = false;
            }
        }

        private void txtPassword_ValueChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtCofirmPassword.Text))
            {
                tabInfo.Tabs[2].Enabled = true;
            }
            else
            {
                tabInfo.Tabs[2].Enabled = false;
            }
        }

        private void txtCofirmPassword_ValueChanged(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtCofirmPassword.Text))
            {
                tabInfo.Tabs[2].Enabled = true;
            }
            else
            {
                tabInfo.Tabs[2].Enabled = false;
            }
        }
    }
}
