using System;
using System.Diagnostics;
using System.Windows.Input;
using Avalonia;
using Avalonia.Media;
using ReactiveUI;

namespace AvaloniaNotes.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private IBrush _AccentBrush;
        private IImage _image;
        private string _Text;
        private DateTime _date;
        private ICommand _closeCommand;

        public NoteViewModel()
        {
            Date = DateTime.Now;
            Debug.WriteLine(DateTime.Now.ToString("f"));
            AccentBrush = Brushes.Azure;

            CloseCommand = ReactiveCommand.Create(() =>
            {
                AvaloniaLocator.CurrentMutable.GetService<MainViewViewModel>().Notes.Remove(this);
            });
        }
        
        public IBrush AccentBrush
        {
            get => _AccentBrush;
            set => this.RaiseAndSetIfChanged(ref _AccentBrush, value);
        }

        public string Text
        {
            get => _Text;
            set => this.RaiseAndSetIfChanged(ref _Text, value);
        }
        
        public IImage Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public DateTime Date
        {
            get => _date;
            set => this.RaiseAndSetIfChanged(ref _date, value);
        }

        public ICommand CloseCommand
        {
            get => _closeCommand;
            set => this.RaiseAndSetIfChanged(ref _closeCommand, value);
        }
    }
}