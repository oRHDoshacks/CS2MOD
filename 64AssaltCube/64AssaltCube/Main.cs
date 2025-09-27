using _64AssaltCube.Renderers;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;

namespace _64AssaltCube
{
    public class Programa
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey); // Usa a API do Windows para detectar o estado das teclas


        public static Entidade player = new Entidade(); // Entidade do Jogador
        public static List<Entidade> inimigos = new List<Entidade>(); // Lista de Entidades do jogo
        public static string nameprocess = "ac_client"; // Nome do processo do jogo
        public static string Modulo1 = "ac_client.exe"; // Nome do modulo do jogo
        public static Vector2 screenSize = new Vector2(300,200); // Tamanho da tela
        public static void Main()
        {
            Renderer renderer = new Renderer();
            Thread renderThread = new Thread(new ThreadStart(renderer.Start().Wait));
            renderThread.Start();
            bool teclaPressionada = false; //Debounce Menu
            InjetarProcesso(nameprocess);
            screenSize = Memory.ScreenSize();
            Console.WriteLine($"Tamanho da tela: {screenSize.X}x{screenSize.Y}");
            while (true)
            {
                screenSize = Memory.ScreenSize();
                if ((GetAsyncKeyState(KeysConfig.MenuKey) & 0x8000) != 0)
                {
                    if (!teclaPressionada) // só executa se ainda não estava pressionada
                    {
                        Configs.Menu = !Configs.Menu;
                        teclaPressionada = true;
                    }
                }
                else
                {
                    teclaPressionada = false; // libera debounce quando solta
                }

                Process[] processos = Process.GetProcessesByName(nameprocess);
                if (processos.Length <= 0)
                { 
                    InjetarProcesso(nameprocess);
                }
                Memory.EntidadeListaAt(Offsets.ListaEntidades);
                Memory.NumeroDePlayersAt(Offsets.NumeroDePlayers);
                AtualizarPlayer();
                AtualizarInimigos();
                if (inimigos.Count > 0)
                {
                    if (Configs.Aimbot)
                    {
                        if ((GetAsyncKeyState(KeysConfig.AimbotKey) & 0x8000) != 0)
                        {
                            if (!teclaPressionada) // só executa se ainda não estava pressionada
                            {
                                Aimbot();
                               
                            }
                        }
                        
                    }
                }
                
                Thread.Sleep(1);
                
            }
            
        }
        public static void AtualizarPlayer()
        {
            player.Endereco = Memory.PlayerLocal();
            player.Vida = Memory.Vida(player.Endereco);
            player.Posicao3d = Memory.GetPosicao(player.Endereco);
            player.Fov = Memory.Fov(player.Endereco);
            player.Nome = Memory.Nome(player.Endereco);
            player.Cabeca3d = Memory.Head(player.Endereco);
            player.Time = Memory.Time(player.Endereco);
            player.Arma = Memory.Arma(player.Endereco);
            player.Mira = Memory.Mira(player.Endereco);
            player.ViewMatrix = Memory.ViewMatrixAt();
        }
        public static void AtualizarInimigos()
        {
            inimigos = Memory.GetEntidades();
            foreach (var inimigo in inimigos)
            {
                if (inimigo.Endereco == 0) { continue; }
                if (inimigo.Endereco == Memory.PlayerLocal()) { continue; }
                inimigo.Vida = Memory.Vida(inimigo.Endereco);
                inimigo.Posicao3d = Memory.GetPosicao(inimigo.Endereco);
                inimigo.Fov = Memory.Fov(inimigo.Endereco);
                inimigo.Nome = Memory.Nome(inimigo.Endereco);
                inimigo.Cabeca3d = Memory.Head(inimigo.Endereco);
                inimigo.Time = Memory.Time(inimigo.Endereco);
                inimigo.Arma = Memory.Arma(inimigo.Endereco);
                inimigo.Mira = Memory.Mira(inimigo.Endereco);
                inimigo.Cabeca2d = Calculate.WordToScreen(player.ViewMatrix, inimigo.Cabeca3d, screenSize);
                inimigo.Posicao2d = Calculate.WordToScreen(player.ViewMatrix, inimigo.Posicao3d, screenSize);
                
                if (inimigo.Time == player.Time)
                {
                    inimigo.Color = new Vector4(0.0f, 1.0f, 0.0f, 1.0f);
                }
                else
                {
                    inimigo.Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
                }
            }
        }
        public static void InjetarProcesso(string nomeProcesso)
        {
            bool porcessoEncontrado = false;
            while (!porcessoEncontrado)
            {
                Process[] processos = Process.GetProcessesByName(nomeProcesso);
                if (processos.Length > 0)
                {
                    Memory.atach(nomeProcesso);
                    Memory.ModuloAtach(Modulo1); // Vinculando o modulo do processo do jogo
                    Console.WriteLine("Injetado com sucesso");
                    porcessoEncontrado = true;
                }
                else
                {
                    Console.WriteLine("Aguardando o processo " + nomeProcesso + " iniciar...");
                    Thread.Sleep(300);
                    Console.Clear();
                }
            }
        }
        public static void Aimbot()
        {
            // ângulo atual da mira do player
            Vector3 viewAngles = player.Mira;

            var alvo = inimigos
                .Where(e => e.Vida > 0)
                .Where(e => e.Time != player.Time)
                .OrderBy(e =>
                {
                    // ângulo necessário para mirar no inimigo
                    Vector2 targetAngles = Calculate.Angles360(player.Cabeca3d, e.Cabeca3d);

                    // diferença angular (yaw/pitch)
                    float yawDiff = Math.Abs(targetAngles.X - viewAngles.X);
                    float pitchDiff = Math.Abs(targetAngles.Y - viewAngles.Y);

                    // retorna a "distância angular"
                    return Math.Sqrt(yawDiff * yawDiff + pitchDiff * pitchDiff);
                })
                .FirstOrDefault();

            if (alvo != null)
            {
             
                Vector2 Angles = Calculate.Angles360(player.Cabeca3d, alvo.Cabeca3d);
                Memory.AimBot(player.Endereco, Angles);
            }
        }

    }
}
