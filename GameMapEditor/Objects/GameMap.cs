
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    [Serializable]
    public class GameMap : IDrawable
    {
        private string name;
        private GameTile[,] map;

        public GameMap()
        {
            map = new GameTile[GlobalData.MapSize.Width, GlobalData.MapSize.Height];
            for(int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = new GameTile(x, y);
        }

        public GameTile this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < this.map.GetLength(0) && y >= 0 && y < this.map.GetLength(1))
                    return this.map[x, y];
                return null;
            }
            set
            {
                if (x > 0 && x < this.map.GetLength(0) && y > 0 && y < this.map.GetLength(1))
                    this.map[x, y] = value;
            }
        }

        public void Draw(Point origin, PaintEventArgs e)
        {
            foreach(GameTile tile in this.map)
                tile.Draw(origin, e);
        }

        /// <summary>
        /// Remplis la map par la texture selectionnée du tileset
        /// </summary>
        public void Fill(BitmapImage texture)
        {
            if (texture.BitmapSelection.Width >= GlobalData.TileSize.Width &&
                texture.BitmapSelection.Height >= GlobalData.TileSize.Height)
            {
                int tmpWidth = texture.BitmapSelection.Width / GlobalData.TileSize.Width;
                int tmpHeight = texture.BitmapSelection.Height / GlobalData.TileSize.Height;

                for (int y = 0; y < GlobalData.MapSize.Height; y += tmpHeight)
                    for (int x = 0; x < GlobalData.MapSize.Width; x += tmpWidth)
                        this.SetTiles(x, y, texture);
            }
        }

        /// <summary>
        /// Modifie de façon intelligente les données de la carte selon la selection du tileset, à partir de la position donnée
        /// </summary>
        /// <param name="position">La position du premier tile en haut à gauche à modifier</param>
        public void SetTiles(int xPosition, int yPosition, BitmapImage texture)
        {
            int tmpWidth = texture.BitmapSelection.Width / GlobalData.TileSize.Width;
            int tmpHeight = texture.BitmapSelection.Height / GlobalData.TileSize.Height;
            int tilesCount = texture.BitmapSource.Width / GlobalData.TileSize.Width;

            for (int x = 0; x < tmpWidth; x++)
            {
                for (int y = 0; y < tmpHeight; y++)
                {
                    GameTile tile = this[xPosition + x, yPosition + y];
                    if (tile != null)
                    {
                        Rectangle selection = new Rectangle(
                            GlobalData.TileSize.Width * x,
                            GlobalData.TileSize.Height * y,
                            GlobalData.TileSize.Width,
                            GlobalData.TileSize.Height);

                        GraphicsUnit unit = GraphicsUnit.Pixel;
                        RectangleF bounds = texture.BitmapSource.GetBounds(ref unit);

                        if (bounds.Contains(selection))
                        {
                            // OutOfMemory eventuel, par usage de textures inexistantes (OutOfRange extension)
                            tile.Texture = texture.BitmapSelection.Clone(selection, PixelFormat.DontCare);
                            
                            // Insertion des données serialisables
                            tile.FormattedIndex = GameTile.EncodeFormattedIndex(selection.Location, tilesCount);
                            tile.TextureFileName = texture.Path;
                        }
                    }
                }
            }
        }

        public void Save()
        {
            // TODO : Take care
            BinaryFormatter serializer = new BinaryFormatter();
            FileStream fs = new FileStream(string.Format("{0}.frog", this.name), FileMode.Create);
            serializer.Serialize(fs, this);
        }

        public static GameMap Load(string fileName)
        {
            try
            {
                BinaryFormatter deserializer = new BinaryFormatter();
                FileStream fileStream = new FileStream(fileName, FileMode.Open);
                return deserializer.Deserialize(fileStream) as GameMap;
            }
            catch(Exception ex) { Debug.WriteLine(ex.Message); }
            return null;
        }

        public List<string> FilesDependences
        {
            get
            {
                List<string> dependances = new List<string>();
                foreach (GameTile tile in this.map)
                {
                    if (!string.IsNullOrWhiteSpace(tile.TextureFileName) && !dependances.Exists(x => x == tile.TextureFileName))
                    {
                        dependances.Add(tile.TextureFileName);
                    }
                }
                return dependances;
            }
        }

        public string Name
        {
            get { return this.name ?? string.Empty; }
            set { this.name = value; }
        }
    }
}
