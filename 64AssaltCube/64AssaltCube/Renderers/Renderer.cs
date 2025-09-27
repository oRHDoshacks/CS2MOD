using ClickableTransparentOverlay;
using ImGuiNET;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace _64AssaltCube.Renderers
{
    public class Renderer : Overlay
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;   // X inicial
            public int Top;    // Y inicial
            public int Right;  // X final
            public int Bottom; // Y final
        }

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static int selectedTab = 0;
        public static ImDrawListPtr drawnList;
        public static Vector2 WindowsPosition = new Vector2();
        
        
        protected override void Render()
        {
            
            DrawOverlay(Programa.screenSize);
            drawnList = ImGui.GetWindowDrawList();
            Esp.DrawCross();
            Position();
            foreach (var inimigo in Programa.inimigos.ToList())
            {
                if (inimigo == null) { continue; }
                if (inimigo.Posicao2d.X <= 0) { continue; }
                if (Configs.EspLine)
                    Esp.DrawnLine(inimigo);
                if (Configs.EspBox)
                    Esp.DrawnBox(inimigo);
                if (Configs.EspName)
                    Esp.DrawnName(inimigo);

            }
            if (Configs.Menu)
            {

                Menu.DrawnMenu();

            }
        }
        
        public static void DrawOverlay(Vector2 ScreanDrawn)
        {
            ImGui.SetNextWindowSize(ScreanDrawn);
            ImGui.SetNextWindowPos(new Vector2(WindowsPosition.X, WindowsPosition.Y));
            ImGui.Begin("Screan", ImGuiWindowFlags.NoDecoration
                | ImGuiWindowFlags.NoBackground
                | ImGuiWindowFlags.NoBringToFrontOnFocus
                | ImGuiWindowFlags.NoMove
                | ImGuiWindowFlags.NoInputs
                | ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
                );

        }
        static void Position()
        {
            // exemplo: pegar pelo nome do processo
            Process processo = Process.GetProcessesByName(Programa.nameprocess)[0];
            IntPtr handle = processo.MainWindowHandle;

            if (GetWindowRect(handle, out RECT rect))
            {

                WindowsPosition.X = rect.Left+7.5f;
                WindowsPosition.Y = rect.Top+30.5f;
                
            }
            else
            {
                Console.WriteLine("Não foi possível obter a posição da janela.");
            }
        }
    }
}
