using JSONExtension;
using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
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
                string textModified;  //TODO change name to fit what it is
                var textArray = text.Split('"', '"');
                if (textArray.Length >= 2 && !string.IsNullOrEmpty(textArray[1]))
                {
                    textModified = textArray[1];
                }
                else
                {
                    return Task.FromResult<QuickInfoItem>(null);
                }

                if (!isLoaded)
                {
                    string projectPath = JSONExtensionPackage.settings.projectPath;
                    if (!string.IsNullOrEmpty(projectPath))
                    {
                        string path = Path.Combine(projectPath, ".JSONExtensionSettings");

                        if (File.Exists(path))
                        {
                            string json = File.ReadAllText(path);
                            SettingsJSON settings = JsonConvert.DeserializeObject<SettingsJSON>(json); //Using Setting to find the jsonPath parameter declared in the Settings class
                            if (settings.jsonPath.EndsWith(".json"))
                            {
                                string temp = File.ReadAllText(settings.jsonPath);

                                var data = (JObject)JsonConvert.DeserializeObject(temp);
                                var langEN = data["en"].Value<JObject>().ToString();
                                langFile = JsonConvert.DeserializeObject<Dictionary<string, string>>(langEN);

                                isLoaded = true;
                            }
                        }
                    }
                }

                string value;
                if (langFile.ContainsKey(textModified))
                {
                    value = langFile[textModified];
                }
                else
                {
                    return Task.FromResult<QuickInfoItem>(null);
                }

                ContainerElement dataElm;
                if (isLoaded)
                {
                    dataElm = new ContainerElement(
                    ContainerElementStyle.Stacked,
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Keyword, "Key: " + textModified)
                    ),
                    new ClassifiedTextElement(
                        new ClassifiedTextRun(PredefinedClassificationTypeNames.Comment, "Value: " + value)
                    ));
                }
                else
                {
                    dataElm = new ContainerElement(ContainerElementStyle.Stacked, new ClassifiedTextElement(new ClassifiedTextRun(PredefinedClassificationTypeNames.Keyword, "JSON Extension not loaded: Add JSON Path in Tools/JSON Extension Settings - Set JSON Path")));
                }

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