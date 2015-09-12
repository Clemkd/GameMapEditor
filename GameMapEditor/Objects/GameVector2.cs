using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor
{
    [ProtoContract]
    public class GameVector2
    {
        [ProtoMember(1)]
        public int X { get; set; }
        [ProtoMember(2)]
        public int Y { get; set; }

        public static GameVector2 operator*(GameVector2 vector, int entier)
        {
            vector.X *= entier;
            vector.Y *= entier;
            return vector;
        }

        public override string ToString() => $"[{X}, {Y}]";
    }
}
