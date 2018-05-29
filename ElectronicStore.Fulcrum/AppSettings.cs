using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Fulcrum
{
    public class AppSettings
    {
        public AppSettings()
        {

        }
        public string SessionCart = "CART_SESSION";

        public string FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();

        public string FromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();

        public string FromDisplayName = ConfigurationManager.AppSettings["FromDisplayName"].ToString();

        public string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();

        public string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

        public string EnabledSSL = ConfigurationManager.AppSettings["EnabledSSL"].ToString();

        public string BaseUrl = ConfigurationManager.AppSettings["BaseUrl"].ToString();

    }
}
