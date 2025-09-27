
using System.Numerics;

namespace _64AssaltCube
{
    public class Calculate
    {

        public static Vector2 WordToScreen(float[] matrix, Vector3 pos, Vector2 windowSize)
        {
            float screenW = (matrix[3] * pos.X) + (matrix[7] * pos.Y) + (matrix[11] * pos.Z) + matrix[15];

            if (screenW > 0.001f)
            {
                float screenX = (matrix[0] * pos.X) + (matrix[4] * pos.Y) + (matrix[8] * pos.Z) + matrix[12];
                float screenY = (matrix[1] * pos.X) + (matrix[5] * pos.Y) + (matrix[9] * pos.Z) + matrix[13];

                float X = (windowSize.X / 2) + (windowSize.X / 2) * screenX / screenW;
                float Y = (windowSize.Y / 2) - (windowSize.Y / 2) * screenY / screenW;


                return new Vector2(X, Y);
            }
            else
            {
                return new Vector2(-99, -99);
            }

        }

        public static Vector2 Angles360(Vector3 a, Vector3 b)
        {
            float yaw;
            float pitch;

            // angulo de mira X
            float deltaX = b.X - a.X;
            float deltaY = b.Y - a.Y;
            yaw = (float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI));
            yaw = (yaw + 450f) % 360f;

            // angulo de mira Y
            float deltaZ = b.Z - a.Z;
            double distance = Math.Sqrt(Math.Pow(deltaX,2)+Math.Pow(deltaY,2));
            pitch = (float)(Math.Atan2(deltaZ, distance) * (180.0 / Math.PI));

            return new Vector2(yaw, pitch);
        }
        public static Vector2 Angles180(Vector3 a, Vector3 b)
        {
            float yaw; 
            float pitch; 
            
            // angulo de mira X
           float deltaX = b.X - a.X; 
           float deltaY = b.Y - a.Y; 
           yaw = -(float)(Math.Atan2(deltaY, deltaX) * (180.0 / Math.PI)); 
            
          // angulo de mira Y
          float deltaZ = b.Z - a.Z;
          double distance = Math.Sqrt(Math.Pow(deltaX,2)+Math.Pow(deltaY,2));
          pitch = (float)(Math.Atan2(deltaZ, distance) * (180.0 / Math.PI)); 
            
          //Retorno de Angulo
          return new Vector2(yaw, pitch); 
        }

    }
}
