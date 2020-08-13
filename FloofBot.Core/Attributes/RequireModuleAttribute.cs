using System;

namespace FloofBot.Core.Attributes
{
    public class RequireModuleAttribute : Attribute
    {
        public string ModuleName { get; }

        public RequireModuleAttribute(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}