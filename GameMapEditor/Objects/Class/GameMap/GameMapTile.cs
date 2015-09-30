using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GameMapEditor
{
    [Serializable]
    [ProtoContract]
    public class GameMapTile : IDrawable
    {
        /// <summary>
        /// Le tile vide
        /// </summary>
        public const int EMPTY = -1;

        [ProtoMember(1)]
        private int formattedIndex;
        [ProtoMember(2)]
        private int texturesetIndex;
        [ProtoMember(3)]
        private GameVector2 position;

        #region Methods
        // Protobuf constructor
        private GameMapTile()
        {
        }

        public GameMapTile(int formattedPosition)
        {
            this.FormattedIndex = EMPTY;
            this.Position = DecodeFormattedIndex(formattedPosition, GlobalData.MapSize.Width);
        }

        /// <summary>
        /// Retourne l'index calculé par la position d'un tile sur sa planche de textures
        /// </summary>
        /// <param name="tileLocation">La position de la texture définie par le point supérieur gauche</param>
        /// <param name="wvalue">Le nombre de tiles total sur la largeur de la planche de textures</param>
        /// <returns>L'index calculé</returns>
        public static int EncodeFormattedIndex(Point tileLocation, int wvalue)
        {
            return tileLocation.X + wvalue * tileLocation.Y;
        }

        /// <summary>
        /// Retourne la position calculé du tile sur sa planche de textures
        /// </summary>
        /// <param name="index">L'index formatté du tile sur sa planche de textures</param>
        /// <param name="wvalue">Le nombre de tiles total sur la largeur de la planche de textures</param>
        /// <returns>La position calculé</returns>
        public static GameVector2 DecodeFormattedIndex(int index, int wvalue)
        {
            int nt = wvalue * GlobalData.TileSize.Width / GlobalData.TileSize.Width;
            int x = index - (index / nt) * nt;
            int y = index / nt;

            return new GameVector2(x, y);
        }

        /// <summary>
        /// Dessine le tile sur le controle de dessin à l'origine spécifiée
        /// </summary>
        /// <param name="origin">L'origine de dessin, définie par le point supérieur gauche</param>
        /// <param name="e">L'évènement du control de dessin</param>
        public void Draw(GameVector2 origin, PaintEventArgs e)
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

        // TODO : Debug Only
        public override string ToString()
        {
            return "[" + this.FormattedIndex.ToString() + "]";
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
            get { return this.texturesetIndex; }
            set { this.texturesetIndex = value; }
        }

        /// <summary>
        /// Obtient ou définit la texture du tile
        /// </summary>
        public Bitmap Texture
        {
            get;
            set;
        }

        /// <summary>
        /// Obtient ou définit la position relative du tile sur la carte
        /// </summary>
        public GameVector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        #endregion
    }
}
