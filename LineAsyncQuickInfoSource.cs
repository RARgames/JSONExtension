using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Operations;
using System.Collections.Generic;
using System.Text;
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
            SnapshotPoint? triggerPoint = session.GetTriggerPoint(_textBuffer.CurrentSnapshot);

            if (triggerPoint != null)
            {
                ITextSnapshotLine line = triggerPoint.Value.GetContainingLine();
                ITrackingSpan lineSpan = _textBuffer.CurrentSnapshot.CreateTrackingSpan(line.Extent, SpanTrackingMode.EdgeInclusive);

                string text = triggerPoint.Value.GetContainingLine().GetText(); //get whole line of current cursor pos
                string[] textArray = text.Split('"');
                int partCount = textArray.Length / 2;
                
                if (partCount <= 0)
                {
                    return Task.FromResult<QuickInfoItem>(null); //do not add anything to Quick Info
                }
                if (!JSONExtensionPackage.settings.isLoaded) //if langFile not loaded show ERROR msg in Quick Info
                {
                    return Task.FromResult(new QuickInfoItem(lineSpan, new ContainerElement(ContainerElementStyle.Stacked, new ClassifiedTextElement(new ClassifiedTextRun(PredefinedClassificationTypeNames.Keyword, "JSONEx: Not Loaded! If the problem persist add JSON Path in Tools/JSONEx Settings")))));
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < partCount; i++)
                {
                    string key = textArray[2 * i + 1];
                    if (key.Contains(" "))
                    {
                        continue;
                    }
                    string value = "";
                    if (JSONExtensionPackage.settings.langFile.ContainsKey(key))
                    {
                        value = JSONExtensionPackage.settings.langFile[key];
                    }
                    if (partCount == 1)
                    {
                        sb.Append(value);
                    }
                    else if (i == partCount - 1)
                    {
                        sb.Append($"{key}: {value}");
                    }
                    else
                    {
                        sb.Append($"{key}: {value}\n");
                    }
                }

                return Task.FromResult(new QuickInfoItem(lineSpan, new ContainerElement(ContainerElementStyle.Stacked, new ClassifiedTextElement(new ClassifiedTextRun(PredefinedClassificationTypeNames.Comment, sb.ToString())))));
            }
            return Task.FromResult<QuickInfoItem>(null); //do not add anything to Quick Info
        }

        public void Dispose()
        {
            // This provider does not perform any cleanup.
        }
    }
}