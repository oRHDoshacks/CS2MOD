using System.Numerics;

namespace _64AssaltCube
{
    public class Entidade
    {
        public float[] ViewMatrix = new float[16];
        public IntPtr Endereco;
        public int Vida;
        public Vector3 Posicao3d;
        public Vector2 Posicao2d;
        public Vector2 Cabeca2d;
        public Vector3 Cabeca3d;
        public Vector3 Mira;
        public Vector4 Color;
        public int Fov;
        public int Time;
        public string Nome = "Nonamed";
        public int Arma;
    }
}
