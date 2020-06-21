using Newtonsoft.Json;
using Shared.ClassExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Config
{
    public class DbConnectionConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string SSPI { get; set; }

        public string Password { get; set; }


        public string GetConnectionString()
        {
            string conn;

            conn = string.Format("data source={0}; initial catalog={1}; persist security info=True; user id={2}; password={3}; MultipleActiveResultSets=True; App=EntityFrameworkCore",
                Server, Database, User, Password);

            if (!string.IsNullOrEmpty(SSPI))
                if (SSPI.DynamicConvert<bool>())
                    conn += ";Integrated Security=SSPI";

            return conn;
        }
    }
}
