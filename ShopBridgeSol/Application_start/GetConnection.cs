using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeSol.Application_start
{
    public class GetConnection
    {
        private static readonly Lazy<GetConnection> _lazy =
        new Lazy<GetConnection>(() => new GetConnection());
        private GetConnection()
        {

            Console.WriteLine("Instance created");
        }
        public static GetConnection Instance
        {
            get
            {
                return _lazy.Value;
            }
        }


        private IConfigurationRoot config;


        private IConfigurationRoot Config
        {
            get
            {
                if (config == null)
                    config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                return config;
            }
        }

        public string GetConnectionString(string DBType)
        {

            string connectionString = "";
            if (DBType.ToUpper() == "SqlServerDB".ToUpper())
            {

                connectionString = Config["ConnectionString:DBConnection"];
            }
            return connectionString.ToString();

        }

    }
}
