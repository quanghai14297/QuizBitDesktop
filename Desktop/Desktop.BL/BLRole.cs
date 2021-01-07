using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.BL
{
    public class BLRole:BLBase
    {
        public BLRole() : base()
        {
            TableMasterName = "Role";
            TableDetailName = "UserJoinRoleID";
            oDL = new DL.DLCustomer(Session.Token);
        }
        public DataTable Get()
        {
            return new DL.DLRole(Session.Token).Get();
        }
    }
}
