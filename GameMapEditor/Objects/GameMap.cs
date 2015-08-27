
using System;
using System.Diagnostics;
using System.Drawing;
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
        }

        public void Draw(PaintEventArgs e)
        {
            foreach(GameTile tile in this.map)
            {
                tile.Draw(e);
            }
        }

        public GameTile Tile(Point position)
        {
            if(position.X > 0 && position.X < this.map.GetLength(0) &&
                position.Y > 0 && position.Y < this.map.GetLength(1))
            {
                return this.map[position.X, position.Y];
            }

            Debug.WriteLine("Position du tile en dehors du tableau de valeurs.");
            return null;
        }
    }
}
