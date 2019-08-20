using JSONExtension;
using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
//using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using Microsoft.VisualStudio.Threading;

namespace JSONExtension
{
    internal sealed class LineAsyncQuickInfoSource : IAsyncQuickInfoSource
    {
        private ITextBuffer _textBuffer;
        private bool isLoaded = false;
        private Dictionary<string, string> langFile;

        public LineAsyncQuickInfoSource(ITextBuffer textBuffer)
        {
            _textBuffer = textBuffer;
        }

        // This is called on a background thread.
        public Task<QuickInfoItem> GetQuickInfoItemAsync(IAsyncQuickInfoSession session, CancellationToken cancellationToken)
        {
            var triggerPoint = session.GetTriggerPoint(_textBuffer.CurrentSnapshot);

            if (triggerPoint != null)
            {
                var line = triggerPoint.Value.GetContainingLine();
                var lineSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(line.Extent, SpanTrackingMode.EdgeInclusive);
                var text = triggerPoint.Value.GetContainingLine().GetText();
                //string textModified;
                //try
                //{
                //    textModified = text.Split('"', '"')[1];
                //}
                //catch
                //{
                //    return Task.FromResult<QuickInfoItem>(null);
                //}

                //if (string.IsNullOrEmpty(textModified))
                //    return Task.FromResult<QuickInfoItem>(null);

                //searching string in JSON file
                //if (!isLoaded)
                //{

                //    //  JoinableTaskFactory.SwitchToMainThreadAsync();
                //    string path = Path.Combine(Settings.GetProjectPath(), ".JSONExtensionSettings");

                //    if (File.Exists(path))
                //    {
                //        string json = File.ReadAllText(path);
                //        SettingsJSON settings = JsonConvert.DeserializeObject<SettingsJSON>(json); //Using Setting to find the jsonPath parameter declared in the Settings class
                //        if (settings.jsonPath.EndsWith(".json"))
                //        {
                //            string temp = File.ReadAllText(settings.jsonPath);


                //            var data = (JObject)JsonConvert.DeserializeObject(temp);
                //            var langEN = data["en"].Value<JObject>().ToString();
                //            langFile = JsonConvert.DeserializeObject<Dictionary<string, string>>(langEN);

                //            isLoaded = true;
                //        }
                //        else
                //        {
                //            //VsShellUtilities.ShowMessageBox(this.package, "Make sure you set it using JSONExtension settings.", "Wrong path to JSON file!", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
                //            //return;
                //            //TODO change key/value to file not loaded
                //        }
                //    }
                //    else
                //    {
                //        //VsShellUtilities.ShowMessageBox(this.package, "Make sure you set it using JSONExtension settings.", "Cannot find JSON file!", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
                //        //return;
                //        //TODO change key/value to file not loaded
                //    }
                //}

                // DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                // string text = string.Empty;
                //if (dte.ActiveDocument != null)
                //{
                //    var selection = (TextSelection)dte.ActiveDocument.Selection;
                //    text = selection.Text;
                //}
                // string value = string.Empty;
                //try
                //{
                //    if (langFile[textModified] != null)// "DialogPort.cs-Ethan-BadInput"
                //    {
                //        // VsShellUtilities.ShowMessageBox(this.package, langFile[text], text, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
                //        value = langFile[text];
                //    }
                //}
                //catch
                //{
                //    return Task.FromResult<QuickInfoItem>(null);
                //}
                ////



                var dataElm = new ContainerElement(
                    ContainerElementStyle.Stacked,
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Keyword, "Key: ")//+ textModified)
                    ),
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Comment, "Value: ")//+ value)
                    ));

                return Task.FromResult(new QuickInfoItem(lineSpan, dataElm));
            }

            return Task.FromResult<QuickInfoItem>(null);
        }
        public void Dispose()
        {
            // This provider does not perform any cleanup.
        }
    }
}