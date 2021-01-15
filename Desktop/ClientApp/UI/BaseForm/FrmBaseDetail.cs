using Desktop.Entity;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
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
    public partial class FrmBaseDetail : Form
    {
        public FrmBaseDetail()
        {
            InitializeComponent();
        }

        #region Declaration
        protected const string mnuSave = "mnuSave";
        protected const string mnuClose = "mnuClose";

        private ActionMode _formActionMode;

        public ActionMode FormActionMode
        {
            get { return _formActionMode; }
            set
            {
                _formActionMode = value;
                ChangeFormByActionMode();
            }
        }

        /// <summary>
        /// Thay đổi giao diện form do ActionMode
        /// </summary>
        protected virtual void ChangeFormByActionMode()
        {
            switch (FormActionMode)
            {
                case ActionMode.View:
                    Text = String.Format("Xem {0}", this.Tag);
                    break;
                case ActionMode.AddNew:
                    Text = String.Format("Thêm mới {0}", this.Tag);
                    InitNewRow();
                    break;
                case ActionMode.Edit:
                    Text = String.Format("Cập nhật {0}", this.Tag);
                    break;
                case ActionMode.Copy:
                    Text = String.Format("Nhân bản {0}", this.Tag);
                    InitCopyRow();
                    break;
            }
        }


        /// <summary>
        /// DataSet
        /// </summary>
        public DictionaryDataSet DsDictionary
        {
            get { return dsDictionary; }
            set { dsDictionary = value; }
        }

        /// <summary>
        /// BindingSource
        /// </summary>
        public BindingSource BsDetail
        {
            get { return bsDetail; }
            set { bsDetail = value; }
        }

        /// <summary>
        /// Dòng dữ liệu hiện tại
        /// </summary>
        public DataRow CurrentRow
        {
            get { return bsDetail.Current != null ? ((System.Data.DataRowView)bsDetail.Current).Row : null; }
        }

        #endregion

        /// <summary>
        /// Xử lý sự kiện click vào Menu
        /// </summary>
        /// <param name="itemKey"></param>
        protected void ToolbarClick(string itemKey)
        {
            switch (itemKey)
            {
                case mnuSave:
                    // Ctrl S
                    btnSave.PerformClick();
                    break;
                case mnuClose:
                    // Ctrl F4
                    btnCancel.PerformClick();
                    break;
                default:
                    break;
            }
        }

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

        /// <summary>
        /// Hàm Set ToolTip cho các control khi Validate giá trị nhập vào
        /// </summary>
        /// <param name="sender">Đối tượng control</param>
        protected void ControlTextEditor_Validate(object sender)
        {
            try
            {
                UltraTextEditor control = (UltraTextEditor)sender;
                if (string.IsNullOrEmpty(control.Text.Trim()))
                {
                    control.Appearance.BorderColor = Color.Red;
                    ttpManager.SetUltraToolTip(control, new UltraToolTipInfo(String.Format(Properties.Resources.Message_Validate_Control, control.Tag), ToolTipImage.None, null, DefaultableBoolean.False));
                }
                else
                {
                    control.Appearance.BorderColor = Color.FromArgb(125, 162, 206);
                    ttpManager.SetUltraToolTip(control, null);
                }
            }
            catch (Exception ex)
            {
#if Debug
                MessageBoxCommon.ShowException(ex);
#endif
            }
        }

        protected void ControlCombo_ItemNotInList(object sender, ValidationErrorEventArgs e)
        {
            try
            {
                UltraCombo control = (UltraCombo)sender;
                if (string.IsNullOrEmpty(control.Text.Trim()))
                {
                    return;
                }
                // Tiếp tục Focus vào control có giá trị nhập sai
                e.RetainFocus = true;
                // Thông báo lỗi dữ liệu nhập
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Error_Input_Dropdown, control.Tag));

                if (e.LastValidValue != null)
                    control.Value = e.LastValidValue;
                else
                    control.Text = "";
                control.SelectAll();
            }
            catch (Exception ex)
            {
#if Debug
                MessageBoxCommon.ShowException(ex);
#endif
            }
        }

        protected void ControlComboEditor_ItemNotInList(object sender, ValidationErrorEventArgs e)
        {
            try
            {
                UltraComboEditor control = (UltraComboEditor)sender;
                if (string.IsNullOrEmpty(control.Text.Trim()))
                {
                    return;
                }
                // Tiếp tục Focus vào control có giá trị nhập sai
                e.RetainFocus = true;
                // Thông báo lỗi dữ liệu nhập
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Error_Input_Dropdown, control.Tag));

                if (e.LastValidValue != null)
                    control.Value = e.LastValidValue;
                else
                    control.SelectedIndex = 0;
                control.SelectAll();
                control.DropDown();
            }
            catch (Exception ex)
            {
#if Debug
                MessageBoxCommon.ShowException(ex);
#endif
            }
        }

        private void FrmBaseDetail_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataForeignKey();
                LoadDataForm();
                BindingData();
                if (FormActionMode == ActionMode.View)
                {
                    SetReadOnlyToView(true);
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        protected void SetReadOnlyToView(bool v)
        {
            foreach (Control item in tabGeneralInfo.Controls)
            {
                item.Enabled = !v;
            }

            btnSave.Enabled = !v;
            btnSaveAdd.Enabled = !v;
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
            btnSaveAdd.Visible = false;
            btnSave.Location = new Point(btnCancel.Location.X - 8 - 75, btnCancel.Location.Y);
        }

        /// <summary>
        /// Hàm binding dữ liệu lên control
        /// </summary>
        protected virtual void BindingData()
        {

        }

        /// <summary>
        /// Khởi tạo dòng dữ liệu mới
        /// </summary>
        protected virtual void InitNewRow()
        {

        }

        /// <summary>
        /// Khởi tạo dòng dữ liệu mới
        /// </summary>
        protected virtual void InitCopyRow()
        {

        }

        /// <summary>
        /// Lưu dữ liệu
        /// </summary>
        /// <returns></returns>
        protected virtual int SaveData()
        {
            return 0;
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// <para>Mã có hợp lệ ko: true - hợp lệ || false - không hợp lệ</para>
        /// </summary>
        /// <returns></returns>
        protected virtual bool CheckCodeIsExists()
        {
            return true;
        }

        /// <summary>
        /// Kiểm tra Form
        /// </summary>
        /// <returns></returns>
        protected virtual bool ValidateForm()
        {
            if (!CommonFunction.CheckNetWork()) return false;
            //if (CurrentRow.RowState == DataRowState.Unchanged)
            //    return false;
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;
                if (!CheckCodeIsExists()) return;
                var result = SaveData();
               
                if (result == (int)EnumResultInsertUpdate.Success)
                {
                    DsDictionary.AcceptChanges();
                    DialogResult = DialogResult.OK;
                }
                else if (result == (int)EnumResultInsertUpdate.DuplicateCode)
                {
                    if (CurrentRow.Table == DsDictionary.Area || CurrentRow.Table == DsDictionary.Unit)
                        MessageBoxCommon.ShowExclamation("Trùng tên");
                    else
                        MessageBoxCommon.ShowExclamation("Trùng mã");
                }
                else if (result == (int)EnumResultInsertUpdate.Failed)
                {
                    MessageBoxCommon.ShowExclamation("Lưu dữ liệu thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnSaveAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm()) return;
                if (!CheckCodeIsExists()) return;
                if (SaveData() == (int)EnumResultInsertUpdate.Success)
                {
                    DsDictionary.AcceptChanges();
                    // Khởi tạo dòng mới
                    FormActionMode = ActionMode.AddNew;
                    LoadDataForm();
                }
                else if (SaveData() == (int)EnumResultInsertUpdate.DuplicateCode)
                {
                    MessageBoxCommon.ShowExclamation("Trùng mã");
                }
                else if (SaveData() == (int)EnumResultInsertUpdate.Failed)
                {
                    MessageBoxCommon.ShowExclamation("Lưu dữ liệu thất bại");
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
