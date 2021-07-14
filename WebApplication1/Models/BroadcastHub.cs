using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class BroadcastHub : Hub<IHubClient>
    {
        //public async Task NewMessage(Message msg)
        //{
        //    await Clients.All.SendAsync("MessageReceived", msg);
        //}
        //public async Task NewMessage()
        //{
        //    await Clients.All.SendAsync("MessageReceived");
        //}
    }
}
