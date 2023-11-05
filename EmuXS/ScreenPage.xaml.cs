using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Windows.UI;
using Microsoft.Graphics.Canvas;

namespace EmuXS
{

    public sealed partial class ScreenPage : Page
    {

        public ScreenPage()
        {
            this.InitializeComponent();

        }

        private void CanvasControl_Draw_1(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawEllipse(155, 115, 80, 30, Colors.Black, 3);
            args.DrawingSession.DrawText("Hello, world!", 100, 100, Colors.Yellow);
            args.DrawingSession.Antialiasing = CanvasAntialiasing.Antialiased;
        }

        private void BackFromScreen_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }
    }
}
