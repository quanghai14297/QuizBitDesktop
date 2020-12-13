using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    /// <summary>
    /// Kết quả khi InsertUpdate dữ liệu lên Cloud
    /// </summary>
    public enum EnumResultInsertUpdate
    {
        [Description("Thất bại")]
        Failed = 0,

        [Description("Thành công")]
        Success = 1,

        [Description("Trùng mã")]
        DuplicateCode = 2
    }
}
