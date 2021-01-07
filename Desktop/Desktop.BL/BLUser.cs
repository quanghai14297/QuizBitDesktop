using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Entity;

namespace Desktop.BL
{
    public class BLUser : BLBase
    {
        public BLUser() : base()
        {
            TableMasterName = "Customer";
            TableDetailName = "";
            oDL = new DL.DLUser(Session.Token);
        }
        public DataTable Get()
        {
            return new DL.DLUser(Session.Token).Get();
        }

    }
}
