using Swed64;
using System.Numerics;

namespace _64AssaltCube
{   
    
    public class Memory
    {
        public static Swed swed;
        public static IntPtr Modulo;
        public static IntPtr ListaEntidades;
        public static int NumeroDePlayers;

        public static void atach(string pName)
        {
            swed = new Swed(pName);
        }
        public static void ModuloAtach(string mName)
        {
            Modulo = swed.GetModuleBase(mName);
        }

        public static void EntidadeListaAt(int Offset)
        {
            int raw = swed.ReadInt(Modulo, Offset);
            ListaEntidades = new IntPtr((long)(uint)raw);
        }
        public static void NumeroDePlayersAt(int Offset)
        {
            NumeroDePlayers = swed.ReadInt(Modulo, Offset);
        }
        public static List<Entidade> GetEntidades()
        {
            List<Entidade> entidades = new List<Entidade>();
            for (int i = 1; i < NumeroDePlayers; i++)
            {
                Entidade entidade = new Entidade();
                int raw = swed.ReadInt(ListaEntidades, i * 4);
                entidade.Endereco = new IntPtr((long)(uint)raw);
                if(entidade.Endereco == IntPtr.Zero) { continue; }
                int Vida = swed.ReadInt(entidade.Endereco, Offsets.Vida);
                if (Vida == 0) { continue; }
                entidades.Add(entidade);
            }
            return entidades;
        }
        public static IntPtr PlayerLocal()
        {
            int raw = swed.ReadInt(Modulo, Offsets.PlayerLocal);
            return new IntPtr((long)(uint)raw);
        }
        public static Vector3 GetPosicao(IntPtr MPlayer)
        {
            return swed.ReadVec(MPlayer, Offsets.Cordenada);
        }
        public static int Vida(IntPtr MPlayer)
        {
            return swed.ReadInt(MPlayer, Offsets.Vida);
        }
        public static int Fov(IntPtr MPlayer)
        {
            return swed.ReadInt(MPlayer, Offsets.Fov);
        }
        public static int Time(IntPtr MPlayer)
        {
            return swed.ReadInt(MPlayer, Offsets.Time);
        }
        public static Vector3 Head(IntPtr MPlayer)
        {
            return swed.ReadVec(MPlayer, Offsets.Cabeca);
        }
        public static string Nome(IntPtr MPlayer)
        {
            
            try
            {
                // Lê no máximo 32 bytes (ajuste conforme o jogo armazena os nomes)
                string nome = swed.ReadString(MPlayer, Offsets.Nome, 16);

                if (string.IsNullOrWhiteSpace(nome))
                    return "NoNamed";

                // remove null terminators e caracteres de controle
                nome = nome.TrimEnd('\0', ' ', '\t');
                nome = new string(nome.Where(c => !char.IsControl(c)).ToArray());

                return string.IsNullOrEmpty(nome) ? "NoNamed" : nome;
            }
            catch (ArgumentException)
            {
                // Aqui cai exatamente no seu erro "destination array..."
                return "NoNamed";
            }
            catch
            {
                // fallback para qualquer outro erro inesperado
                return "NoNamed";
            }
        }
        public static int Arma(IntPtr MPlayer)
        {
            return swed.ReadInt(MPlayer, Offsets.Arma);
        }
        public static Vector3 Mira(IntPtr MPlayer)
        {
            return swed.ReadVec(MPlayer, Offsets.Mira);
        }
        public static float[] ViewMatrixAt()
        {
            float[] ViewMatrix = swed.ReadMatrix(Modulo+Offsets.ViewMatrix);
            
            return ViewMatrix;
        }
        public static Vector2 ScreenSize()
        {
            Vector2 sizer = new Vector2(swed.ReadInt(Modulo,Offsets.screan), swed.ReadInt(Modulo , Offsets.screan+0x4));
            return sizer;
        }
        public static void WriteVida(IntPtr MPlayer, int vida)
        {
            swed.WriteInt(MPlayer, Offsets.Vida, vida);
        }
        
        public static void AimBot(IntPtr MPlayer,Vector2 Mira)
        {
            swed.WriteFloat(MPlayer + Offsets.Mira,Mira.X);
            swed.WriteFloat(MPlayer + Offsets.Mira+0x4,Mira.Y);
        }
    }
}
