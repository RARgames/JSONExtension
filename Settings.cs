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
                        if (settingsJSON.jsonPath.EndsWith(".json")) //if jsonPath ends with .json, read language file and transform en localization into langFile dictionary
                        {
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
                            ShowMessageAndStopExecution("JSON Path not valid!\nMake sure to set it using JSON Extension settings. The file should end with .json and all keys/values should be in en array.");
#pragma warning restore
                        }
                    }
                    else if (verbose)
                    {
                        ShowMessageAndStopExecution("Path not set!\nMake sure to set it using JSON Extension settings in Tools menu.");
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
            if (isLoaded)
            {
                if (string.IsNullOrEmpty(oldKey)) //if there was no key (key input in dialog)
                {
                    langFile.Add(newKey, newValue); //create new key and value in json
                }
                else
                {
                    if (string.IsNullOrEmpty(oldValue)) //if there was a key before a dialog, but it had no value - therefore it was not available in .json
                    {
                        langFile.Add(newKey, newValue); //create new key and value in json
                        if (string.Compare(oldKey, newKey) != 0) //if key has changed
                        {
                            //TODO do not allow key replacement to existing key
                            //TODO Implement Replace(oldKey, newKey) in the whole solution
                        }
                    }
                    else //if there was a key and it had a value
                    {//TODO check if replacing in whole solution replaces in .json too, if so fix it
                        if (langFile.ContainsKey(oldKey)) //extra precaucion
                        {
                            //TODO do not allow key replacement to existing key
                            langFile.Remove(oldKey); //remove old key
                            langFile.Add(newKey, newValue); //create new key and value in json
                            if (string.Compare(oldKey, newKey) != 0) //if key has changed
                            {
                                //TODO Implement Replace(oldKey, newKey) in the whole solution

                                //IVsFindTarget.Replace
                                //IVsFindHelper pHelper = Package.GetGlobalService(typeof(IVsFindHelper)) as IVsFindHelper;
                                //ptarget.Replace(oldKey, newKey, (uint)__VSFINDOPTIONS.FR_Solution | (uint)__VSFINDOPTIONS.FR_ReplaceAll | (uint)__VSFINDOPTIONS.FR_MatchCase, 1, pHelper, out int pfReplaced);
                                //MessageBox.Show(pfReplaced.ToString());
                                //__VSFINDOPTIONS.FR_Solution | __VSFINDOPTIONS.FR_ReplaceAll | __VSFINDOPTIONS.FR_MatchCase
                            }
                        }
                    }
                }
                Save();
            }
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
            MessageBox.Show(message, "JSON Extension");
            return;
        }
    }

    public class SettingsJSON //structure for JSON serialization
    {
        public string jsonPath;
    }
}