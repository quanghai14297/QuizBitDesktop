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
using QuizBit.Entity;
using ClientApp.UI.Business;

namespace ClientApp.UI.Controls
{
    public partial class UctOrder : UserControl
    {
        #region Property

        private BLOrder oBL = new BLOrder();

        private Guid _orderID;

        public Guid OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        private BusinessDataSet.OrderViewRow _orderRow;

        public BusinessDataSet.OrderViewRow OrderRow
        {
            get { return _orderRow; }
            set
            {
                _orderRow = value;
                if (_orderRow != null)
                {
                    OrderStatus = (EnumOrderStatus)_orderRow.OrderStatus;
                    OrderID = _orderRow.OrderID;
                    lblOrderNo.Text = _orderRow.OrderNo;
                    lblNumberOfPeople.Text = _orderRow.NumberOfPeople.ToString();
                    lblArea.Text = _orderRow.AreaName;
                    lblTable.Text = _orderRow.TableName;
                    lblTotalAmount.Text = CommonFunction.ConvertToCurrency(_orderRow.TotalAmount);
                }
            }
        }

        private EnumOrderStatus _orderStatus;

        public EnumOrderStatus OrderStatus
        {
            get { return _orderStatus; }
            set
            {
                _orderStatus = value;
                if (_orderStatus == EnumOrderStatus.Ordering)
                {
                    pnlTop.Appearance.BackColor = Color.FromArgb(128, 128, 255);
                    //pnlBottom.Appearance.BackColor = Color.FromArgb(128, 128, 255);
                }
                else if (_orderStatus == EnumOrderStatus.WaitPay)
                {
                    pnlTop.Appearance.BackColor = Color.FromArgb(255, 143, 0);
                    //pnlBottom.Appearance.BackColor = Color.FromArgb(255, 143, 0);
                }
                else
                {
                    pnlTop.Appearance.BackColor = Color.Silver;
                    btnPayment.Visible = false;
                    btnCancel.Visible = false;
                    btnReturnFood.Visible = false;
                }
            }
        }

        #endregion

        public UctOrder()
        {
            InitializeComponent();
        }

        private void UctOrder_Load(object sender, EventArgs e)
        {

        }

        private void Label_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void Label_DoubleClick(object sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        /// <summary>
        /// Trả món cho khách
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnFood_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Yêu cầu thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (!oBL.CheckSendKitchenOrder(OrderID))
                {
                    MessageBoxCommon.ShowExclamation("Order chưa được phục vụ món ăn nào nên không thể thanh toán");
                    return;
                }
                using (var frm = new FrmInvoice())
                {
                    frm.FormActionMode = ActionMode.AddNew;
                    frm.OrderID = OrderID;
                    frm.SAInvoiceID = OrderID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        this.OrderStatus = EnumOrderStatus.Done;
                        this.OnClick(e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }

        /// <summary>
        /// Hủy Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (oBL.CheckSendKitchenOrder(OrderID))
                {
                    MessageBoxCommon.ShowExclamation("Order đã gửi bếp không thể hủy");
                    return;
                }
                int result = 1;
                string cancelDescription = "";
                {
                    if (MessageBoxCommon.ShowYesNoQuestion_Cancel(EnumCancelAction.Booking, out cancelDescription) == DialogResult.Yes)
                    {
                        DictionaryDataSet.OrderRow drObjectChange = (DictionaryDataSet.OrderRow)ShareDictionary.DsDictionary.Order.FindByOrderID(OrderID);
                        if (drObjectChange != null)
                        {
                            drObjectChange.CancelReason = cancelDescription;
                            drObjectChange.CancelEmployeeID = Session.UserLogin.EmployeeID;
                            drObjectChange.OrderStatus = (int)EnumOrderStatus.Cancel;
                            result = oBL.InsertUpdateObject(QuizBit.Contract.CommonFunction.GetItem<Order>(drObjectChange));
                            if (result > 0)
                                OrderStatus = EnumOrderStatus.Cancel;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBoxCommon.ShowException(ex);
            }
        }
    }
}
