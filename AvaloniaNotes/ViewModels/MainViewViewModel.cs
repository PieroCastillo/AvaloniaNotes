using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using AvaloniaNotes.Views;
using JetBrains.Annotations;
using ReactiveUI;

namespace AvaloniaNotes.ViewModels
{
    public class MainViewViewModel : ViewModelBase
    {
        private ObservableCollection<NoteViewModel> _Notes;
        private ICommand _AddCommand;
        private ICommand _settingsCommand;
        private ICommand _minimizeCommand;
        private ICommand _closeCommand;

        public MainViewViewModel()
        {
            Notes = new();
            AddCommand = ReactiveCommand.Create(() =>
            {
                Notes.Add(new NoteViewModel{Date = DateTime.Now});
            });
            MinimizeCommand = ReactiveCommand.Create(() =>
            {
                if (AvaloniaLocator.CurrentMutable.GetService<MainWindow>() is Window window)
                {
                    window.WindowState = WindowState.Minimized;
                }
            });
            CloseCommand = ReactiveCommand.Create(() =>
            {
                if (AvaloniaLocator.CurrentMutable.GetService<MainWindow>() is Window window)
                {
                    window.Close();
                }
            });
            SettingsCommand = ReactiveCommand.Create(() =>
            {
                
            });
            AvaloniaLocator.CurrentMutable.Bind<MainViewViewModel>().ToConstant(this);
        }
        
        public ObservableCollection<NoteViewModel> Notes
        {
            get => _Notes;
            set => this.RaiseAndSetIfChanged(ref _Notes, value);
        }

        public ICommand AddCommand
        {
            get => _AddCommand;
            set => this.RaiseAndSetIfChanged(ref _AddCommand, value);
        }

        public ICommand SettingsCommand
        {
            get => _settingsCommand;
            set => this.RaiseAndSetIfChanged(ref _settingsCommand, value);
        }
        
        public ICommand MinimizeCommand
        {
            get => _minimizeCommand;
            set => this.RaiseAndSetIfChanged(ref _minimizeCommand, value);
        }

        public ICommand CloseCommand
        {
            get => _closeCommand;
            set => this.RaiseAndSetIfChanged(ref _closeCommand, value);
        }
    }
}