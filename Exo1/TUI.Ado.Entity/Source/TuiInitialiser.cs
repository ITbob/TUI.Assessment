using InteractivePreGeneratedViews;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUI.Data.Access.Source;

namespace TUI.Ado.Entity.Source
{
    public class TuiInitialiser : IInitialiser
    {
        public void Initialise()
        {
            Database.SetInitializer(new TuiInitializer());
            this.SetCache();
        }

        private void SetCache()
        {
            using (var ctx = new TuiContext())
            {
                try
                {
                    InteractiveViews
                        .SetViewCacheFactory(
                            ctx,
                            new SqlServerViewCacheFactory(ctx.Database.Connection.ConnectionString));
                }
                catch (Exception)
                {
                    Debug.WriteLine("not available");
                }
            }
        }
    }
}
