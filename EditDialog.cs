using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Task = System.Threading.Tasks.Task;
using Microsoft.VisualStudio.Text;
using System.Windows.Forms;
using System.Text;
using Microsoft.VisualStudio.TextManager.Interop;

namespace JSONExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class EditDialog
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 4129;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("50c3e0cd-6afa-4509-98ec-6fc50c8df2f1");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditDialog"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private EditDialog(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static EditDialog Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in EditDialog's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new EditDialog(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            JSONExtensionPackage.settings.LoadLangFile(); //if not loaded try to load language file into settings

            //TODO implement
            //DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            //string text = string.Empty;
            //if (dte.ActiveDocument != null)
            //{
            //    var selection = (TextSelection)dte.ActiveDocument.Selection;
            //    // text = selection.Text;
            //   // text = dte.ActiveDocument.poin
            //    VsShellUtilities.ShowMessageBox(this.package, "", "", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
            //}


            //          DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;

            //  EnvDTE80.DTE2 dte2;
            //  dte2 = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            //  dte2.MainWindow.Activate();
            //    int line = ((EnvDTE.TextSelection)dte2.ActiveDocument.Selection).ActivePoint.Line;
            //            int line = ((EnvDTE.TextSelection)dte.ActiveDocument.Selection).ActivePoint.Line;
            var service = Package.GetGlobalService(typeof(SVsTextManager));
            var textManager = service as IVsTextManager2;
            IVsTextView view;
            int result = textManager.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out view);

            view.GetSelection(out int startLine, out int startColumn, out int endLine, out int endColumn);//end could be before beginning
          //  var start = new TextViewPosition(startLine, startColumn);
         //   var end = new TextViewPosition(endLine, endColumn);

            view.GetSelectedText(out string selectedText);
//
       //     TextViewSelection selection = new TextViewSelection(start, end, selectedText);
      //      return selection;
            MessageBox.Show(selectedText);
            //VsShellUtilities.ShowMessageBox(this.package, "", "", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
        }
    }
}
