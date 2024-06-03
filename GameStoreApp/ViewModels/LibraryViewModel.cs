using GameStoreApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

public class LibraryViewModel : ViewModelBase {
    private readonly GameStoreContext _context;
    private readonly IUserService _userService;

    public ObservableCollection<Game> UserLibrary { get; private set; }

    public LibraryViewModel(IScreen screen, GameStoreContext context, IUserService userService) : base(screen) {
        _context = context;
        _userService = userService;

        UserLibrary = new ObservableCollection<Game>();
        LoadUserLibrary();
        this.WhenAnyValue(x => x._userService.CurrentUser).WhereNotNull().Subscribe(new AnonymousObserver<User>(x => { 
            LoadUserLibrary(); }));
    }

    public void LoadUserLibrary() {
        var user = _userService.CurrentUser;
        if (user != null) {
            var library = _context.Libraries.Include(l => l.LibraryGames).ThenInclude(lg => lg.Game).FirstOrDefault(l => l.IDUser == user.ID);
            if (library != null) {
                UserLibrary.Clear();
                foreach (var game in library.LibraryGames.Select(lg => lg.Game)) {
                    UserLibrary.Add(game);
                }
            }
        }
    }
}
