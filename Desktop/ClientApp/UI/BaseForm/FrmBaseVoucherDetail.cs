using Desktop.Entity;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinToolTip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.UI.BaseForm
{
    public partial class FrmBaseVoucherDetail : Form
    {
        public FrmBaseVoucherDetail()
        {
            InitializeComponent();
        }

        #region Declaration
        protected const string btnAdd = "btnAdd";
        protected const string btnEdit = "btnEdit";
        protected const string btnSave = "btnSave";
        protected const string btnDelete = "btnDelete";
        protected const string btnCancel = "btnCancel";
        protected const string btnGet = "btnGet";
        protected const string btnHelp = "btnHelp";
        protected const string btnClose = "btnClose";

        protected const string popNext = "popNext";
        protected const string popPrevious = "popPrevious";
        protected const string btnNext = "btnNext";
        protected const string btnPrevious = "btnPrevious";
        protected const string btnMoveFirst = "btnMoveFirst";
        protected const string btnMoveLast = "btnMoveLast";

        private ActionMode _formActionMode;

        /// <summary>
        /// Trạng thái mở form
        /// </summary>
        public ActionMode FormActionMode
        {
            get { return _formActionMode; }
            set { _formActionMode = value; }
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
        public BindingSource BsList
        {
            get { return bsList; }
            set { bsList = value; }
        }

        /// <summary>
        /// BindingSource
        /// </summary>
        public BindingSource BsDetail
        {
            get { return bsDetail; }
            set { bsDetail = value; }
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
                case btnAdd:
                    Add();
                    break;
                case btnEdit:
                    Edit();
                    break;
                case btnSave:
                    Save();
                    break;
                case btnDelete:
                    Delete();
                    break;
                case btnCancel:
                    Cancel();
                    break;
                case btnGet:
                    Get();
                    break;
                case btnHelp:
                    Help();
                    break;
                case btnClose:
                    CloseForm();
                    break;
                case popNext:
                case btnNext:
                    MoveNext();
                    break;
                case popPrevious:
                case btnPrevious:
                    MovePrevious();
                    break;
                case btnMoveFirst:
                    MoveFirst();
                    break;
                case btnMoveLast:
                    MoveLast();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Thêm mới
        /// </summary>
        protected virtual void Add()
        {
            FormActionMode = ActionMode.AddNew;
        }

        /// <summary>
        /// Cập nhật
        /// </summary>
        protected virtual void Edit()
        {
            FormActionMode = ActionMode.Edit;
        }

        /// <summary>
        /// Xóa
        /// </summary>
        protected virtual void Delete()
        {

        }

        /// <summary>
        /// Lưu
        /// </summary>
        protected virtual void Save()
        {
            FormActionMode = ActionMode.View;
        }

        /// <summary>
        /// Hoãn lại
        /// </summary>
        protected virtual void Cancel()
        {
            FormActionMode = ActionMode.View;
            dsDictionary.RejectChanges();
        }

        /// <summary>
        /// Lấy lại dữ liệu
        /// </summary>
        protected virtual void Get()
        {
            LoadDataForeignKey();
            LoadDataForm();
            LoadDataDetail();
        }

        /// <summary>
        /// Help
        /// </summary>
        protected virtual void Help()
        {
            Process.Start("http://khanhjm.com/help");
        }

        /// <summary>
        /// Đóng
        /// </summary>
        protected virtual void CloseForm()
        {
            this.Close();
        }

        /// <summary>
        /// Bản ghi sau
        /// </summary>
        protected virtual void MoveNext()
        {
            bsList.MoveNext();
            LoadDataDetail();
        }

        /// <summary>
        /// Bản ghi trước
        /// </summary>
        protected virtual void MovePrevious()
        {
            bsList.MovePrevious();
            LoadDataDetail();
        }

        /// <summary>
        /// Bản ghi đầu
        /// </summary>
        protected virtual void MoveFirst()
        {
            bsList.MoveFirst();
            LoadDataDetail();
        }

        /// <summary>
        /// Bản ghi cuối
        /// </summary>
        protected virtual void MoveLast()
        {
            bsList.MoveLast();
            LoadDataDetail();
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
        /// Hàm Set ToolTip cho các control khi Validate giá trị nhập vào
        /// </summary>
        /// <param name="control"></param>
        protected void ControlTextEditor_Validate(UltraTextEditor control)
        {
            try
            {
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
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void FrmBaseVoucherDetail_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataForeignKey();
                LoadDataForm();
                LoadDataDetail();
                BindingData();
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
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
            }
        }

        /// <summary>
        /// Hàm lấy chi tiết dữ liệu
        /// </summary>
        protected virtual void LoadDataDetail()
        {

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
        /// Lưu dữ liệu
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaveData()
        {
            return true;
        }

        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <returns>>Mã đã tồn tại chưa: Tồn tại - True || Không tồn tại - False</returns>
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
            return true;
        }
    }
}
