using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XFormsOfflineSync.Data;
using XFormsOfflineSync.Interfaces;

namespace XFormsOfflineSync
{
	public partial class App : Application
    {
        public static DB Database;

        public App ()
		{
			InitializeComponent();

            Database = new DB(DependencyService.Get<IFileHelper>().GetLocalFilePath("DBSQLite.db3"));

            MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
