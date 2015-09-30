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
        }

        public GameVector2(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static GameVector2 operator*(GameVector2 vector, int entier)
        {
            vector.X *= entier;
            vector.Y *= entier;
            return vector;
        }

        public static GameVector2 operator *(GameVector2 vector1, Size size)
        {
            vector1.X *= size.Width;
            vector1.Y *= size.Height;
            return vector1;
        }

        public override string ToString() => $"GameVector2 [{X}, {Y}]";
    }
}
