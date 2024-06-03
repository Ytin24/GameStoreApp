using GameStoreApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

public class GameBrowserViewModel : ViewModelBase {
    private readonly GameStoreContext _context;
    private readonly IUserService _userService;
    private readonly LibraryViewModel _libraryViewModel;

    public ObservableCollection<Game> Games { get; }

    public ReactiveCommand<Game, Unit> PurchaseCommand { get; }

    public GameBrowserViewModel(IScreen screen, GameStoreContext context, IUserService userService, LibraryViewModel libraryViewModel) : base(screen) {
        _context = context;
        _userService = userService;
        _libraryViewModel = libraryViewModel;

        Games = new ObservableCollection<Game>(context.Games.ToList());

        PurchaseCommand = ReactiveCommand.Create<Game>(PurchaseGame);

        PurchaseCommand.ThrownExceptions.Subscribe(ex => HandlePurchaseError(ex));
    }

    private void PurchaseGame(Game game) {
        var user = _userService.CurrentUser;
        if (user != null && game != null) {
            var library = _context.Libraries.Include(l => l.LibraryGames).FirstOrDefault(l => l.IDUser == user.ID);
            if (library == null) {
                library = new Library {
                    IDUser = user.ID,
                    LibraryGames = new List<LibraryGame> { new LibraryGame { GameID = game.ID, Game = game } }
                };
                _context.Libraries.Add(library);
            }
            else if (!library.LibraryGames.Any(lg => lg.GameID == game.ID)) {
                library.LibraryGames.Add(new LibraryGame { GameID = game.ID, Game = game });
            }
            _context.SaveChanges();
            _libraryViewModel.LoadUserLibrary();
        }
    }

    private void HandlePurchaseError(Exception ex) {
        // Handle the error (e.g., show a message to the user)
    }
}
