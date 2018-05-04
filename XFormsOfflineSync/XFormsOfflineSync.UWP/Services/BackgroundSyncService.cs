using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using XFormsOfflineSync.UWP.Background;

namespace XFormsOfflineSync.UWP.Services
{
    public class BackgroundSyncService
    {
        //private ApplicationTrigger

        public async Task Start()
        {
            if (IsRegistered())
                Deregister();

            BackgroundExecutionManager.RemoveAccess();

            // does this prompt everytime?
            await BackgroundExecutionManager.RequestAccessAsync();

            var builder = new BackgroundTaskBuilder();

            builder.Name = "BackgroundTask";
            builder.TaskEntryPoint = typeof(BackgroundTask).FullName;

            var trigger = new ApplicationTrigger();
            builder.SetTrigger(trigger);
            BackgroundTaskRegistration task = builder.Register();

            task.Completed += Task_Completed;

            var result = await trigger.RequestAsync();
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            // alert UI
        }

        public void Stop()
        {
            Deregister();
        }

        private void Deregister()
        {
            var taskName = "BackgroundTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
                if (task.Value.Name == taskName)
                    task.Value.Unregister(true);
        }

        private bool IsRegistered()
        {
            var taskName = "BackgroundTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
                if (task.Value.Name == taskName)
                    return true;

            return false;
        }
    }
}