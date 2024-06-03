using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace GameStoreApp.Views {
    public partial class GameBrowserView : ReactiveUserControl<GameBrowserViewModel> {
        public GameBrowserView() {
            InitializeComponent();
        }
    }
}
