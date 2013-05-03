using System;
using System.Collections.Generic;
using System.Linq;
using Noesis.Javascript;
using Orchard;
using Orchard.Environment;
using Orchard.Exceptions;
using OrchardHUN.Scripting.Exceptions;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.JavaScript.Services
{
    public class JavaScriptRuntime : IScriptingRuntime
    {
        private readonly IJavaScriptRuntimeEventHandler _eventHandler;
        private readonly Work<IWorkContextAccessor> _wcaWork;

        private readonly IEngineDescriptor _descriptor = new EngineDescriptor("JS", new Orchard.Localization.LocalizedString("JavaScript"));
        public IEngineDescriptor Descriptor
        {
            get { return _descriptor; }
        }


        public JavaScriptRuntime(IJavaScriptRuntimeEventHandler eventHandler, Work<IWorkContextAccessor> wcaWork)
        {
            _eventHandler = eventHandler;
            _wcaWork = wcaWork;
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

                    context.SetParameter("Factory", new TypeFactory(scope.Assemblies));

                    var workContext = _wcaWork.Value.GetContext();
                    var orchardGlobal = new Dictionary<string, dynamic>();
                    orchardGlobal["WorkContext"] = workContext;
                    orchardGlobal["OrchardServices"] = workContext.Resolve<IOrchardServices>();
                    orchardGlobal["Layout"] = new StaticShape(workContext.Layout);

                    var existing = scope.GetVariable("Orchard");
                    if (existing != null)
                    {
                        if (!(existing is IDictionary<string, object>)) throw new ArgumentException("The Orchard global variable should be an IDictionary<string, dynamic>.");

                        var existingDictionary = existing as IDictionary<string, dynamic>;
                        foreach (var existingItem in existingDictionary)
                        {
                            orchardGlobal[existingItem.Key] = existingItem.Value;
                        }
                    }

                    context.SetParameter("Orchard", orchardGlobal);

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