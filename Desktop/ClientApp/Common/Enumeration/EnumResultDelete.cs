using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    /// <summary>
    /// Kết quả khi Delete dữ liệu lên Cloud
    /// </summary>
    public enum EnumResultDelete
    {
        [Description("Thất bại")]
        Failed = 0,

        [Description("Thành công")]
        Success = 1,

        [Description("Đã được sử dụng")]
        ItemWasUsed = 2
    }
}
