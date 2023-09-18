using EmuXS.ConPTY;
using EmuXS.ConPTY.Interop.Definitions;
using System;
using System.IO;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;

namespace EmuXS
{

    public sealed partial class Termi : Page
    {
        public TermConPTY termConPTY;
        public Termi()
        {
            this.InitializeComponent();

            InitializeComponent();

            termConPTY = new TermConPTY
            {
                WorkingDirectory = Directory.GetCurrentDirectory(),
                Arguments = string.Empty,
                FilterControlSequences = true,
            };

            termConPTY.OutputDataReceived += async (sender, data) =>
            {
                await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Run Data1;
                    Data1 = new Run();
                    Data1.Text = data + "\n";
                    TextIO1.Inlines.Add(Data1);
                });
            };


            ProcessInfo processInfo = termConPTY.Start();

        }


        public void TextBox1_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                termConPTY.WriteLine(TextBox1.Text);
                e.Handled = true;
                TextBox1.Text = "";
            }

        }

        private void Termib1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }


    }
}
