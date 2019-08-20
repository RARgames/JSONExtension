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

        private bool isLoaded = false;
        private Dictionary<string, string> langFile;
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

            if (!isLoaded)
            {
                string path = Path.Combine(Settings.GetProjectPath(), ".JSONExtensionSettings");
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
                    else
                    {
                        VsShellUtilities.ShowMessageBox(this.package, "Make sure you set it using JSONExtension settings.", "Wrong path to JSON file!", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
                        return;
                    }
                }
                else
                {
                    VsShellUtilities.ShowMessageBox(this.package, "Make sure you set it using JSONExtension settings.", "Cannot find JSON file!", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
                    return;
                }
            }

            DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            string text = string.Empty;
            if (dte.ActiveDocument != null)
            {
                var selection = (TextSelection)dte.ActiveDocument.Selection;
                text = selection.Text;
            }
            if (langFile[text] != null)// "DialogPort.cs-Ethan-BadInput"
            {
                VsShellUtilities.ShowMessageBox(this.package, langFile[text], text, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
            }
        }
    }
}
