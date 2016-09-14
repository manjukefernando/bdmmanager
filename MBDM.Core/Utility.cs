using System.Configuration;

namespace MBDM.Core {
    public sealed class Utility {

        private static readonly string _DeafaultConnectionStringEntry = "DefaultConnectionStringEntry";


        /// <summary>
        /// This function will be used to retrieve details from the application configuration file.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetApplicationSetting(string key) {
                return ConfigurationManager.AppSettings[key];
        }



        public static string GetDefaultConnectionStringId() {
            return GetApplicationSetting(GetDefaultConnectionStringEntry());
        }

        public static string GetDefaultConnectionStringEntry() {
            return _DeafaultConnectionStringEntry;
        }

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //public static string GetCurrentFunctionName() {
        //    var zST = new StackTrace(new StackFrame(1));
        //    return zST.GetFrame(0).GetMethod().ReflectedType.UnderlyingSystemType.FullName + '.' + zST.GetFrame(0).GetMethod().Name;
        //}

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //public static string GetCurrentModuleFileName() {
        //    return "";
        //    //*** To be implemented
        //    //var zST = new StackTrace(new StackFrame(1));
        //    //return zST.GetFrame(0).GetFileName();
        //}
    }
}
