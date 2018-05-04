using Xamarin.Forms;
using XFormsOfflineSync.Messages;

namespace XFormsOfflineSync
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            startSync.Clicked += (s, e) =>
            {
                var message = new StartSyncMessage();
                MessagingCenter.Send(message, nameof(StartSyncMessage));
            };

            //stopLongRunningTask.Clicked += (s, e) =>
            //{
            //    var message = new StopLongRunningTaskMessage();
            //    MessagingCenter.Send(message, "StopLongRunningTaskMessage");
            //};

            HandleReceivedMessages();
        }

        private void HandleReceivedMessages()
        {
            //MessagingCenter.Subscribe<TickedMessage>(this, "TickedMessage", message => {
            //    Device.BeginInvokeOnMainThread(() => {
            //        ticker.Text = message.Message;
            //    });
            //});

            MessagingCenter.Subscribe<CancelledMessage>(this, nameof(CancelledMessage), message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    // Show UI Cancelled message

                    //ticker.Text = "Cancelled";
                });
            });
        }
    }
}