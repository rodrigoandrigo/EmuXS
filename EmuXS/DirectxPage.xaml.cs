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

namespace EmuXS
{

    public sealed partial class DirectxPage : Page
    {
        private SharpDX.Direct3D11.Device device;
        private SwapChain swapChain;
        private RenderTargetView renderTargetView;

        public DirectxPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;



        }
        private void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CreateDeviceResources();
            CreateSwapChain();
            Draw();
        }

        private void CreateDeviceResources()
        {
            var deviceCreationFlags = DeviceCreationFlags.BgraSupport;
#if DEBUG
            deviceCreationFlags |= DeviceCreationFlags.Debug;
#endif

            device = new SharpDX.Direct3D11.Device(DriverType.Hardware, deviceCreationFlags);
        }

        private void CreateSwapChain()
        {
            var desc = new SwapChainDescription1()
            {
                AlphaMode = AlphaMode.Ignore,
                BufferCount = 2,
                Format = Format.B8G8R8A8_UNorm,
                Height = (int)swapChainPanel.ActualHeight,
                Width = (int)swapChainPanel.ActualWidth,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.FlipSequential,
                Usage = Usage.RenderTargetOutput
            };

            var dxgiDevice = device.QueryInterface<SharpDX.DXGI.Device2>();
            var dxgiAdapter = dxgiDevice.Adapter.QueryInterface<SharpDX.DXGI.Adapter2>();
            var dxgiFactory = dxgiAdapter.GetParent<SharpDX.DXGI.Factory2>();
            swapChain = new SwapChain1(dxgiFactory, device, ref desc);
            dxgiDevice.MaximumFrameLatency = 1;

            using (var backBuffer = swapChain.GetBackBuffer<Texture2D>(0))
                renderTargetView = new RenderTargetView(device, backBuffer);
        }

        private void Draw()
        {
            var context = device.ImmediateContext;
            context.OutputMerger.SetRenderTargets(renderTargetView);
            context.ClearRenderTargetView(renderTargetView, new SharpDX.Mathematics.Interop.RawColor4(0.5f, 0.5f, 0.5f, 1.0f));
            swapChain.Present(1, PresentFlags.None);
        }

        private void BackFromScreen_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
