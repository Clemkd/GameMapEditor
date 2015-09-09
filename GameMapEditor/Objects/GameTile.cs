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

        /// <summary>
        /// Retourne l'index calculé par la position d'un tile sur sa planche de textures
        /// </summary>
        /// <param name="tileLocation">La position de la texture définie par le point supérieur gauche</param>
        /// <param name="tilesetWidthTilesCount">Le nombre de tiles total sur la largeur de la planche de textures</param>
        /// <returns>L'index calculé</returns>
        public static int EncodeFormattedIndex(Point tileLocation, int tilesetWidthTilesCount)
        {
            return tileLocation.X + tilesetWidthTilesCount * tileLocation.Y;
        }

        /// <summary>
        /// Retourne la position calculé du tile sur sa planche de textures
        /// </summary>
        /// <param name="index">L'index formatté du tile sur sa planche de textures</param>
        /// <param name="tilesetWidthTilesCount">Le nombre de tiles total sur la largeur de la planche de textures</param>
        /// <returns>La position calculé</returns>
        public static Point DecodeFormattedIndex(int index, int tilesetWidthTilesCount)
        {
            int nt = tilesetWidthTilesCount * GlobalData.TileSize.Width / GlobalData.TileSize.Width;
            int x = --index - (index / nt) * nt;
            int y = index / nt;

            return new Point(x, y);
        }

        /// <summary>
        /// Dessine le tile sur le controle de dessin à l'origine spécifiée
        /// </summary>
        /// <param name="origin">L'origine de dessin, définie par le point supérieur gauche</param>
        /// <param name="e">L'évènement du control de dessin</param>
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
        /// <summary>
        /// Obtient ou définit l'index formatté du tile
        /// </summary>
        public int FormattedIndex
        {
            get { return this.formattedIndex; }
            set { this.formattedIndex = value; }
        }

        /// <summary>
        /// Obtient ou définit l'index de la planche de textures
        /// </summary>
        public int TextureIndex
        {
            get { return this.textureIndex; }
            set { this.textureIndex = value; }
        }

        // TODO : Modifier en [field: NonSerialized] et charger la texture lors de l'ouverture
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
