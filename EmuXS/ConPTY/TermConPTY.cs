using EmuXS.ConPTY.Interop.Definitions;

namespace EmuXS.ConPTY
{

    public class TermConPTY : WindowsPseudoConsole
    {

        public new ProcessInfo Start(short width = 80, short height = 30)
        {
            FileName = "cmd.exe";

            return base.Start(width, height);
        }
    }
}
