using Microsoft.Owin;
using Owin;
using ShoppingList;

[assembly: OwinStartup(typeof(Startup))]

namespace ShoppingList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
