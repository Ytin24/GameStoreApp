using GameStoreApp.ViewModels;
using ReactiveUI;
using System;
using System.Reactive;

public class NavigationViewModel : ReactiveObject {
    private readonly IScreen _screen;

    public RoutingState Router => _screen.Router;

    public ReactiveCommand<Unit, IRoutableViewModel> ShowLoginCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ShowRegisterCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ShowGameBrowserCommand { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> ShowLibraryCommand { get; }
    public ReactiveCommand<Unit, Unit> LogoutCommand { get; }

    private bool _isAuthenticated;
    public bool IsAuthenticated {
        get => _isAuthenticated;
        private set => this.RaiseAndSetIfChanged(ref _isAuthenticated, value);
    }

    public NavigationViewModel(IScreen screen,
                               LoginViewModel loginViewModel,
                               RegisterViewModel registerViewModel,
                               GameBrowserViewModel gameBrowserViewModel,
                               LibraryViewModel libraryViewModel) {
        _screen = screen;

        ShowLoginCommand = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(loginViewModel));
        ShowRegisterCommand = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(registerViewModel));
        ShowGameBrowserCommand = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(gameBrowserViewModel));
        ShowLibraryCommand = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(libraryViewModel));
        LogoutCommand = ReactiveCommand.Create(() => { IsAuthenticated = false; Router.Navigate.Execute(loginViewModel).Subscribe(); });

        loginViewModel.LoginCompleted = ReactiveCommand.Create<bool>(Auth);
        registerViewModel.LoginCompleted = ReactiveCommand.Create<bool>(Auth);
    }

    private void Auth(bool x) {
        IsAuthenticated = x;
        if (x) {
            ShowLibraryCommand.Execute();
        }
    }
}
