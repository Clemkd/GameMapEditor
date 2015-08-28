
using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    //[Serializable]
    class GameMap : IDrawable
    {
        private GameTile[,] map;

        public GameMap()
        {
            map = new GameTile[GlobalData.MapSize.Width, GlobalData.MapSize.Height];
            for(int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new GameTile(new Point(x, y));
                }
            }
        }

        public void Draw(PaintEventArgs e)
        {
            foreach(GameTile tile in this.map)
            {
                tile.Draw(e);
            }
        }

        public GameTile this[int x, int y]
        {
            get
            {
                
                if (x >= 0 && x < this.map.GetLength(0) && y >= 0 && y < this.map.GetLength(1))
                {
                    return this.map[x, y];
                }

                Debug.WriteLine("Position du tile en dehors du tableau de valeurs.");
                return null;
            }
            set
            {
                if (x > 0 && x < this.map.GetLength(0) && y > 0 && y < this.map.GetLength(1))
                {
                    this.map[x, y] = value;
                }
            }
        }


        public override string ToString()
        {
            StringBuilder strBuild = new StringBuilder();
            for(int y = 0; y < this.map.GetLength(1); y++)
            {
                
                for (int x = 0; x < this.map.GetLength(0); x++ )
                {
                    strBuild.Append(this.map[x, y].ToString());
                }
                strBuild.Append("\n");
            }

            return strBuild.ToString();
        }
    }
}
