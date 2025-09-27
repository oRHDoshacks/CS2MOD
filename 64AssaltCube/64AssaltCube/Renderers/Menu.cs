using _64AssaltCube;
using _64AssaltCube.Renderers;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _64AssaltCube.Renderers
{
    internal class Menu
    {
        
        public static void DrawnMenu()
        {
            Vector2 separador = new Vector2(1, 1);
            ImGui.PushStyleVar(ImGuiStyleVar.Alpha, 1f);

            // Fundo da janela principal (verde escuro)
            ImGui.PushStyleColor(ImGuiCol.WindowBg, new Vector4(0.0f, 0.0f, 0.2f, 1f));

            ImGui.SetNextWindowSize(new Vector2(600, 300), ImGuiCond.FirstUseEver);
            ImGui.Begin("##MainWindow", ImGuiWindowFlags.NoCollapse
                | ImGuiWindowFlags.NoTitleBar
                | ImGuiWindowFlags.NoResize
                | ImGuiWindowFlags.NoScrollbar
                | ImGuiWindowFlags.NoScrollWithMouse
                | ImGuiWindowFlags.NoBringToFrontOnFocus
            );

            Vector2 windowSize = ImGui.GetWindowSize();
            Vector2 windowPos = ImGui.GetWindowPos();
            ImGui.PopStyleColor(); // remove cor da janela

            // Fundo do BeginChild (cinza esverdeado)
            ImGui.PushStyleColor(ImGuiCol.ChildBg, new Vector4(0.2f, 0.2f, 0.25f, 1f));

            ImGui.BeginChild("SideTabBar", new Vector2(0, 40));
            {
                int buttonCount = 4;
                float buttonWidth = 120f;
                float buttonHeight = 25f;
                float spacing = ImGui.GetStyle().ItemSpacing.X;
                float totalWidth = buttonCount * buttonWidth + (buttonCount - 1) * spacing;

                float startX = (ImGui.GetWindowWidth() - totalWidth) * 0.5f;
                float startY = (ImGui.GetWindowHeight() - buttonHeight) * 0.5f;

                ImGui.SetCursorPos(new Vector2(startX, startY));

                if (ImGui.Button("Menu Aimbot", new Vector2(buttonWidth, buttonHeight))) Renderer.selectedTab = 0;
                ImGui.SameLine();
                if (ImGui.Button("Menu Esp", new Vector2(buttonWidth, buttonHeight))) Renderer.selectedTab = 1;
                ImGui.SameLine();
                if (ImGui.Button("Menu Extras", new Vector2(buttonWidth, buttonHeight))) Renderer.selectedTab = 2;
                ImGui.SameLine();
                if (ImGui.Button("Menu Config", new Vector2(buttonWidth, buttonHeight))) Renderer.selectedTab = 3;
                ImGui.EndChild();
                if (Renderer.selectedTab == 0)
                {
                    ImGui.BeginChild("Aimbot1", new Vector2(110, 80));
                    ImGui.Dummy(separador);
                    ImGui.Dummy(separador);
                    ImGui.SameLine();
                    ImGui.Checkbox("Aimbot", ref Configs.Aimbot);
                    ImGui.EndChild();
                }

                if (Renderer.selectedTab == 1)
                {
                    ImGui.BeginChild("Aimbot1", new Vector2(125, 100));
                    ImGui.Dummy(separador);
                    ImGui.Dummy(separador);
                    ImGui.Checkbox("Esp Line", ref Configs.EspLine);
                    ImGui.Dummy(separador);
                    ImGui.Checkbox("Esp Box", ref Configs.EspBox);
                    ImGui.Dummy(separador);
                    ImGui.Checkbox("Esp Name", ref Configs.EspName);
                    ImGui.EndChild();
                }

                if (Renderer.selectedTab == 2)
                {
                    // futuro conteúdo
                }
            }
            ImGui.PopStyleColor(); // remove cor do child

            ImGui.End();
            ImGui.PopStyleVar();
        }
    }
}
