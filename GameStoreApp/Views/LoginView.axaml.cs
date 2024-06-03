using Avalonia.Controls;
using Avalonia.ReactiveUI;
using GameStoreApp.ViewModels;

namespace GameStoreApp.Views {
    public partial class LoginView : ReactiveUserControl<LoginViewModel> {
        public LoginView() {
            InitializeComponent();
        }
    }
}
