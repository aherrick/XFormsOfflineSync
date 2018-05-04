using System.IO;
using Windows.Storage;
using Xamarin.Forms;
using XFormsOfflineSync.Interfaces;
using XFormsOfflineSync.UWP.PlatformSpecific;

[assembly: Dependency(typeof(FileHelper))]

namespace XFormsOfflineSync.UWP.PlatformSpecific
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}