using ReactiveUI;
using Splat;
using GameStoreApp.ViewModels;
using System.Diagnostics.Metrics;

public class MainViewModel : ReactiveObject, IScreen {
    public RoutingState Router { get; } = new RoutingState();

    public MainViewModel() {
       
    }
}
