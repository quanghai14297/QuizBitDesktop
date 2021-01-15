using Infragistics.Win;
using Desktop.BL;
using Desktop.Lib;
using System;
using System.Windows.Forms;

namespace ClientApp.UI.Dictionary
{
    public partial class FrmLogin : Form
    {

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!CommonFunction.CheckNetWork()) return;
                var connect = CloudLibrary.CreateServiceConnection();
                Session.Token = connect.Login(txtUserName.Text, txtPassword.Text);
                if (!string.IsNullOrEmpty(Session.Token))
                {
                    Session.UserLogin = connect.GetUserLogin(txtUserName.Text, txtPassword.Text);
                    if(Session.UserLogin.RoleName.Contains("Chạy bàn") && Session.UserLogin.RoleName.Contains("Nhân viên bếp"))
                    {
                        MessageBoxCommon.ShowExclamation("Bạn không có quyền để đăng nhập vào đây.");
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                        
                }
                else
                    MessageBoxCommon.ShowExclamation("Tài khoản hoặc mật khẩu không chính xác.");
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
