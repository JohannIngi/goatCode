﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace goatCode.Hubs
{
    public class CodeHub : Hub
    {
        public void JoinDocument(int documentID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(documentID));
        }
        public void OnChange(object changeData, int documentID)
        {
            Clients.Group(Convert.ToString(documentID)).OnChange(changeData);
            //Clients.All.OnChange(changeData);
        }
    }
}