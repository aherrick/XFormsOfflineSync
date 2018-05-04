using System.IO;
using Xamarin.Forms;
using XFormsOfflineSync.Droid.PlatformSpecific;
using XFormsOfflineSync.Interfaces;

[assembly: Dependency(typeof(FileHelper))]

namespace XFormsOfflineSync.Droid.PlatformSpecific
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}