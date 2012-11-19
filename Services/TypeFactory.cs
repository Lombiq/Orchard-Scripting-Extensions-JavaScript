using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using OrchardHUN.Scripting.Exceptions;

namespace OrchardHUN.Scripting.JavaScript.Services
{
    public class TypeFactory
    {
        private readonly Dictionary<string, Type> _types;


        public TypeFactory(IEnumerable<Assembly> assemblies)
        {
            _types = new Dictionary<string, Type>();

            // .ToDictionary() doesn't work as there can be duplicate full names (but how?)
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            foreach (var type in types)
            {
                _types[type.FullName] = type;
            }
        }


        public object Create(string typeName, object[] ctorArguments = null)
        {
            if (String.IsNullOrEmpty(typeName) || !_types.ContainsKey(typeName))
                throw new ScriptRuntimeException("The type " + typeName + " was not found in any assembly loaded into the script scope.");

            return Activator.CreateInstance(_types[typeName], ctorArguments);
        }
    }
}