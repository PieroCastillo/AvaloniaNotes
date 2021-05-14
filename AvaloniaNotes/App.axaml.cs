using System;
using System.IO;
using System.Threading.Tasks;
using AuraUtilities.Configuration;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;
using AvaloniaNotes.ViewModels;
using AvaloniaNotes.Views;

namespace AvaloniaNotes
{
    public class App : Application
    {
        public override void Initialize()
        {
            var settingsProvider = new SettingsProvider();
            Settings = settingsProvider.Load<AppSettings>();

            switch (Settings.Theme)
            {
                case Theme.Light:
                    Styles.Insert(0,FluentLight);
                    break;
                case Theme.Dark:
                    Styles.Insert(0, FluentDark);
                    break;
            }

            if (Settings.AccentColor is null)
            {
                Settings.AccentColor = Colors.LightGreen.ToString();
            }
            
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        
        public Theme GetTheme() => Settings.Theme;

        public async Task SetTheme(Theme theme)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                switch (theme)
                {
                    case Theme.Light:
                        Application.Current.Styles[0] = App.FluentLight;
                        break;
                    case Theme.Dark:
                        Application.Current.Styles[0] = App.FluentDark;
                        break;
                }
            }, (DispatcherPriority)1);
            Settings.Theme = theme;
        }

        public Color GetTitleBarColor() => Color.Parse(Settings.AccentColor);

        public void SetTitleBarColor(Color color) => Settings.AccentColor = color.ToString();
        
        public readonly static Styles FluentDark = new Styles
        {
            new StyleInclude(new Uri("avares://AvaloniaNotes/Styles"))
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/FluentDark.xaml")
            }
        };

        public readonly static Styles FluentLight = new Styles
        {
            new StyleInclude(new Uri("avares://AvaloniaNotes/Styles"))
            {
                Source = new Uri("avares://Avalonia.Themes.Fluent/FluentLight.xaml")
            }
        };
        
        public AppSettings Settings { get; private set; }
    }
}