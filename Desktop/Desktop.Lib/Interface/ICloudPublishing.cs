using QuizBit.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Lib
{
    public interface ICloudPublishing
    {
        /// <summary>
        /// Lấy ra thông tin đơn vị tính
        /// </summary>
        /// <returns></returns>
        List<Unit> GetUnitList();
    }
}
