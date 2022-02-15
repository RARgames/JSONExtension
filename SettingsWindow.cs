using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONExtension
{
    public partial class SettingsWindow : Form
    {
        public SettingsWindow(string oldLanguageCode = "", string oldLanguagePath = "")
        {
            InitializeComponent();

            languageCodeText.Text = oldLanguageCode;
            languagePathText.Text = oldLanguagePath;

            this.Cursor = new Cursor(Cursor.Current.Handle);
            this.Location = Cursor.Position; //move window to the cursor location
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            string languageCode = languageCodeText.Text;
            string languagePath = languagePathText.Text;
            string projectPath = JSONExtensionPackage.settings.projectPath; //get project path from settings

            if (!string.IsNullOrEmpty(projectPath) && !string.IsNullOrEmpty(languageCode) && !string.IsNullOrEmpty(languagePath))
            {
                SettingsJSON settingsJSON = new SettingsJSON
                {
                    languageCode = languageCode,
                    languagePath = languagePath
                };
                File.WriteAllText(Path.Combine(projectPath, ".JSONExSettings"), JsonConvert.SerializeObject(settingsJSON));
                JSONExtensionPackage.settings.LoadLangFile(true); //force reload lang file
                Dispose();
            }
            else
            {
                MessageBox.Show("Error selecting JSON file\n1. Open project for which you want to set JSON file.\n2. Select valid JSON.", "JSONEx");
            }
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //open file explorer
            if (openFileDialog.ShowDialog() == DialogResult.OK) //if OK clicked save path to text box
            {
                languagePathText.Text = openFileDialog.FileName;
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

            JObject jsonToVerify = JObject.Parse(File.ReadAllText(languagePathText.Text));
            bool valid = jsonToVerify.IsValid(schemanet);
            if (!valid)
            {
                MessageBox.Show("Error selecting JSON file\nFile does not match schema.", "JSONEx");
                return;
            }
        }
    }
}
