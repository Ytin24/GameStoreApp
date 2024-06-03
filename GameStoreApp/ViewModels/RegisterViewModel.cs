using GameStoreApp.ViewModels;
using Microsoft.VisualBasic;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

public class RegisterViewModel : ViewModelBase, IValidatableViewModel {
    public string UrlPathSegment => "register";
    public IScreen HostScreen { get; }

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

    private string _confirmPassword;
    public string ConfirmPassword {
        get => _confirmPassword;
        set => this.RaiseAndSetIfChanged(ref _confirmPassword, value);
    }

    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    public ReactiveCommand<bool, Unit> LoginCompleted { get; set; }

    public ValidationContext ValidationContext { get; } = new ValidationContext();

    IValidationContext IValidatableViewModel.ValidationContext => ValidationContext;

    public RegisterViewModel(IScreen screen, IUserService userService) : base(screen) {
        HostScreen = screen;

        var canRegister = this.WhenAnyValue(
            x => x.Login,
            x => x.Password,
            x => x.ConfirmPassword,
            (login, password, confirmPassword) =>
                !string.IsNullOrWhiteSpace(login) &&
                !string.IsNullOrWhiteSpace(password) &&
                password == confirmPassword);

        RegisterCommand = ReactiveCommand.CreateFromTask(async () => {
            await LoginCompleted?.Execute(await userService.RegisterUser(Login, Password));
        }, canRegister);

        this.ValidationRule(
            vm => vm.Login,
            login => !string.IsNullOrWhiteSpace(login),
            "Login cannot be empty");
        this.ValidationRule(
            vm => vm.Password,
            password => !string.IsNullOrWhiteSpace(password),
            "Password cannot be empty");
        this.ValidationRule(
            vm => vm.ConfirmPassword,
            confirmPassword => confirmPassword == Password,
            "Passwords must match");
    }
}
