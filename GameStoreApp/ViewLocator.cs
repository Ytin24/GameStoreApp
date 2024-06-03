using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GameStoreApp.ViewModels;
using ReactiveUI;
using System;
using System.Linq;
using System.Reflection;

namespace GameStoreApp {
    public class ViewLocator : IViewLocator {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null) {
            if (viewModel == null) {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var viewModelName = viewModel.GetType().Name;

            // Предполагаем, что View имеет то же имя, что и ViewModel, но без "Model" и с суффиксом "View"
            var viewName = viewModelName.Replace("ViewModel", "View");

            // Ищем соответствующий тип View
            var viewType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == viewName);
            if (viewType == null) {
                return default;
            }

            // Создаем экземпляр View
            var view = Activator.CreateInstance(viewType);
            if (view == null) {
                throw new InvalidOperationException($"Не удалось создать представление для {viewModelName}");
            }

            // Устанавливаем DataContext
            ((UserControl)view).DataContext = viewModel;
            return (IViewFor?)view;
        }
    }
}

