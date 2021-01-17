using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.UI.BaseForm
{
    public partial class FrmBaseList : Form
    {
        public FrmBaseList()
        {
            InitializeComponent();
        }

        #region Declaration
        protected const string mnuAdd = "mnuAdd";
        protected const string mnuCopy = "mnuCopy";
        protected const string mnuEdit = "mnuEdit";
        protected const string mnuDelete = "mnuDelete";
        protected const string mnuGet = "mnuGet";
        #endregion

        #region Function

        /// <summary>
        /// Dòng dữ liệu hiện tại
        /// </summary>
        public DataRow CurrentRow
        {
            get { return bsList.Current != null ? ((System.Data.DataRowView)bsList.Current).Row : null; }
        }

        /// <summary>
        /// Ẩn hiện các nút chức năng khi thay đổi con trỏ
        /// </summary>
        protected void ShowHideFunctionByData()
        {
            if (bsList != null)
                if (bsList.Position >= 0)
                    if (grdList.ActiveRow != null)
                        if (grdList.ActiveRow.Index >= 0)
                        {
                            // Hiện xóa
                            tbrFunction.Tools[mnuDelete].SharedProps.Enabled = true;
                            // Kiểm tra số dòng
                            if (grdList.Selected.Rows.Count > 1 || (grdList.Selected.Rows.Count == 1 && grdList.Selected.Rows[0].Index != grdList.ActiveRow.Index))
                            {
                                tbrFunction.Tools[mnuCopy].SharedProps.Enabled = false;
                                tbrFunction.Tools[mnuEdit].SharedProps.Enabled = false;
                            }
                            else
                            {
                                tbrFunction.Tools[mnuCopy].SharedProps.Enabled = true;
                                tbrFunction.Tools[mnuEdit].SharedProps.Enabled = true;
                            }
                            return;
                        }
            tbrFunction.Tools[mnuCopy].SharedProps.Enabled = false;
            tbrFunction.Tools[mnuEdit].SharedProps.Enabled = false;
            tbrFunction.Tools[mnuDelete].SharedProps.Enabled = false;
        }

        /// <summary>
        /// Khởi tạo đối tượng BL
        /// </summary>
        protected virtual void InitBusinessObject()
        {

        }

        /// <summary>
        /// Load dữ liệu ràng buộc từ Bảng khóa ngoại
        /// </summary>
        protected virtual void LoadDataForeignKey()
        {

        }

        /// <summary>
        /// Hàm lấy dữ liệu
        /// </summary>
        protected virtual void LoadDataForm()
        {

        }

        /// <summary>
        /// Hàm binding dữ liệu
        /// </summary>
        protected virtual void BindingData()
        {

        }

        /// <summary>
        /// Show form chi tiết
        /// </summary>
        /// <param name="actionMode"></param>
        protected virtual void ShowFormDetail(ActionMode actionMode)
        {

        }

        /// <summary>
        /// Hàm khi click vào Toolbar
        /// </summary>
        protected virtual void ToolbarClick(string itemKey)
        {
            switch (itemKey)
            {
                case mnuGet: GetData(); break;
                case mnuAdd: Add(); break;
                case mnuCopy: Copy(); break;
                case mnuEdit: Edit(); break;
                case mnuDelete: Delete(); break;
            }
        }

        /// <summary>
        /// Thêm
        /// </summary>
        protected virtual void Add()
        {
            ShowFormDetail(ActionMode.AddNew);
        }

        /// <summary>
        /// Nhân bản
        /// </summary>
        protected virtual void Copy()
        {
            ShowFormDetail(ActionMode.Copy);
        }

        /// <summary>
        /// Sửa
        /// </summary>
        protected virtual void Edit()
        {
            ShowFormDetail(ActionMode.Edit);
        }

        /// <summary>
        /// Xóa
        /// </summary>
        protected virtual void Delete()
        {

        }

        /// <summary>
        /// Hàm nạp dữ liệu
        /// </summary>
        protected virtual void GetData()
        {
            if (!CommonFunction.CheckNetWork()) return;
            LoadDataForeignKey();
            LoadDataForm();
        }

        /// <summary>
        /// Xem chi tiết theo ID
        /// </summary>
        protected virtual void GetDetailByID()
        {

        }

        /// <summary>
        /// Kích hoạt và Select Row mà BindingSource đang được trỏ đến
        /// </summary>
        protected void ActiveAndSelectRow()
        {
            if (bsList != null && bsList.Count > 0)
            {
                grdList.Rows[bsList.Position].Activate();
                grdList.Rows[bsList.Position].Selected = true;
            }
            else ShowHideFunctionByData();
        }

        #endregion

        /// <summary>
        /// Sự kiện click vào item của toolbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbrFunction_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
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

        private void FrmBaseList_Load(object sender, EventArgs e)
        {
            try
            {
                InitBusinessObject();
                LoadDataForeignKey();
                LoadDataForm();
                BindingData();
                GetDetailByID();
                ShowHideFunctionByData();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Ẩn hiện tab thông tin chi tiết
        /// </summary>
        /// <param name="isVisible"></param>
        protected void SetVisibleTabInfo(bool isVisible)
        {
            pnlDetail.Visible = isVisible;
            splContent.Visible = isVisible;
        }

        /// <summary>
        /// Sự kiện thay đổi con trỏ của bsList
        /// <para>Dùng để lấy lại dữ liệu cho TabControl Info</para>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsList_CurrentChanged(object sender, EventArgs e)
        {
            try
            {
                if (bsList != null && bsList.Count > 0)
                {
                    GetDetailByID();
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_AfterRowActivate(object sender, EventArgs e)
        {
            try
            {
                ShowHideFunctionByData();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        #region Event Grid

        private void grdList_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        {
            try
            {
                if (grdList.ActiveCell != null)
                {
                    UltraGridRow row = grdList.ActiveRow;
                    if (row != null && row.Index >= 0)
                    {
                        grdList.ActiveCell = null;
                        row.Selected = true;
                    }
                }
                ShowHideFunctionByData();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_BeforeRowsDeleted(object sender, Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventArgs e)
        {
            try
            {
                Delete();
                // Vì Delete đã Remove row khỏi BindingSource nên không cần thực hiện DeleteRow nữa
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (bsList != null)
                    if (bsList.Current != null)
                        if (e.KeyCode == Keys.Return)
                        {
                            if (grdList.ActiveRow != null && grdList.Selected.Rows.Count == 1)
                            {
                                if (grdList.ActiveRow.GetType() == typeof(UltraGridRow))
                                    Edit();
                            }
                            else
                            {
                                grdList.Rows[bsList.Position].Activate();
                            }
                        }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                var mousePoint = new Point(e.X, e.Y);
                UIElement element = grdList.DisplayLayout.UIElement.ElementFromPoint(mousePoint);
                UltraGridRow row = (UltraGridRow)element.GetContext(typeof(UltraGridRow));
                if (row != null)
                    if (row.Index >= 0)
                        Edit();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void grdList_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    grdList.ActiveRow = null;

                    var mousePoint = new Point(e.X, e.Y);
                    UIElement element = grdList.DisplayLayout.UIElement.ElementFromPoint(mousePoint);
                    UltraGridRow row = (UltraGridRow)element.GetContext(typeof(UltraGridRow));
                    if (row != null)
                        if (row.Index >= 0)
                        {
                            row.Activated = true;
                            if (!grdList.Selected.Rows.Contains(row))
                                grdList.Selected.Rows.Clear();
                            ShowHideFunctionByData();
                        }
                        else grdList.Selected.Rows.Clear();
                    else grdList.Selected.Rows.Clear();
                }
                ShowHideFunctionByData();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        #endregion
    }
}
