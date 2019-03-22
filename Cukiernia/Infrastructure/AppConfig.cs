using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Cukiernia.Infrastructure
{   //clasa ,ktora sluzy do dostawania sie do Webconfig
    
    public class AppConfig
    {  //sciezka wzgledna, pobiera pobranie naszego katalogu IkonyKategoriiFolder z pliku configuracyjnego, z web config
        private static string _ikonyKategoriFolderWzgledny = ConfigurationManager.AppSettings["IkonyKategoriiFolder"];

        public static string IkonyKategoriFolderWzgledny
        {
            get
            {
                return _ikonyKategoriFolderWzgledny;
            }
        }

        private static string _obrazkiFolderWzgledny = ConfigurationManager.AppSettings["ObrazkiFolder"];

        public static string ObrazkiFolderWzgledny
        {
            get
            {
                return _obrazkiFolderWzgledny;
            }
        }
    }
}