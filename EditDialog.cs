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
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;

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

            if (!JSONExtensionPackage.settings.isLoaded) //if langFile not loaded show ERROR msg in Quick Info
            {
                MessageBox.Show("Not Loaded!\nIf the problem persist add JSON Path in Tools/JSON Extension Settings", "JSON Extension");
                return;
            }

            DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;

            var activePoint = ((EnvDTE.TextSelection)dte.ActiveDocument.Selection).ActivePoint;
            string text = activePoint.CreateEditPoint().GetLines(activePoint.Line, activePoint.Line + 1);

            string key;
            var textArray = text.Split('"', '"');
            if (textArray.Length >= 2 && !string.IsNullOrEmpty(textArray[1])) //if there is char between "" set key to it
            {
                key = textArray[1];

                if (JSONExtensionPackage.settings.langFile.ContainsKey(key)) //if langFile contains key, get it's value 
                {
                    var form = new EditWindow(key, JSONExtensionPackage.settings.langFile[key]);
                    form.ShowDialog();
                }
                else
                {
                    //TODO if not ask to create and edit
                }
            }
            else
            {
                MessageBox.Show("Key does not exist in this line.\nExample: \"key\"", "JSON Extension");
                return;
            }
        }
    }
}

//TODO check if JSONExtensionPackage.settings.LoadLangFile(); is needed everywhere
//TODO check for correctness of given json