using System.Diagnostics;
using Windows.ApplicationModel.Background;

namespace XFormsOfflineSync.UWP.Background
{
    public sealed class BackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Background Task Running");

            _deferral = taskInstance.GetDeferral();
            try
            {
                Trace.WriteLine("hi");
                //var settings = Windows.Storage.ApplicationData.Current.LocalSettings;

                //settings.Values.Add("BackgroundTask", "Hello from UWP");
            }
            catch { }
            _deferral.Complete();
        }
    }
}