﻿using System;
using System.ComponentModel.Design;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Task = System.Threading.Tasks.Task;

namespace JSONExtension
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class OpenSettings
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("50c3e0cd-6afa-4509-98ec-6fc50c8df2fa");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenSettings"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private OpenSettings(AsyncPackage package, OleMenuCommandService commandService)
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
        public static OpenSettings Instance
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
            // Switch to the main thread - the call to AddCommand in OpenSettings's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new OpenSettings(package, commandService);
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

            string jsonFilePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog(); //open file explorer
            bool jsonPathSet = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if OK clicked save path and set flag
            {
                jsonFilePath = openFileDialog.FileName;
                jsonPathSet = true;
            }

            JSchema schemanet = JSchema.Parse(@"
            {
                'type': 'object',
                'required': ['en'],
                'properties': {
                'en': {
                    'type': 'object',
                    'patternProperties': {
                     '^[a-zA-Z0-9-_]*$': { 'type':'string'}
                     }
                 }
               }
             }
            ");

            JObject jsonToVerify = JObject.Parse(File.ReadAllText(jsonFilePath));
            bool valid = jsonToVerify.IsValid(schemanet);
            if (!valid)
            {
                MessageBox.Show("Error selecting JSON file\nFile does not mathc schema.", "JSONEx");
                return;
            }

            string projectPath = JSONExtensionPackage.settings.projectPath; //get project path from settings

            if (projectPath != null && jsonPathSet) //if project path exists and jsonPathSet flag is true, save settingsJSON and show SUCCESS msg
            {
                SettingsJSON settingsJSON = new SettingsJSON
                {
                    jsonPath = jsonFilePath
                };
                File.WriteAllText(Path.Combine(projectPath, ".JSONExtensionSettings"), JsonConvert.SerializeObject(settingsJSON));

                JSONExtensionPackage.settings.LoadLangFile(true); //force reload lang file

                MessageBox.Show("JSON Path set successfully\nProject Path: " + projectPath + "\nJSON Path: " + jsonFilePath, "JSONEx");
            }
            else //otherwise show ERROR msg
            {
                MessageBox.Show("Error selecting JSON file\n1. Open project for which you want to set JSON file.\n2. Select valid JSON.", "JSONEx");
            }
        }
    }
}
