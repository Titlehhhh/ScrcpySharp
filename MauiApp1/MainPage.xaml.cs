using ControllingAndroid;

namespace MauiApp1
{
	public partial class MainPage : ContentPage
	{
		IScreen screen;
		public MainPage()
		{
			InitializeComponent();
			screen = new Screen();
		}

		private async void OnCounterClicked(object sender, EventArgs e)
		{
			try
			{
				await Task.Run(async () =>
				{
					await Task.Delay(1000);
					using (screen.Press(new Point(500, 500)))
					{
						using (screen.Press(new Point(600, 600)))
						{
							await Task.Delay(5000);
						}
					}

				});
			}
			catch
			{

			}
		}
	}
}