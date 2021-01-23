using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Interfaces;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient
    {
        public UsersClient(IConfiguration Configuration) : base(Configuration, WebAPI.Identity.User) { }
    }
}
