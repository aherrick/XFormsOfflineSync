namespace XFormsOfflineSync.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new XFormsOfflineSync.App());
        }
    }
}