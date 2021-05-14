using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;
using ReactiveUI;

namespace AvaloniaNotes.Views
{
    [PseudoClasses(":windows",":other")]
    public class AppTitleBar : TemplatedControl
    {
        private Button? CloseButton;
        private Button? MinimizeButton;
        private Button AddButton;
        private Button SettingsButton;

        public AppTitleBar()
        {
            
        }
        
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (OperatingSystem.IsWindows())
            {
                PseudoClasses.Add(":windows");
            }
            else
            {
                PseudoClasses.Add(":other");
            }
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);
            var mainwindow = AvaloniaLocator.CurrentMutable.GetService<MainWindow>();
            if (mainwindow is not null)
            {
                mainwindow.BeginMoveDrag(e);
            }
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            Background = new SolidColorBrush((App.Current as App).GetTitleBarColor());

            CloseButton = e.NameScope.Find<Button>("PART_CloseButton");
            MinimizeButton = e.NameScope.Find<Button>("PART_MinimizeButton");
            SettingsButton = e.NameScope.Find<Button>("PART_SettingsButton");
            AddButton = e.NameScope.Find<Button>("PART_AddButton");

            if (CloseButton is not null)
            {
                CloseButton.Bind(Button.CommandProperty, this.GetObservable(CloseButtonCommandProperty));
            }

            if (MinimizeButton is not null)
            {
                MinimizeButton.Bind(Button.CommandProperty, this.GetObservable(MinimizeButtonCommandProperty));
            }

            AddButton.Bind(Button.CommandProperty, this.GetObservable(AddButtonCommandProperty));
            SettingsButton.Bind(Button.CommandProperty, this.GetObservable(SettingsButtonCommandProperty));
        }

        private ICommand _AddButtonCommand;

        public ICommand AddButtonCommand
        {
            get => _AddButtonCommand;
            set => SetAndRaise(AddButtonCommandProperty, ref _AddButtonCommand, value);
        }

        public static readonly DirectProperty<AppTitleBar, ICommand> AddButtonCommandProperty =
            AvaloniaProperty.RegisterDirect<AppTitleBar, ICommand>(nameof(AddButtonCommand), o => o.AddButtonCommand,
                (o, v) => o.AddButtonCommand = v);
        
        private ICommand _SettingsButtonCommand;

        public ICommand SettingsButtonCommand
        {
            get => _SettingsButtonCommand;
            set => SetAndRaise(SettingsButtonCommandProperty, ref _SettingsButtonCommand, value);
        }

        public static readonly DirectProperty<AppTitleBar, ICommand> SettingsButtonCommandProperty =
            AvaloniaProperty.RegisterDirect<AppTitleBar, ICommand>(nameof(SettingsButtonCommand), o => o.SettingsButtonCommand,
                (o, v) => o.SettingsButtonCommand = v);       
        
        private ICommand _MinimizeButtonCommand;

        public ICommand MinimizeButtonCommand
        {
            get => _MinimizeButtonCommand;
            set => SetAndRaise(MinimizeButtonCommandProperty, ref _MinimizeButtonCommand, value);
        }

        public static readonly DirectProperty<AppTitleBar, ICommand> MinimizeButtonCommandProperty =
            AvaloniaProperty.RegisterDirect<AppTitleBar, ICommand>(nameof(MinimizeButtonCommand), o => o.MinimizeButtonCommand,
                (o, v) => o.MinimizeButtonCommand = v);     
        
        private ICommand _CloseButtonCommand;

        public ICommand CloseButtonCommand
        {
            get => _CloseButtonCommand;
            set => SetAndRaise(CloseButtonCommandProperty, ref _CloseButtonCommand, value);
        }

        public static readonly DirectProperty<AppTitleBar, ICommand> CloseButtonCommandProperty =
            AvaloniaProperty.RegisterDirect<AppTitleBar, ICommand>(nameof(CloseButtonCommand), o => o.CloseButtonCommand,
                (o, v) => o.CloseButtonCommand = v);
    }
}