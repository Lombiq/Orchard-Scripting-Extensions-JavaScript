using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using OrchardHUN.Scripting.Models;

namespace OrchardHUN.Scripting.JavaScript.Drivers
{
    public class ScriptPartDriver : ContentPartDriver<ScriptPart>
    {
        protected override string Prefix
        {
            get { return "OrchardHUN.Scripting.ScriptPart"; }
        }

        protected override DriverResult Editor(ScriptPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ScriptJs_Edit",
                    () => shapeHelper.EditorTemplate(
                            TemplateName: "Parts.ScriptJs",
                            Model: part,
                            Prefix: Prefix));
        }

        protected override DriverResult Editor(ScriptPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            return Editor(part, shapeHelper);
        }
    }
}