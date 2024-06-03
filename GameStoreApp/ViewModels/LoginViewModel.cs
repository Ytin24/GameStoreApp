using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace GameStoreApp.ViewModels {
    public class LoginViewModel : ViewModelBase {
        private readonly IUserService _userService;
        private readonly LibraryViewModel _libraryViewModel;

        private string _login;
        public string Login {
            get => _login;
            set => this.RaiseAndSetIfChanged(ref _login, value);
        }

        private string _password;
        public string Password {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ReactiveCommand<Unit, Unit> LoginCommand { get; }
        public ReactiveCommand<bool, Unit> LoginCompleted { get; set; }

        public LoginViewModel(IScreen screen, IUserService userService, LibraryViewModel libraryViewModel) : base(screen) {
            _userService = userService;
            _libraryViewModel = libraryViewModel;

            var canLogin = this.WhenAnyValue(
                x => x.Login,
                x => x.Password,
                (login, password) =>
                    !string.IsNullOrWhiteSpace(login) &&
                    !string.IsNullOrWhiteSpace(password));

            LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync, canLogin);

        }

        private async Task LoginAsync() {
            var user = await _userService.LoginUser(Login, Password);
            if (user != null) {
                await LoginCompleted?.Execute(true);
            }
            else {
                await LoginCompleted?.Execute(false);
            }
        }

    }
}
