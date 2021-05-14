using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Threading;

namespace AvaloniaNotes.Controls
{
    public class ContentDialog : HeaderedContentControl
    {
        private Button _closebutton;
        private Action onClose = () => { };

        public static ContentDialog GetContentDialog(Control control)
            => control.GetValue(ContentDialogProperty);

        public static void SetContentDialog(Control control, ContentDialog value)
            => control.SetValue(ContentDialogProperty, value);

        public static readonly AttachedProperty<ContentDialog> ContentDialogProperty =
            AvaloniaProperty.RegisterAttached<ContentDialog, Control, ContentDialog>("ContentDialog");

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            _closebutton = e.NameScope.Find<Button>("PART_CloseButton");
            _closebutton.Click += Close;
        }

        private void Close(object? sender, RoutedEventArgs e)
        {
            var layer = OverlayLayer.GetOverlayLayer(this);
            onClose.Invoke();
            Dispatcher.UIThread.Post(() =>
            {
                layer.Children.Remove(this);
            });
        }

        public static void ShowAt(Control control, Action onClose)
        {
            var layer = OverlayLayer.GetOverlayLayer(control);
            var contentControl = GetContentDialog(control);
            contentControl.onClose = onClose;
            layer.GetObservable(BoundsProperty).Subscribe((bounds) =>
            {
                contentControl.Width = bounds.Width;
                contentControl.Height = bounds.Height;
            });
            Dispatcher.UIThread.Post(() =>
            {
                layer.Children.Add(contentControl);
            });
        }
    }
}