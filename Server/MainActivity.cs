using Android.AccessibilityServices;
using Android.App;
using Android.App.Admin;
using Android.Content;
using Android.Content.PM;
using Android.Hardware.Input;
using Android.OS;
using Android.Test;
using Android.Views;
using Java.Lang;

namespace Server
{
	[Activity(Label = "@string/app_name", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle? savedInstanceState)
		{
			base.OnCreate(savedInstanceState);



			



			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.activity_main);

		}

	}

}