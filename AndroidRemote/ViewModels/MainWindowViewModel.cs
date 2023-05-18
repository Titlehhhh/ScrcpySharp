using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.DeviceCommands;
using Avalonia;
using Avalonia.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndroidRemote.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		[Reactive]
		public string Status { get; private set; }

		public ICommand ConnectCommand { get; }

		public MainWindowViewModel()
		{
			ConnectCommand = ReactiveCommand.CreateFromTask(Connect);
		}
		private AdbClient adbClient;
		private DeviceData device;
		private async Task Connect()
		{
			adbClient = new AdbClient();
			await adbClient.ConnectAsync("127.0.0.1:62001");
			device = (await adbClient.GetDevicesAsync(default)).First();

			using (var fs = File.OpenRead(@"C:\Users\Title\OneDrive\Рабочий стол\TestAndoidApp\com.Title.ControllingServer.apk"))
			{
				Status = "Instlling....";
				await adbClient.InstallAsync(device, fs);
				Status = "Starting server...";
			}
			await adbClient.StartAppAsync(device, "com.Title.ControllingServer");

			//await adbClient.ExecuteShellCommand();
		}
		Point point;
		public void OnMouseMove(Point point)
		{
			this.point = point;
		}
		public async void OnClick(PointerPoint point)
		{

		}
	}
}