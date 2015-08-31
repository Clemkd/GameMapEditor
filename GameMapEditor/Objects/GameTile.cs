using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    [Serializable]
    public class GameTile : IDrawable
    {
        private int formattedIndex;
        private int textureIndex;

        #region Methods
        public GameTile(int x, int y)
        {
            this.Position = new Point(x, y);
        }

        public static int EncodeFormattedIndex(Point tileLocation, int tilesetWidthTilesCount)
        {
            return tileLocation.X + tilesetWidthTilesCount * tileLocation.Y;
        }
        
        public static Point DecodeFormattedIndex(int index, int tilesetWidth)
        {
            int nt = tilesetWidth * GlobalData.TileSize.Width / GlobalData.TileSize.Width;
            int x = --index - (index / nt) * nt;
            int y = index / nt;

            return new Point(x, y);
        }

        public void Draw(Point origin, PaintEventArgs e)
        {
            if (this.Texture != null)
            {
                e.Graphics.DrawImage(this.Texture, new Rectangle(
                    this.Position.X * GlobalData.TileSize.Width - origin.X,
                    this.Position.Y * GlobalData.TileSize.Height - origin.Y,
                    GlobalData.TileSize.Width,
                    GlobalData.TileSize.Height));
            }
        }
        #endregion

        #region Properties
        public int FormattedIndex
        {
            get { return this.formattedIndex; }
            set { this.formattedIndex = value; }
        }

        public int TextureIndex
        {
            get { return this.textureIndex; }
            set { this.textureIndex = value; }
        }

        public Bitmap Texture
        {
            get;
            set;
        }

        public Point Position
        {
            get;
            set;
        }
        #endregion
    }
}
