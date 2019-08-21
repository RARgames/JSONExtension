using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace JSONExtension
{
    [Export(typeof(IAsyncQuickInfoSourceProvider))]
    [Name("Line Async Quick Info Provider")]
    [ContentType("any")]
    [Order]
    internal sealed class LineAsyncQuickInfoSourceProvider : IAsyncQuickInfoSourceProvider
    {
        public IAsyncQuickInfoSource TryCreateQuickInfoSource(ITextBuffer textBuffer) //creates instance of LineAsyncQuickInfoSource for displaying Quick Info
        {  
            return textBuffer.Properties.GetOrCreateSingletonProperty(() => new LineAsyncQuickInfoSource(textBuffer)); //this ensures only one instance per textbuffer is created
        }
    }
}
