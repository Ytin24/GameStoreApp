using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Threading.Tasks;

public interface IUserService {
    Task<bool> RegisterUser(string login, string password);
    Task<User> LoginUser(string login, string password);
    User CurrentUser { get; }
}
public class UserService : ReactiveObject, IUserService {
    private readonly GameStoreContext _context;
    private User _currentUser;
    public User CurrentUser {
        get => _currentUser;
        set => this.RaiseAndSetIfChanged(ref _currentUser, value);
    }

    public UserService(GameStoreContext context) {
        _context = context;
    }

    public async Task<bool> RegisterUser(string login, string password) {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (existingUser != null) {
            return false;
        }

        var user = new User {
            Login = login,
            Password = password
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        CurrentUser = user;
        return true;
    }

    public async Task<User> LoginUser(string login, string password) {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        if (user != null) {
            CurrentUser = user;
        }
        return user;
    }

}
