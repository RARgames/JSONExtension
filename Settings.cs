using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONExtension
{
    public class Settings
    {
        public string projectPath;

        public void Initialize()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            projectPath = GetProjectPath();
            VsShellUtilities.ShowMessageBox(ServiceProvider.GlobalProvider, projectPath, "TEMP", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST); //Show a message box
            //TODO seems to be working, remove if sure
        }

        private string GetProjectPath()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            IVsSolution solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution; // get solution reference from a service provider
            if (solution != null)
            {
                solution.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object open);
                bool isOpen = (bool)open; // is the solution open?
                if (isOpen)
                {
                    solution.GetSolutionInfo(out string dir, out string file, out string ops);  // dir will contain the solution's directory path (folder in the open folder case)
                    return dir;
                }
            }
            return null;
        }
    }

    public class SettingsJSON
    {
        public string jsonPath;
    }
}

//TODO nie dziala w status bar
//TODO nie dziala edit
//TODO nie dziala wyswietlanie

//TODO code/using cleaning
//TODO add comments
//TODO edit all error mesages to make clear JSON Extension


