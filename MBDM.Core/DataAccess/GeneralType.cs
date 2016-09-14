using System;

namespace MBDM.Core.DataAccess {
    internal class GeneralType {
        public string AssemblyName { get; set; }
        public Type AssemblyObject { get; set; }

        public GeneralType(string assemblyName, Type assemblyObject) {
            this.AssemblyName = assemblyName;
            this.AssemblyObject = assemblyObject;
        }
    }
}
