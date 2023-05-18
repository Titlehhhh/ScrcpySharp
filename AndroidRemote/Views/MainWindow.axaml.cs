using AdvancedSharpAdbClient;
using AndroidRemote.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using System;
using System.Linq;

namespace AndroidRemote.Views
{
	public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{

		public MainWindow()
		{
			InitializeComponent();
			this.KeyUp += MainWindow_KeyUp;
			this.PointerPressed += MainWindow_PointerPressed;
			this.PointerMoved += MainWindow_PointerMoved;
		}

		private void MainWindow_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
		{
			if (this.ViewModel is null)
				return;
			this.ViewModel.OnMouseMove(e.GetPosition(Surface));
		}

		private void MainWindow_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
		{
			if (this.ViewModel is null)
				return;
			var point = e.GetCurrentPoint(Surface);
			Debug.Text = $"Pos: {point.Position.X} {point.Position.Y}";
			this.ViewModel.OnClick(point);

		}

		private void MainWindow_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
		{
			if (this.ViewModel is null)
				return;

		}
	}
}