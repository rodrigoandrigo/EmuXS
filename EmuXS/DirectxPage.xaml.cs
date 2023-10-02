using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using Device = SharpDX.Direct3D11.Device;


namespace EmuXS
{

    public sealed partial class DirectxPage : Page
    {
        private SharpDX.Direct3D11.Device d3dDevice;
        private SwapChain swapChain;
        private SharpDX.Direct3D11.DeviceContext d3dContext;
        private RenderTargetView renderTargetView;
        private Texture2D backBuffer;
        private bool initialized = false;

        public DirectxPage()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            this.Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InitializeDirectX();
            CompositionTarget.Rendering += OnRendering;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            CompositionTarget.Rendering -= OnRendering;
            DisposeDirectX();
        }

        private void InitializeDirectX()
        {
            // Configurar a descrição da cadeia de troca
            var swapChainDesc = new SwapChainDescription()
            {
                BufferCount = 1,
                ModeDescription = new ModeDescription(
                    (int)swapChainPanel1.ActualWidth,
                    (int)swapChainPanel1.ActualHeight,
                    new Rational(60, 1), Format.R8G8B8A8_UNorm),
                IsWindowed = false,
                //otputHandle = ,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };



            // Crie o dispositivo e a cadeia de troca
            SharpDX.Direct3D11.Device.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.None,
                swapChainDesc,
                out d3dDevice,
                out swapChain);

            // Obtenha o contexto do dispositivo
            d3dContext = d3dDevice.ImmediateContext;

            // Obtenha o back buffer da cadeia de troca
            backBuffer = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);

            // Crie uma render target view
            renderTargetView = new RenderTargetView(d3dDevice, backBuffer);

            initialized = true;
        }

        private void DisposeDirectX()
        {
            if (initialized)
            {
                renderTargetView.Dispose();
                backBuffer.Dispose();
                d3dContext.ClearState();
                d3dContext.Flush();
                d3dContext.Dispose();
                d3dDevice.Dispose();
                swapChain.Dispose();
                initialized = false;
            }
        }

        private void OnRendering(object sender, object e)
        {
            if (initialized)
            {
                // Limpe o back buffer
                d3dContext.ClearRenderTargetView(renderTargetView, Color.CornflowerBlue);

                // Renderize sua cena DirectX aqui

                // Apresente a cena
                swapChain.Present(1, PresentFlags.None);
            }
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

        private void Configuracoes1_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(#));
        }

        private void Teladirectx_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
