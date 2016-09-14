using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MBDM.Core.DataAccess.Provider;

namespace MBDM.Core.DataAccess {
    public class ProviderFactory {
        //private static volatile ProviderFactory _instance;
        private static object syncroot = new object();

        private static List<GeneralType> _Providers = new List<GeneralType>();



        public ProviderFactory() {
            LoadProviders();
        }

        /*
        public static ProviderFactory CreateInstance {
            get {
                if (_instance == null) {
                    lock (syncroot) {
                        if (_instance == null) {
                            _instance = new ProviderFactory();
                        }
                    }
                }
                return _instance;
            }
        }
        */
        public static IProvider CreateProvider(string provider) {
            IProvider iProvider = null;
            if (_Providers.Count == 0) {
                LoadProviders();
            }

            foreach (GeneralType t in _Providers) {
                if (t.AssemblyName.ToUpper().Equals(provider.ToUpper())) {
                    var type = t.AssemblyObject;
                    iProvider = (IProvider)Activator.CreateInstance(type);
                    break;
                }
            }

            return iProvider;
        }

        private static void LoadProviders() {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var t in types) {
                if (t.GetInterface("IProvider") != null) {
                    _Providers.Add(new GeneralType(t.Name, t));
                }
            }
        }

        /// <summary>
        /// Gets the provider from the given connection string key
        /// </summary>
        /// <param name="connectionStringId"></param>
        /// <returns>String containing the provider</returns>
        public static string GetProvider(string connectionStringId) {
                return ConfigurationManager.ConnectionStrings[connectionStringId].ProviderName;
        }

        /// <summary>
        /// Gets the provider from the default connection string 
        /// </summary>
        /// <returns>String containing the provider</returns>
        public static string GetProvider() {
                /*
                 if (defConString == null) {
                    defConString = Utility.GetDefaultConnectionStringId();
                }
                */
                var defConString = Utility.GetApplicationSetting(Utility.GetDefaultConnectionStringEntry()) ??
                                   Utility.GetDefaultConnectionStringId();
                return ConfigurationManager.ConnectionStrings[defConString].ProviderName;
           
        }
    }
}
