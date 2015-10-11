using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor
{
    [Serializable]
    [ProtoContract]
    public class GameVector2
    {
        [ProtoMember(1)]
        public int X { get; set; }
        [ProtoMember(2)]
        public int Y { get; set; }

        public GameVector2()
        {
            this.X = 0;
            this.Y = 0;
        }

        public GameVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is GameVector2))
                return false;
            if (obj == this)
                return true;

            GameVector2 vector = obj as GameVector2;
            if (vector.X == this.X && this.Y == vector.Y)
                return true;
            return false;
        }

        public static GameVector2 operator*(GameVector2 vector, int entier)
        {
            vector.X *= entier;
            vector.Y *= entier;
            return vector;
        }

        public static GameVector2 operator *(GameVector2 vector, float value)
        {
            vector.X = (int)(vector.X * value);
            vector.Y *= (int)(vector.Y * value);
            return vector;
        }

        public static GameVector2 operator *(GameVector2 vector1, Size size)
        {
            vector1.X *= size.Width;
            vector1.Y *= size.Height;
            return vector1;
        }

        public static GameVector2 operator +(GameVector2 vector1, GameVector2 vector2)
        {
            vector1.X += vector2.X;
            vector1.Y += vector2.Y;
            return vector1;
        }

        public override string ToString() => $"GameVector2 [{X}, {Y}]";
    }
}
