
using System.Data.Entity;
using System.Linq;
using TUI.Login.source;

namespace TUI.Ado.Entity.Source.Repositories
{
    internal class UserRepository : TuiContextRepository<User>
    {
        protected override DbSet<User> GetDb()
        {
            return this.Context.Users;
        }

        protected override IQueryable<User> GetQueryable()
        {
            return this.Context.Users;
        }

    }
}
