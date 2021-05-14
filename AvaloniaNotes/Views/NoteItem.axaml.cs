using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using AvaloniaNotes.Controls;
using ReactiveUI;

namespace AvaloniaNotes.Views
{
    public class NoteItem : UserControl
    {
        private TextPresenter _textPresenter;
        private Image _image;
        private TextBlock _dateBlock;
        private Button _closeButton;
        
        public NoteItem()
        {
            InitializeComponent();
            //search of components
            _textPresenter = this.Find<TextPresenter>("PART_TextPresenter");
            _image = this.Find<Image>("PART_Image");
            _dateBlock = this.Find<TextBlock>("PART_DateText");
            _closeButton = this.Find<Button>("PART_CloseButton");
            
            //bindings
            _textPresenter.Bind(TextPresenter.TextProperty, this.GetObservable(TextProperty));
            _image.Bind(Image.SourceProperty, this.GetObservable(ImageSourceProperty));
            this.GetObservable(DateProperty).Subscribe(d =>
            {
                _dateBlock.Text = d.ToString("f");
            });
            _closeButton.Bind(Button.CommandProperty, this.GetObservable(CloseButtonCommandProperty));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // protected override void OnPointerReleased(PointerReleasedEventArgs e)
        // {
        //     base.OnPointerReleased(e);
        //     
        //     ContentDialog.SetContentDialog(this, new ContentDialog
        //     {
        //         Header = "Test",
        //         Content="Hello World!"
        //     });
        //     ContentDialog.ShowAt(this, () =>{Debug.WriteLine("Closed correctly");});
        // }

        private string _Text;

        public string Text
        {
            get => _Text;
            set => SetAndRaise(TextProperty, ref _Text, value);
        }

        public static readonly DirectProperty<NoteItem, string> TextProperty =
            AvaloniaProperty.RegisterDirect<NoteItem, string>(nameof(Text), o => o.Text, (o, v) => o.Text = v);
        

        public IImage ImageSource
        {
            get => GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public static readonly StyledProperty<IImage> ImageSourceProperty =
            AvaloniaProperty.Register<NoteItem, IImage>(nameof(ImageSource));

        public DateTime Date
        {
            get => GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        public static readonly StyledProperty<DateTime> DateProperty =
            AvaloniaProperty.Register<NoteItem, DateTime>(nameof(Date));

        private ICommand _CloseButtonCommand;

        public ICommand CloseButtonCommand
        {
            get => _CloseButtonCommand;
            set => SetAndRaise(CloseButtonCommandProperty, ref _CloseButtonCommand, value);
        }

        public readonly static DirectProperty<NoteItem, ICommand> CloseButtonCommandProperty =
            AvaloniaProperty.RegisterDirect<NoteItem, ICommand>(nameof(CloseButtonCommand), o => o.CloseButtonCommand,
                (o, v) => o.CloseButtonCommand = v);
    }
}