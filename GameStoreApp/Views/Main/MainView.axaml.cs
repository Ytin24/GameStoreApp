using GameStoreApp.ViewModels;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace GameStoreApp.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
}
