using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmuXS
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class SDL : Page
    {
        public SDL()
        {
            this.InitializeComponent();                        
            SDL2.SDL.SDL_main_func mainFunction = SDLMain;
            SDL2.SDL.SDL_WinRTRunApp(mainFunction, IntPtr.Zero);
            Canvas1.DataContext = SDL2.SDL.SDL_WinRTRunApp(mainFunction, IntPtr.Zero);

        }
        public static int SDLMain(int argc, IntPtr argv)
        {
            // Initialize SDL; tell SDL which subsystems we want to use.

            var initResult = SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_VIDEO | SDL2.SDL.SDL_INIT_AUDIO);

            if (initResult != 0)
                throw new Exception(String.Format("Failure while initializing SDL. SDL Error: {0}", SDL2.SDL.SDL_GetError()));

            // Get the current display resolution.

            SDL2.SDL.SDL_DisplayMode displayMode;

            var getDisplayModeResult = SDL2.SDL.SDL_GetCurrentDisplayMode(0, out displayMode);

            if (getDisplayModeResult != 0)
                throw new Exception(String.Format("Failure while getting display mode. SDL Error: {0}", SDL2.SDL.SDL_GetError()));

            // Create the window we'll be rendering in; make it the full size of the screen.

            var window = SDL2.SDL.SDL_CreateWindow("Tela SDL2",
                SDL2.SDL.SDL_WINDOWPOS_CENTERED,
                SDL2.SDL.SDL_WINDOWPOS_CENTERED,
                displayMode.w,
                displayMode.h,
                SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP
            );

            if (window == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a window. SDL Error: {0}", SDL2.SDL.SDL_GetError()));

            // Create a renderer for our new window.

            var renderer = SDL2.SDL.SDL_CreateRenderer(window, -1, SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED /*| SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC*/);

            if (renderer == IntPtr.Zero)
                throw new Exception(String.Format("Unable to create a renderer. SDL Error: {0}", SDL2.SDL.SDL_GetError()));

            // We can scale the image up or down based on a scaling factor.
            //SDL.SDL_RenderSetScale(renderer, 2, 2);

            // By setting the logical size we ensure that the image will scale to fit the window while
            // still maintaining the original aspect ratio.
            SDL2.SDL.SDL_RenderSetLogicalSize(renderer, 960, 540);

            // Start the game loop.
            GameLoop(renderer);

            return 0;
        }

        public static void GameLoop(IntPtr sdlRenderer)
        {
            // Clears the current render surface.
            SDL2.SDL.SDL_RenderClear(sdlRenderer);

            // Sets the color that the screen will be cleared with.
            SDL2.SDL.SDL_SetRenderDrawColor(sdlRenderer, 39, 245, 234, 1);

            // Switches out the currently presented render surface with the one we just did work on.
            SDL2.SDL.SDL_RenderPresent(sdlRenderer);
        }

        private void Page2b1_Click(object sender, RoutedEventArgs e)
        {
            SplitV1.IsPaneOpen = !SplitV1.IsPaneOpen;

        }

        private void Page2b2_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Page2));
        }

        private void Term_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Termi));
        }

        private void Configuracoes1_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(#));
        }
    }
}
