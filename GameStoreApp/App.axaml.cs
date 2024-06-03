using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using GameStoreApp.Views;
using ReactiveUI;
using Splat;
using System;

namespace GameStoreApp {
    public partial class App : Application {
        public override void Initialize() {
            AvaloniaXamlLoader.Load(this);
            Bootstrapper.Init();
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
        }

        public override void OnFrameworkInitializationCompleted() {
            var mainViewModel = GetRequiredService<MainViewModel>();
            var navigationViewModel = GetRequiredService<NavigationViewModel>();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
                desktop.MainWindow = new MainWindow {
                    DataContext = navigationViewModel
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
                singleViewPlatform.MainView = new MainView {
                    DataContext = navigationViewModel
                };
            }

            navigationViewModel.ShowLoginCommand.Execute().Subscribe();
            base.OnFrameworkInitializationCompleted();
        }

        private static T GetRequiredService<T>() => Splat.Locator.Current.GetRequiredService<T>();
    }
}
