using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace JSONExtension
{
    public class Settings
    {
        public string projectPath;
        public Dictionary<string, string> langFile; //contains keys and values of language file
        public bool isLoaded = false; //flag after loading langFile set to true

        public void Initialize() //called at the start by JSONExtensionPackage
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            projectPath = GetProjectPath();
            LoadLangFile();
        }

        private string GetProjectPath()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsSolution solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution; //get solution reference from a service provider
            if (solution != null)
            {
                solution.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object open);
                bool isOpen = (bool)open; //if the solution open
                if (isOpen)
                {
                    solution.GetSolutionInfo(out string dir, out string file, out string ops);  //dir contains the solution's directory path
                    return dir;
                }
            }
            return null;
        }

        public void LoadLangFile(bool verbose = false) //load language file
        {
            if (!isLoaded) //if not loaded
            {
                if (!string.IsNullOrEmpty(projectPath)) //check if valid project path
                {
                    string path = Path.Combine(projectPath, ".JSONExtensionSettings");

                    if (File.Exists(path)) //if file exists at this path
                    {
                        string json = File.ReadAllText(path);
                        SettingsJSON settingsJSON = JsonConvert.DeserializeObject<SettingsJSON>(json); //Using JsonCOnvert to deserialize and check jsonPath
                        if (settingsJSON.jsonPath.EndsWith(".json")) //if jsonPath ends with .json, read language file and transform en localization into langFile dictionary
                        {
                            string temp = File.ReadAllText(settingsJSON.jsonPath);

                            var data = (JObject)JsonConvert.DeserializeObject(temp);
                            var langEN = data["en"].Value<JObject>().ToString();
                            langFile = JsonConvert.DeserializeObject<Dictionary<string, string>>(langEN);

                            isLoaded = true;
                        }
                        else if (verbose)
                        {
#pragma warning disable
                            ShowMessageAndStopExecution("JSON Extension: JSON Path not valid!", "Make sure to set it using JSON Extension settings. The file should end with .json and all keys/values should be in en array.");
#pragma warning restore
                        }
                    }
                    else if (verbose)
                    {
                        ShowMessageAndStopExecution("JSON Extension: Path not set!", "Make sure to set it using JSON Extension settings in Tools menu.");
                    }
                }
                else if (verbose)
                {
                    ShowMessageAndStopExecution("JSON Extension: Error getting project path!", "Please report this issue to the developer.");
                }
            }
        }

        public void ShowMessageAndStopExecution(string title, string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            VsShellUtilities.ShowMessageBox(ServiceProvider.GlobalProvider, message, title, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
            return;
        }
    }

    public class SettingsJSON //structure for JSON serialization
    {
        public string jsonPath;
    }
}

//TODO allow to create a new key and value if not found
//TODO implement edit window

//TODO modify key binding at JSONExtension\JSONExtensionPackage.vsct
//TODO check if JSONExtensionPackage.settings.LoadLangFile(); is needed everywhere