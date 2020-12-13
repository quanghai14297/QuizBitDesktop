using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    /// <summary>
    /// Loại mặt hàng
    /// </summary>
    public enum EnumTableStatus
    {
        [Description("Trống")]
        Empty = 0,

        [Description("Đang phục vụ")]
        Using = 1,

        [Description("Đã đặt trước")]
        Booking = 2
    }

    /// <summary>
    /// Trạng thái của đặt bàn
    /// </summary>
    public enum EnumBookingStatus
    {
        [Description("Đã đặt trước")]
        Booked = 0,

        [Description("Đã nhận bàn")]
        Receiver = 1,

        [Description("Đã hủy")]
        Cancel = 2
    }

    /// <summary>
    /// Trạng thái của Order
    /// </summary>
    public enum EnumOrderStatus
    {
        [Description("Đang phục vụ")]
        Ordering = 0,

        [Description("Chờ thanh toán")]
        WaitPay = 1,

        [Description("Đã thanh toán")]
        Done = 2,

        [Description("Đã hủy")]
        Cancel = 3
    }

    /// <summary>
    /// Trạng thái của Order
    /// </summary>
    public enum EnumOrderDetailStatus
    {
        [Description("Chưa phục vụ")]
        Ordering = 0,

        [Description("Đã gửi bếp")]
        SendKitchen = 1,

        [Description("Đang chế biến")]
        Cooking = 2,

        [Description("Đã chế biến")]
        Cooked = 3,

        [Description("Đã phục vụ")]
        Served = 3,

        [Description("Đã hủy")]
        Cancel = 3
    }

    /// <summary>
    /// Chức năng Hủy
    /// </summary>
    public enum EnumCancelAction
    {
        [Description("Order")]
        Order = 0,

        [Description("Đặt bàn")]
        Booking = 1
    }
}
