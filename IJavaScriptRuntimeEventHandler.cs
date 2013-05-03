using Noesis.Javascript;
using Orchard.Events;
using OrchardHUN.Scripting.Services;

namespace OrchardHUN.Scripting.JavaScript
{
    public interface IJavaScriptRuntimeEventHandler : IEventHandler
    {
        void BeforeExecution(BeforeJavaScriptExecutionContext context);
        void AfterExecution(AfterJavaScriptExecutionContext context);
    }

    public abstract class JavaScriptScriptingEventContext
    {
        public ScriptScope Scope { get; set; }
        public JavascriptContext Context { get; private set; }

        protected JavaScriptScriptingEventContext(ScriptScope scope, JavascriptContext context)
        {
            Scope = scope;
            Context = context;
        }
    }

    public class BeforeJavaScriptExecutionContext : JavaScriptScriptingEventContext
    {
        public BeforeJavaScriptExecutionContext(ScriptScope scope, JavascriptContext context)
            : base(scope, context)
        {
        }
    }

    public class AfterJavaScriptExecutionContext : JavaScriptScriptingEventContext
    {
        public AfterJavaScriptExecutionContext(ScriptScope scope, JavascriptContext context)
            : base(scope, context)
        {
        }
    }
}
