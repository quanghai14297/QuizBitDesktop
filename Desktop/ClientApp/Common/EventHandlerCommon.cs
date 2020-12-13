using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class EventHandlerCommon
    {
        public delegate void EventHandler();

        public static event EventHandler LoadArea;

        public static void HandleLoadArea()
        {
            if (LoadArea != null)
                LoadArea();
        }
    }
}
