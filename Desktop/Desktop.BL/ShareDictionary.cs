using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Entity;
using QuizBit.Entity;

namespace Desktop.BL
{
    public static class ShareDictionary
    {
        private static Timer timerObj = null;

        private static DateTime lastUpdateTS = DateTime.MinValue;

        public static int refreshInterval { get; set; }

        private static DictionaryDataSet _dsDictionary;

        /// <summary>
        /// DataSet danh mục
        /// </summary>
        public static DictionaryDataSet DsDictionary
        {
            get
            {
                if (_dsDictionary == null)
                    _dsDictionary = new DictionaryDataSet();
                return _dsDictionary;
            }
            set { _dsDictionary = value; }
        }

        private static BusinessDataSet _dsBusiness;

        /// <summary>
        /// DataSet để View nghiệp vụ
        /// </summary>
        public static BusinessDataSet DsBusiness
        {
            get
            {
                if (_dsBusiness == null)
                    _dsBusiness = new BusinessDataSet();
                return _dsBusiness;
            }
            set { _dsBusiness = value; }
        }


        /// <summary>
        /// Load các thực thể
        /// </summary>
        public static void LoadDictionary()
        {
            try
            {
                if (timerObj == null) timerObj = new Timer(CallAPI_LoadDictionary, null, Timeout.Infinite, Timeout.Infinite);
                timerObj.Change(0, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gọi API Lấy dữ liệu
        /// </summary>
        /// <param name="state"></param>
        private static void CallAPI_LoadDictionary(object state)
        {
            try
            {
                TimeSpan elapsed = DateTime.Now - lastUpdateTS;
                if (elapsed.TotalMinutes >= refreshInterval)
                {
                    // Load value from database     
                    ProcessRecord();
                    lastUpdateTS = DateTime.Now;
                }
                // 10 seconds interval to call the method again..  
                timerObj.Change(10000, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy dữ liệu
        /// </summary>
        private static void ProcessRecord()
        {

        }

        /// <summary>
        /// Load Khu vực
        /// </summary>
        /// <param name="force"></param>
        public static void LoadArea(bool force = false)
        {
            var oBL = new BLArea();
            if (force)
            {
                DsDictionary.Area.Clear();
                DsDictionary.Area.Merge(oBL.Get());
            }
            else
            {
                if (DsDictionary.Area.Rows.Count == 0)
                {
                    DsDictionary.Area.Merge(oBL.Get());
                }
            }
        }

        /// <summary>
        /// Load Khu vực
        /// </summary>
        /// <param name="force"></param>
        public static void LoadOrder(bool force = false, DateTime today = default(DateTime))
        {
            var oBL = new BLOrder();
            if (force)
            {
                DsDictionary.Order.Clear();
                DsDictionary.Order.Merge(oBL.GetOrderByTime(today == default(DateTime) ? DateTime.Now : today));
            }
            else
            {
                if (DsDictionary.Order.Rows.Count == 0)
                {
                    DsDictionary.Order.Merge(oBL.GetOrderByTime(today == default(DateTime) ? DateTime.Now : today));
                }
            }
        }

        /// <summary>
        /// Load Khu vực
        /// </summary>
        /// <param name="force"></param>
        public static void LoadBooking(bool force = false, DateTime today = default(DateTime))
        {
            var oBL = new BLBooking();
            if (force)
            {
                DsDictionary.Booking.Clear();
                DsDictionary.Booking.Merge(oBL.GetBookingByFromTime(today == default(DateTime) ? DateTime.Now : today), false, System.Data.MissingSchemaAction.Ignore);
            }
            else
            {
                if (DsDictionary.Booking.Rows.Count == 0)
                {
                    DsDictionary.Booking.Merge(oBL.GetBookingByFromTime(today == default(DateTime) ? DateTime.Now : today));
                }
            }
        }

        /// <summary>
        /// Load Khách hàng
        /// </summary>
        /// <param name="force"></param>
        public static void LoadCustomer(bool force = false)
        {
            var oBL = new BLCustomer();
            if (force)
            {
                DsDictionary.Customer.Clear();
                DsDictionary.Customer.Merge(oBL.Get());
            }
            else
            {
                if (DsDictionary.Customer.Rows.Count == 0)
                {
                    DsDictionary.Customer.Merge(oBL.Get());
                }
            }
        }

        /// <summary>
        /// Load Món ăn
        /// </summary>
        /// <param name="force"></param>
        public static void LoadInventoryItem(bool force = false)
        {
            var oBL = new BLInventoryItem();
            if (force)
            {
                DsDictionary.InventoryItem.Clear();
                DsDictionary.InventoryItem.Merge(oBL.Get());
            }
            else
            {
                if (DsDictionary.InventoryItem.Rows.Count == 0)
                {
                    DsDictionary.InventoryItem.Merge(oBL.Get());
                }
            }
        }

        /// <summary>
        /// Load Thực đơn
        /// </summary>
        /// <param name="force"></param>
        public static void LoadInventoryItemCategory(bool force = false)
        {
            var oBL = new BLInventoryItemCategory();
            if (force)
            {
                DsDictionary.InventoryItemCategory.Clear();
                DsDictionary.InventoryItemCategory.Merge(oBL.Get());
            }
            else
            {
                if (DsDictionary.InventoryItemCategory.Rows.Count == 0)
                {
                    DsDictionary.InventoryItemCategory.Merge(oBL.Get());
                }
            }
        }

        /// <summary>
        /// Load Thực đơn
        /// </summary>
        /// <param name="force"></param>
        public static void LoadUnit(bool force = false)
        {
            var oBL = new BLUnit();
            if (force)
            {
                DsDictionary.Unit.Clear();
                DsDictionary.Unit.Merge(oBL.Get());
            }
            else
            {
                if (DsDictionary.Unit.Rows.Count == 0)
                {
                    DsDictionary.Unit.Merge(oBL.Get());
                }
            }
        }

        public static void LoadOrderView(bool force = false, DateTime today = default(DateTime))
        {
            var oBL = new BLOrder();
            if (force)
            {
                DsBusiness.OrderView.Clear();
                DsBusiness.OrderView.Merge(oBL.GetOrderByOrderDate(today == default(DateTime) ? DateTime.Now : today));
            }
            else
            {
                if (DsBusiness.OrderView.Rows.Count == 0)
                {
                    DsBusiness.OrderView.Merge(oBL.GetOrderByOrderDate(today == default(DateTime) ? DateTime.Now : today));
                }
            }
        }
    }
}
