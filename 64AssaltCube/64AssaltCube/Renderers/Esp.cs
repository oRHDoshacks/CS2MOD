using ClickableTransparentOverlay;
using ImGuiNET;
using System.Drawing;
using System.Numerics;

namespace _64AssaltCube.Renderers
{
    internal class Esp
    {
        public static void DrawnLine(Entidade inimigo)
        {
            Vector2 linestart = new Vector2(Programa.screenSize.X / 2, Programa.screenSize.Y / 2)+Renderer.WindowsPosition;
            Renderer.drawnList.AddLine(linestart, inimigo.Posicao2d+Renderer.WindowsPosition, ImGui.ColorConvertFloat4ToU32(inimigo.Color));
          
        }
        public static void DrawnBox(Entidade inimigo)
        {
            if (inimigo == null || inimigo.Posicao2d.X < 0 || inimigo.Posicao2d.Y < 0)
                return;

            float entyheight = (inimigo.Posicao2d.Y) - inimigo.Cabeca2d.Y;
            float boxleft = inimigo.Cabeca2d.X  - entyheight / 3;
            float boxRight = inimigo.Posicao2d.X +  + entyheight / 3;

            Vector2 rectTop = new Vector2(boxleft, inimigo.Cabeca2d.Y +  - entyheight * 0.1f) + Renderer.WindowsPosition;
            Vector2 rectBottom = new Vector2(boxRight, inimigo.Posicao2d.Y) + Renderer.WindowsPosition;

            Vector4 boxColor=inimigo.Color;

            


            Vector4 topColorVec = new Vector4(0.03f, 0.03f, 0.03f, 0f);
            Vector4 bottomColorVec = new Vector4(boxColor.X * 0.9f, boxColor.Y * 0.9f, boxColor.Z * 0.9f, 1.0f);

            uint colorTop = ImGui.ColorConvertFloat4ToU32(topColorVec);
            uint colorBottom = ImGui.ColorConvertFloat4ToU32(bottomColorVec);


                
                
                    Renderer.drawnList.AddRectFilledMultiColor(
                        rectTop,
                        rectBottom,
                        colorTop, colorTop,
                        colorBottom, colorBottom
                    );
                
                
        }

        public static void DrawnName(Entidade inimigo)
        {
            if (inimigo == null || inimigo.Posicao2d.X<0 || inimigo.Posicao2d.Y < 0)
                return;

            // altura do personagem na tela
            float entyheight = inimigo.Posicao2d.Y - inimigo.Cabeca2d.Y;
            float boxleft = inimigo.Cabeca2d.X - entyheight / 3;

            Vector2 rectTop = new Vector2(boxleft, inimigo.Cabeca2d.Y - entyheight * 0.1f);

            // garante que o nome nunca é nulo
            if (string.IsNullOrWhiteSpace(inimigo.Nome))
                inimigo.Nome = "NoNamed";

            // limita o tamanho do nome
            int maxLength = 15;
            if (inimigo.Nome.Length > maxLength)
                inimigo.Nome = inimigo.Nome.Substring(0, maxLength);

            string safeName = new string(inimigo.Nome
            .Where(c => c >= 32 && c <= 126) // só imprime ASCII visível
            .ToArray());

            if (string.IsNullOrWhiteSpace(safeName))
                safeName = "NoNamed";

            if (safeName.Length > 15)
                safeName = safeName.Substring(0, 15);

            Renderer.drawnList.AddText(rectTop + Renderer.WindowsPosition, ImGui.ColorConvertFloat4ToU32(inimigo.Color), safeName);
        }


        public static void DrawCross()
        {

            // drawnList.AddText(new Vector2(screenSize.X / 2, 2), ImGui.ColorConvertFloat4ToU32(circColor), $"X: {mirapos.X} Y:{mirapos.Y}");
            Vector2 n1 = new Vector2((Programa.screenSize.X / 2) - 10, (Programa.screenSize.Y / 2));
            Vector2 n2 = new Vector2((Programa.screenSize.X / 2) + 10, (Programa.screenSize.Y / 2));
            Vector2 n3 = new Vector2((Programa.screenSize.X / 2), (Programa.screenSize.Y / 2) - 10);
            Vector2 n4 = new Vector2((Programa.screenSize.X / 2), (Programa.screenSize.Y / 2) + 10);
            Renderer.drawnList.AddLine(n1 + Renderer.WindowsPosition, n2 + Renderer.WindowsPosition, ImGui.ColorConvertFloat4ToU32(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)));
            Renderer.drawnList.AddLine(n3 + Renderer.WindowsPosition, n4 + Renderer.WindowsPosition, ImGui.ColorConvertFloat4ToU32(new Vector4(1.0f, 0.0f, 0.0f, 1.0f)));
        }

    }
}
