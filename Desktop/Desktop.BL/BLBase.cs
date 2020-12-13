using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.DL;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLBase
    {
        public string TableMasterName = "";

        public string TableDetailName = "";

        protected DLBase oDL;

        public BLBase()
        {
            if (oDL == null)
                oDL = new DLBase();
        }

        /// <summary>
        /// Phương thức thêm/cập nhật mới đối tượng
        /// </summary>
        /// <typeparam name="T">loại đối tượng</typeparam>
        /// <param name="obj">đối tượng cần thêm</param>
        /// <returns>thêm/cập nhật đối tượng thành công/thất bại</returns>
        public int InsertUpdateObject<T>(T obj)
        {
            return oDL.InsertUpdateObject(obj);
        }

        /// <summary>
        /// Cập nhật dữ liệu chính
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public int InsertUpdate(DataRow row)
        {
            return oDL.InsertUpdate(row);
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="ds"></param>
        public bool InsertUpdate(DictionaryDataSet ds, int positionMaster = 0)
        {
            return oDL.InsertUpdateDataMasterDetail(ds.Tables[TableMasterName].Rows[positionMaster], ds.Tables[TableDetailName]);
        }

        /// <summary>
        /// Lấy ra toàn bộ Master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetAll()
        {
            return oDL.GetAll();
        }

        /// <summary>
        /// Lấy ra Master
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataByID(Guid id)
        {
            return oDL.GetDataByID(id);
        }

        /// <summary>
        /// Lấy ra Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetDataDetailByID(Guid id)
        {
            return oDL.GetDataDetailByID(id);
        }
    }
}
