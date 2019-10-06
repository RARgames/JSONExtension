using EnvDTE;
using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TextManager.Interop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JSONExtension
{
    public class Settings
    {
        public string projectPath;
        public Dictionary<string, string> langFile; //contains keys and values of language file
        public bool isLoaded = false; //flag after loading langFile set to true

        private JObject data;
        private string jsonPath;

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

        public void LoadLangFile(bool forceReload = false, bool verbose = false) //load language file
        {
            if (forceReload)
                isLoaded = false;
            if (!isLoaded) //if not loaded
            {
                if (!string.IsNullOrEmpty(projectPath)) //check if valid project path
                {
                    string path = Path.Combine(projectPath, ".JSONExtensionSettings");

                    if (File.Exists(path)) //if file exists at this path
                    {
                        string json = File.ReadAllText(path);
                        SettingsJSON settingsJSON = JsonConvert.DeserializeObject<SettingsJSON>(json); //Using JsonCOnvert to deserialize and check jsonPath
                        jsonPath = settingsJSON.jsonPath;
                        string temp = File.ReadAllText(jsonPath);

                        data = (JObject)JsonConvert.DeserializeObject(temp);
                        var langEN = data["en"].Value<JObject>().ToString();
                        langFile = JsonConvert.DeserializeObject<Dictionary<string, string>>(langEN);

                        isLoaded = true;
                    }
                    else if (verbose)
                    {
#pragma warning disable
                        ShowMessageAndStopExecution("Path not set!\nMake sure to set it using JSONEx settings in Tools menu.");
#pragma warning enable
                    }
                }
                else if (verbose)
                {
                    ShowMessageAndStopExecution("Error getting project path!\nOpen the project first.");
                }
            }
        }

        public void EditEntry(string oldKey, string newKey, string oldValue, string newValue)
        {
            ThreadHelper.JoinableTaskFactory.Run(async delegate
            { //switch to main thread
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                if (string.IsNullOrEmpty(newKey) || string.IsNullOrEmpty(newValue))
                {
                    MessageBox.Show("Key edit failed!\nNew key/value cannot be empty.", "JSONEx");
                    return;
                }

                if (isLoaded)
                {
                    if (string.IsNullOrEmpty(oldKey)) //if there was no key (key input in dialog)
                    {
                        langFile.Add(newKey, newValue); //create new key and value in json
                        Save();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(oldValue)) //if there was a key before a dialog, but it had no value - therefore it was not available in .json
                        {
                            if ((string.Compare(oldKey, newKey) == 0) || ((string.Compare(oldKey, newKey) != 0) && !langFile.ContainsKey(newKey)))
                            {
                                langFile.Add(newKey, newValue); //create new key and value in json
                                Save();
                                if (string.Compare(oldKey, newKey) != 0) //if key has changed
                                {
                                    DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                                    dte.Find.FindReplace(vsFindAction.vsFindActionReplaceAll, "\"" + oldKey + "\"", (int)vsFindOptions.vsFindOptionsKeepModifiedDocumentsOpen | (int)vsFindOptions.vsFindOptionsMatchCase, "\"" + newKey + "\"", vsFindTarget.vsFindTargetCurrentProject);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Key edit failed!\nYou cannot rename a key to existing key.", "JSONEx");
                                return;
                            }
                        }
                        else //if there was a key and it had a value
                        {
                            if (langFile.ContainsKey(oldKey)) //extra precaucion
                            {
                                if ((string.Compare(oldKey, newKey) == 0) || ((string.Compare(oldKey, newKey) != 0) && !langFile.ContainsKey(newKey)))
                                {
                                    langFile.Remove(oldKey); //remove old key
                                    langFile.Add(newKey, newValue); //create new key and value in json
                                    Save();
                                    if (string.Compare(oldKey, newKey) != 0) //if key has changed
                                    {
                                        DTE dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                                        dte.Find.FindReplace(vsFindAction.vsFindActionReplaceAll, "\"" + oldKey + "\"", (int)vsFindOptions.vsFindOptionsKeepModifiedDocumentsOpen | (int)vsFindOptions.vsFindOptionsMatchCase, "\"" + newKey + "\"", vsFindTarget.vsFindTargetCurrentProject);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Key edit failed!\nYou cannot rename a key to existing key.", "JSONEx");
                                    return;
                                }
                            }
                        }
                    }
                }
            });
        }

        public void Save()
        {
            if (!isLoaded)
            {
                return;
            }
            data["en"] = JToken.FromObject(langFile);
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }

        private void ShowMessageAndStopExecution(string message)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            MessageBox.Show(message, "JSONEx");
            return;
        }
    }

    public class SettingsJSON //structure for JSON serialization
    {
        public string jsonPath;
    }
}