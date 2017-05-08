using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace goatCode.Hubs
{
    public class CodeHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}