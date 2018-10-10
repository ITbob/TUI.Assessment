using TUI.Ado.Entity.Source.Repositories;
using TUI.Ado.Entity.Source.Session;
using TUI.Data.Access.Source.Session;
using TUI.Data.Access.Source.Unit;
using TUI.Login.source;

namespace TUI.Ado.Entity.Source.Unit
{
    public class UserUnit : IUnit<User>
    {
        public ISession<User> GetSession()
        {
            return new TuiContextStringLessSession<User>(new UserRepository());
        }
    }
}
