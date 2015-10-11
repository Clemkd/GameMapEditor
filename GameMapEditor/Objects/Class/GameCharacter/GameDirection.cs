using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Class.GameCharacter
{
    public class GameDirection
    {
        public GameDirection(byte index, GameVector2 vector)
        {
            this.Index = index;
            this.Vector = vector;
        }

        public byte Index
        {
            get;
            private set;
        }

        public GameVector2 Vector
        {
            get;
            private set;
        }

        public static GameDirection Down = new GameDirection(0, new GameVector2(0, 1));
        public static GameDirection Up = new GameDirection(3, new GameVector2(0, -1));
        public static GameDirection Right = new GameDirection(2, new GameVector2(1, 0));
        public static GameDirection Left = new GameDirection(1, new GameVector2(-1, 0));
    }
}
