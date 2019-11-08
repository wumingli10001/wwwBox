using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace wwwApi_S.App_Start
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("AppHost",typeof(string).Assembly) { }

        public override void Configure(Container container)
        {
            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;

            var connectionFactory = new OrmLiteConnectionFactory();

            connectionFactory.RegisterConnection("","",MySqlDialect.Provider);

            container.Register<IDbConnectionFactory>(connectionFactory);

        }
    }
}
