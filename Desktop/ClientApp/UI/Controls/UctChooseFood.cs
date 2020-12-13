using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop.Entity;
using Desktop.BL;

namespace ClientApp.UI.Controls
{
    public partial class UctChooseFood : UserControl
    {
        public UctChooseFood()
        {
            InitializeComponent();
        }

        public UctChooseFood(DictionaryDataSet.OrderDetailRow itemRow)
        {
            InitializeComponent();
            if (itemRow != null)
            {
                OrderDetailRow = itemRow;
            }
        }

        #region Declaration

        private bool _isRemove = false;

        private Desktop.Entity.DictionaryDataSet.InventoryItemRow _inventoryItemRow;

        private Desktop.Entity.DictionaryDataSet.OrderDetailRow _orderDetailRow;

        private int _sortOrder;

        private decimal _unitPrice;

        private decimal _quantity = 1;

        private bool _isSendKitchen;

        #endregion

        /// <summary>
        /// Có xóa món ăn khỏi Order không
        /// </summary>
        public bool IsRemove
        {
            get { return _isRemove; }
            set
            {
                _isRemove = value;
            }
        }

        /// <summary>
        /// Mặt hàng được chọn
        /// </summary>
        public Desktop.Entity.DictionaryDataSet.InventoryItemRow InventoryItemRow
        {
            get { return _inventoryItemRow; }
            set
            {
                _inventoryItemRow = value;
                if (_inventoryItemRow != null)
                {
                    UnitPrice = _inventoryItemRow.IsUnitPriceNull() ? 0 : _inventoryItemRow.UnitPrice;
                    lblInventoryItemName.Text = _inventoryItemRow.InventoryItemName;
                }
            }
        }

        /// <summary>
        /// Chi tiết Order được chọn
        /// </summary>
        public Desktop.Entity.DictionaryDataSet.OrderDetailRow OrderDetailRow
        {
            get { return _orderDetailRow; }
            set
            {
                _orderDetailRow = value;
                if (_orderDetailRow != null)
                {
                    if (!_orderDetailRow.IsInventoryItemIDNull())
                    {
                        //InventoryItemRow = ShareDictionary.DsDictionary.InventoryItem.FindByInventoryItemID(_orderDetailRow.InventoryItemID);
                        InventoryItemRow = new BLInventoryItem().GetByID(_orderDetailRow.InventoryItemID);
                    }
                    SortOrder = _orderDetailRow.SortOrder;
                    bsFood.DataSource = _orderDetailRow.Table;
                    bsFood.Position = bsFood.Find("OrderDetailID", _orderDetailRow.OrderDetailID);
                    txtQuantity.DataBindings.Add(new Binding("Text", bsFood, "Quantity", true));
                    Quantity = _orderDetailRow.Quantity;
                    CheckSendKitchen();
                }
            }
        }

        /// <summary>
        /// Thứ tự trong order
        /// </summary>
        public int SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                lblUnitPrice.Text = CommonFunction.ConvertToCurrency(_unitPrice);
                lblAmount.Text = CommonFunction.ConvertToCurrency(_unitPrice * _quantity);
            }
        }

        /// <summary>
        /// Số lượng món ăn được gọi
        /// </summary>
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OrderDetailRow.Quantity = _quantity;
                txtQuantity.Text = _quantity.ToString();
                lblAmount.Text = CommonFunction.ConvertToCurrency(_unitPrice * _quantity);
            }
        }

        /// <summary>
        /// Đã gửi bếp chưa
        /// </summary>
        public bool IsSendKitchen
        {
            get { return _isSendKitchen; }
            set
            {
                _isSendKitchen = value;
                CheckVisibleBySendKitchenStatus();
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái gửi bếp thì không cho Remove item
        /// </summary>
        public void CheckVisibleBySendKitchenStatus()
        {
            pnlRemove.Visible = !IsSendKitchen;
            txtQuantity.Enabled = !IsSendKitchen;
            if (IsSendKitchen) txtQuantity.SpinButtonDisplayStyle = Infragistics.Win.SpinButtonDisplayStyle.None;
            else txtQuantity.SpinButtonDisplayStyle = Infragistics.Win.SpinButtonDisplayStyle.OnRight;
        }

        public void txtQuantity_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuantity.Text))
                    Quantity = 0;
                else
                {
                    int number = int.Parse(txtQuantity.Text);
                    if (number >= 0)
                    {
                        Quantity = number;
                    }
                }
                OnCalculateAmount(e);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        private void txtQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text.Trim()))
            {
                MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control, txtQuantity.Tag));
                txtQuantity.Focus();
                return;
            }
            else
            {
                int table = 0;
                if (int.TryParse(txtQuantity.Value.ToString(), out table))
                {
                    if (table <= 0)
                    {
                        MessageBoxCommon.ShowExclamation(string.Format(Properties.Resources.Message_Validate_Control_More, txtQuantity.Tag, "0"));
                        txtQuantity.Focus();
                        return;
                    }
                }
            }
        }

        private void UctChooseFood_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái gửi bếp
        /// </summary>
        private void CheckSendKitchen()
        {
            IsSendKitchen = new BLOrder().CheckSendKitchenOrderDetail(_orderDetailRow.OrderDetailID);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!IsRemove)
                Quantity += 1;
            base.OnClick(e);
        }

        /// <summary>
        /// Xóa khỏi Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                IsRemove = true;
                this.OnClick(e);
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        public event EventHandler CalculateAmount;

        /// <summary>
        /// Tính lại tổng tiền
        /// </summary>
        protected virtual void OnCalculateAmount(EventArgs e)
        {
            if (CalculateAmount != null)
            {
                CalculateAmount(this, e);
            }
        }
    }
}
