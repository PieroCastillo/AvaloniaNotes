using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;

namespace AvaloniaNotes.Views
{
    public class MainView : UserControl
    {
        private AppTitleBar _titleBar;
        
        public MainView()
        {
            AvaloniaXamlLoader.Load(this);
            AvaloniaLocator.CurrentMutable.Bind<MainView>().ToConstant(this);
        }

        protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
        {
            base.OnAttachedToLogicalTree(e);
            
            _titleBar = this.Find<AppTitleBar>("TitleBar");
        }
    }
}