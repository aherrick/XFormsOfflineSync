using System;
using System.IO;
using Xamarin.Forms;
using XFormsOfflineSync.Interfaces;
using XFormsOfflineSync.iOS.PlatformSpecific;

[assembly: Dependency(typeof(FileHelper))]

namespace XFormsOfflineSync.iOS.PlatformSpecific
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}