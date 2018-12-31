using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Hubs;
using System.Text;
using System.Threading.Tasks;

namespace airQ.Hubs
{
    public class dashboardHub : Hub
    {
        public void Connect(string userName)
        {
            Client client = new Client
            {
                userName = userName,
                ID = Context.ConnectionId
            };
            
        }
        public async void publishToUsers(string Data)
        {
            Clients.All.updateInfo(Data);
        }
    }
    public class Client
    {
        public string userName { get; set; }
        public string ID { get; set; }
    }
}