using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLBooking : BLBase
    {
        public BLBooking()
        {
            oDL = new DL.DLBooking();
        }

        /// <summary>
        /// Lấy ra danh sách đặt bàn theo ngày
        /// </summary>
        /// <param name="fromTime">Thời gian lấy dữ liệu</param>
        /// <returns></returns>
        public DataTable GetBookingByFromTime(DateTime fromDay)
        {
            return new DL.DLBooking().GetBookingByFromTime(fromDay);
        }

        public DataRow InitNewRow(DictionaryDataSet.BookingDataTable table)
        {
            DictionaryDataSet.BookingRow drNewRow = table.NewBookingRow();
            drNewRow.BookingID = Guid.NewGuid();
            drNewRow.BookingNo = DateTime.Now.Year.ToString() + "." + GetNumberBooking(DateTime.Now.Year);
            drNewRow.BookingDate = DateTime.Now;
            drNewRow.BookingStatus = 0;
            drNewRow.FromTime = DateTime.Now;
            drNewRow.NumberOfPeople = 1;
            drNewRow.RequestMeal = "";
            drNewRow.EmployeeID = Session.UserLogin.EmployeeID;
            drNewRow.CreatedDate = DateTime.Now;
            drNewRow.CreatedBy = Session.UserLogin.UserName;
            drNewRow.ModifiedDate = DateTime.Now;
            drNewRow.ModifiedBy = Session.UserLogin.UserName;
            table.Rows.InsertAt(drNewRow, 0);
            return drNewRow;
        }

        /// <summary>
        /// Lấy ra số đặt bàn tiếp theo trong năm
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public string GetNumberBooking(int year)
        {
            return new DL.DLBooking().GetNumberBooking(year);
        }

        /// <summary>
        /// Thay đổi trạng thái Đặt bàn
        /// </summary>
        /// <param name="bookingID"></param>
        /// <param name="bookingStatus"></param>
        public bool ChangeBookingStatus(Guid bookingID, int bookingStatus)
        {
            return new DL.DLBooking().ChangeBookingStatus(bookingID, bookingStatus);
        }

        /// <summary>
        /// Kiểm tra BookingNo có trùng không
        /// <para>true - đã tồn tại || false - chưa tồn tại</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool CheckCodeIsExists(Guid id, string bookingNo)
        {
            return new DL.DLBooking().CheckCodeIsExists(id, bookingNo);
        }
    }
}
