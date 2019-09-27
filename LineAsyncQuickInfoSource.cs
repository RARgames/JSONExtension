using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using System.Threading;
using System.Threading.Tasks;

namespace JSONExtension
{
    internal sealed class LineAsyncQuickInfoSource : IAsyncQuickInfoSource
    {
        private ITextBuffer _textBuffer;

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
                var text = triggerPoint.Value.GetContainingLine().GetText(); //get whole line of current cursor pos

                string key;
                var textArray = text.Split('"', '"');
                if (textArray.Length >= 2 && !string.IsNullOrEmpty(textArray[1])) //if there is char between "" set key to it
                {
                    key = textArray[1];
                }
                else
                {
                    return Task.FromResult<QuickInfoItem>(null); //do not add anything to Quick Info
                }
#pragma warning disable
                JSONExtensionPackage.settings.LoadLangFile(); //if not loaded try to load language file into settings
#pragma warning restore
                if (!JSONExtensionPackage.settings.isLoaded) //if langFile not loaded show ERROR msg in Quick Info
                {
                    return Task.FromResult(new QuickInfoItem(lineSpan, new ContainerElement(ContainerElementStyle.Stacked, new ClassifiedTextElement(new ClassifiedTextRun(PredefinedClassificationTypeNames.Keyword, "JSON Extension: Not Loaded! If the problem persist add JSON Path in Tools/JSON Extension Settings")))));
                }

                if (JSONExtensionPackage.settings.langFile.ContainsKey(key)) //if langFile contains key, get it's value 
                {
                    return Task.FromResult(new QuickInfoItem(lineSpan, new ContainerElement(ContainerElementStyle.Stacked, new ClassifiedTextElement(new ClassifiedTextRun(PredefinedClassificationTypeNames.Comment, JSONExtensionPackage.settings.langFile[key]))))); //add key's value to quick info
                }
                else
                {
                    return Task.FromResult<QuickInfoItem>(null); //do not add anything to Quick Info
                }
            }
            return Task.FromResult<QuickInfoItem>(null); //do not add anything to Quick Info
        }
        public void Dispose()
        {
            // This provider does not perform any cleanup.
        }
    }
}