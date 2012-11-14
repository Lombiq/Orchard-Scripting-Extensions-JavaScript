using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Noesis.Javascript;
using OrchardHUN.Scripting.Exceptions;
using OrchardHUN.Scripting.Services;
using Orchard.Exceptions;

namespace OrchardHUN.Scripting.JavaScript.Services
{
    public class JavaScriptRuntime : IScriptingRuntime
    {
        private readonly IJavaScriptRuntimeEventHandler _eventHandler;

        private readonly IEngineDescriptor _descriptor = new EngineDescriptor("JS", new Orchard.Localization.LocalizedString("JavaScript"));
        public IEngineDescriptor Descriptor
        {
            get { return _descriptor; }
        }


        public JavaScriptRuntime(IJavaScriptRuntimeEventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }


        public dynamic ExecuteExpression(string expression, ScriptScope scope)
        {
            try
            {
                using (var context = new JavascriptContext())
                {
                    foreach (var variable in scope.Variables)
                    {
                        context.SetParameter(variable.Key, variable.Value);
                    }

                    _eventHandler.BeforeExecution(new BeforeJavaScriptExecutionContext(scope, context));
                    var output = context.Run(expression);
                    _eventHandler.AfterExecution(new AfterJavaScriptExecutionContext(scope, context));

                    foreach (var variableName in scope.Variables.Select(kvp => kvp.Key))
                    {
                        scope.SetVariable(variableName, context.GetParameter(variableName));
                    }

                    return output;
                }
            }
            catch (Exception ex)
            {
                if (ex.IsFatal()) throw;

                throw new ScriptRuntimeException("The JavaScript script could not be executed.", ex);
            }
        }
    }
}