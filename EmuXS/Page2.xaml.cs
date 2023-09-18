using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EmuXS
{

    public sealed partial class Page2 : Page
    {
        public Page2()
        {
            this.InitializeComponent();
        }

        private void Page2b1_Click(object sender, RoutedEventArgs e)
        {
            SplitV1.IsPaneOpen = !SplitV1.IsPaneOpen;

        }

        private void Page2b2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Term_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Termi));
        }

        private void Telasdl_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SDL));
        }

        private void Configuracoes1_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(#));
        }
    }
}
