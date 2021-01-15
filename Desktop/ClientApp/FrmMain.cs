using System;
using System.Windows.Forms;
using ClientApp.UI.Dictionary;
using ClientApp.UI.Business;
using ClientApp.UI.Report;
using Desktop.BL;
using Desktop.Entity;
using QuizBit.Entity;

namespace ClientApp
{
    public partial class FrmMain : Form
    {
        #region Declaration

        const string btnLogin = "btnLogin";
        const string btnLogout = "btnLogout";
        const string btnExit = "btnExit";

        const string btnAbout = "btnAbout";

        const string btnUnit = "btnUnit";
        const string expUnit = "expUnit";

        const string btnInventoryItemCategory = "btnInventoryItemCategory";
        const string expInventoryItemCategory = "expInventoryItemCategory";

        const string expEmployee = "expEmployee";
        const string btnInventoryItem = "btnInventoryItem";
        const string expInventoryItem = "expInventoryItem";

        const string btnCustomer = "btnCustomer";
        const string expCustomer = "expCustomer";

        const string btnArea = "btnArea";
        const string expArea = "expArea";

        const string btnBooking = "btnBooking";
        const string expBooking = "expBooking";


        const string btnVoucher = "btnVoucher";
        const string expVoucher = "expVoucher";
        const string expUser = "expUser";

        const string mnuCategory = "mnuCategory";
        const string mnuBusiness = "mnuBusiness";

        const string tbrFunctionShare = "tbrFunctionShare";

        const string stbUserName = "stbUserName";

        const string btnSelectWorkPlace = "btnSelectWorkPlace";

        #endregion

        #region Event

        public FrmMain()
        {
            InitializeComponent();
        }

        private void tbrManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            try
            {
                ToolbarClick(e.Tool.Key);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exbMain_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
        {
            try
            {
                ToolbarClick(e.Item.Key);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Dispose();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        #endregion

        #region Function

        /// <summary>
        /// Hàm tìm các form con đã mở trong Main
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private Form findOpenedForm(string formName)
        {
#if Debug
            CloseMdiChildrenForm(formName);
#endif
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name.Equals(formName))
                {
                    return form;
                }
            }
            return null;
        }

        /// <summary>
        /// Đóng toàn bộ Form con của Main
        /// </summary>
        private void CloseMdiChildrenForm(string key = "")
        {
            foreach (Form form in this.MdiChildren)
            {
                if (key != "" && form.Name.Equals(key)) continue;
                form.Close();
                form.Dispose();
            }
        }

        private void ShowOrder()
        {
            var fList = findOpenedForm("FrmOrder");
            if (fList == null)
            {
                //fList = new FrmUser();
                fList.MdiParent = this;
            }
            fList.Show();
            fList.Select();
        }

        /// <summary>
        /// Show các form thông dụng theo key
        /// </summary>
        /// <param name="key"></param>
        private void ShowForm(string key)
        {
            CloseMdiChildrenForm();
            Form fList = null;
            switch (key)
            {
                case TableName.Order:
                    fList = findOpenedForm("FrmOrder");
                    if (fList == null)
                    {
                        //fList = new FrmUser();
                        fList.MdiParent = this;
                    }
                    break;
                case expUser:
                    fList = findOpenedForm("FrmUser");
                    if (fList == null)
                    {
                        //fList = new FrmUser();
                        fList.MdiParent = this;
                    }
                    break;
                case "expReportSales":
                    fList = findOpenedForm("frmReportSales");
                    if (fList == null)
                    {
                        fList = new frmReportSales();
                        fList.MdiParent = this;
                    }
                    break;
                case "expReportSalesArea":
                    fList = findOpenedForm("frmReportSalesArea");
                    if (fList == null)
                    {
                        fList = new frmReportSalesArea();
                        fList.MdiParent = this;
                    }
                    break;
                case "expReportSalesCustomer":
                    fList = findOpenedForm("frmReportSalesByCustomer");
                    if (fList == null)
                    {
                        fList = new frmReportSalesByCustomer();
                        fList.MdiParent = this;
                    }
                    break;
                case "expReportSalesEmployee":
                    fList = findOpenedForm("frmReportSalesByEmployee");
                    if (fList == null)
                    {
                        fList = new frmReportSalesByEmployee();
                        fList.MdiParent = this;
                    }
                    break;
                case "expSellingInventory":
                    fList = findOpenedForm("frmSellingInventory");
                    if (fList == null)
                    {
                        fList = new frmSellingInventory();
                        fList.MdiParent = this;
                    }
                    break;
                case expUnit:
                case btnUnit:
                    fList = findOpenedForm("FrmUnit");
                    if (fList == null)
                    {
                        fList = new FrmUnit();
                        fList.MdiParent = this;
                    }
                    break;
                case expInventoryItemCategory:
                case btnInventoryItemCategory:
                    fList = findOpenedForm("FrmInventoryItemCategory");
                    if (fList == null)
                    {
                        fList = new FrmInventoryItemCategory();
                        fList.MdiParent = this;
                    }
                    break;
                case expEmployee:
                    fList = findOpenedForm("frmEmployee");
                    if (fList == null)
                    {
                        fList = new frmEmployee();
                        fList.MdiParent = this;
                    }
                    break;
                case expInventoryItem:
                case btnInventoryItem:
                    fList = findOpenedForm("FrmInventoryItem");
                    if (fList == null)
                    {
                        fList = new FrmInventoryItem();
                        fList.MdiParent = this;
                    }
                    break;
                case expCustomer:
                case btnCustomer:
                    fList = findOpenedForm("FrmCustomer");
                    if (fList == null)
                    {
                        fList = new FrmCustomer();
                        fList.MdiParent = this;
                    }
                    break;
                case expArea:
                case btnArea:
                    fList = findOpenedForm("FrmArea");
                    if (fList == null)
                    {
                        fList = new FrmArea();
                        fList.MdiParent = this;
                    }
                    break;
                case expBooking:
                case btnBooking:
                    fList = findOpenedForm("FrmBooking");
                    if (fList == null)
                    {
                        fList = new FrmBooking();
                        fList.MdiParent = this;
                    }
                    break;
                default:
                    break;
            }
            fList.Show();
            fList.Select();
            if (Session.UserLogin.RoleName.Contains("Lễ tân"))
            {
                exbMain.Groups[0].Items[0].Visible = false;
                exbMain.Groups[0].Items[1].Visible = false;
                exbMain.Groups[0].Items[2].Visible = false;
                exbMain.Groups[0].Items[3].Visible = false;
                exbMain.Groups[1].Visible = false;
                exbMain.Groups[2].Visible = false;
            }
        }

        /// <summary>
        /// Sự kiện click vào Menu
        /// </summary>
        /// <param name="key"></param>
        private void ToolbarClick(string key)
        {
            switch (key)
            {
                case btnLogin:
                    // Đăng nhập
                    Login();
                    break;
                case btnLogout:
                    // Đăng xuất
                    Logout();
                    break;
                case btnSelectWorkPlace:
                    using (var f = new FrmCompanyInfo())
                    {
                        f.ShowDialog();
                    }
                    break;
                default:
                    ShowForm(key);
                    break;
            }
        }

        /// <summary>
        /// Sự kiện Load Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                SetVisibilityByUserLogin();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Load xong form Main thì bắt đầu đăng nhập
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            try
            {
                Login();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        private void Login()
        {
            var fLogin = new FrmLogin();
            var result = fLogin.ShowDialog();
            if (result == DialogResult.OK)
            {
                SetVisibilityByUserLogin();
                var f = new FrmBooking();
                f.MdiParent = this;
                f.Show();
                LoginSuccess();
            }
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        private void Logout()
        {
            CloseMdiChildrenForm();
            Session.Token = string.Empty;
            Session.UserLogin = null;
            SetVisibilityByUserLogin();
            timerLoadData.Enabled = false;
            if (bgwLoadData != null && bgwLoadData.IsBusy) bgwLoadData.CancelAsync();
            Login();
        }

        /// <summary>
        /// Ẩn hiện các panel Margin
        /// </summary>
        /// <param name="visible"></param>
        private void SetVisibilityMargin(bool visible)
        {
            pnlMarginTop.Visible = visible;
            pnlMarginBottom.Visible = visible;
            pnlMarginLeft.Visible = visible;
            pnlMarginRight.Visible = visible;
            splMain.Visible = visible;
        }

        /// <summary>
        /// Set Visible cho các control khi đăng nhập đăng xuất
        /// </summary>
        private void SetVisibilityByUserLogin()
        {
            var isLogin = Session.IsLogin;
            SetVisibilityMargin(isLogin);
            tbrManager.Tools[btnLogout].SharedProps.Enabled = isLogin;
            tbrManager.Tools[mnuCategory].SharedProps.Visible = isLogin;
            tbrManager.Tools[mnuBusiness].SharedProps.Visible = isLogin;
            tbrManager.Toolbars[tbrFunctionShare].Visible = isLogin;
            exbMain.Visible = isLogin;
            tbrManager.Tools[btnLogin].SharedProps.Enabled = !isLogin;
            stbMain.Panels[stbUserName].Text = string.Format("Người dùng: {0}", isLogin ? Session.UserLogin.DisplayName : string.Empty);
        }

        #endregion

        #region Load dữ liệu ẩn

        /// <summary>
        /// Sự kiện xử lý dữ liệu khi đăng nhập thành công
        /// </summary>
        private void LoginSuccess()
        {
            //ShareDictionary.LoadUnit(true);
            //ShareDictionary.LoadInventoryItemCategory(true);
            //ShareDictionary.LoadInventoryItem(true);
            //timerLoadData.Enabled = true;
            timerLoadData.Interval = 500;
        }


        private void bgwLoadData_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                ShareDictionary.LoadBooking(true);
            }
            catch (Exception ex)
            {
                //MessageBoxCommon.ShowException(ex);
            }
        }

        private void bgwLoadData_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                // Tự hủy Đặt bàn
                if (ShareDictionary.DsDictionary.Booking.Rows.Count == 0) return;
                foreach (DictionaryDataSet.BookingRow item in ShareDictionary.DsDictionary.Booking.Rows)
                {
                    if (item.BookingStatus == (int)EnumBookingStatus.Booked && DateTime.Now.CompareTo(item.FromTime.AddMinutes(30)) > 0)
                    {
                        item.CancelDescription = "Tự động hủy do đã quá 30 phút mà khách hàng chưa nhận bàn";
                        item.BookingStatus = (int)EnumBookingStatus.Cancel;
                        new BLBooking().InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Booking>(item));
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBoxCommon.ShowException(ex);
            }
        }

        private void timerLoadData_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!bgwLoadData.IsBusy)
                    bgwLoadData.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        #endregion
    }
}
