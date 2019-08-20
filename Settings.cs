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
        public static string GetProjectPath()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution; // get solution reference from a service provider
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

//TODO after start check every frame if mouse is moving
//TODO if not moving for 0.5s check what text is in cursor location and find it in json file
//TODO if still not moving not check again
//TODO if moving do nothing
