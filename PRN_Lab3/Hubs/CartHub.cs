using Microsoft.AspNetCore.SignalR;

namespace PRN_Lab3.Hubs
{
    public class CartHub : Hub
    {
        public async Task AddCart()
        {
            await Clients.All.SendAsync("CartAdded");
        }
    }
}
