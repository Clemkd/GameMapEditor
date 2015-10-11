using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Class.GameCharacter
{
    public class GameTimer
    {
        private int ticks;

        public GameTimer()
        {
            this.ticks = 0;
        }

        public bool AsyncWait(int ticks)
        {
            if(this.ticks >= ticks)
            {
                this.ticks = 0;
                return true;
            }
            this.ticks++;
            return false;
        }
    }
}
