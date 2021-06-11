using System;
using System.Net;

namespace UnityAtoms.Editor
{
    public class Template
    {
        public const string autoGeneratedWarning =
@"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a function.
//
//     Changes to this file may cause incorrect behavior and will be lost
//     if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------";

        public static string Resolve(TemplateData templateData, string className, bool createAssetMenu = true, string methodsString = default) =>
$@"{autoGeneratedWarning}

{templateData.namespaces}
{templateData.withNamespace}
{templateData.openingBrackets}
    /// <summary>
    /// Inherits from {WebUtility.HtmlEncode(templateData.typeName)}.
    /// </summary>
    {templateData.attributes}{(createAssetMenu ? $"[CreateAssetMenu(menuName = \"Atoms{templateData.nestedMenu}\", fileName = \"{className}\")]" : string.Empty)}
    public sealed class {className} : {templateData.typeName}
    {{
        {methodsString}
    }}
{templateData.closingBrackets}
";

        public static string ResolveAtomAsset(Type type, out string className, string withNamespace = default)
        {
            var templateData = TemplateData.Generate(type, out className, withNamespace);
            var content = Resolve(templateData, className);

            return content;
        }

        public static string ResolveAtom(Type type, out string className, string withNamespace = default)
        {
            var templateData = TemplateData.Generate(type, out className, withNamespace);
            var content = Resolve(templateData, className, false);

            return content;
        }
    }
}

