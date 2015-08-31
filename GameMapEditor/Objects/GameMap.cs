
using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace GameMapEditor
{
    public delegate void MapChangedEventArgs(object sender);

    [Serializable]
    public class GameMap : IDrawable
    {
        private string name;
        private Dictionary<string, int> textures;
        private GameTile[,] tiles;

        [field : NonSerialized]
        public event MapChangedEventArgs MapChanged;

        public GameMap(string mapName)
        {
            this.name = mapName;
            this.textures = new Dictionary<string, int>();
            this.tiles = new GameTile[GlobalData.MapSize.Width, GlobalData.MapSize.Height];
            this.InitializeComponents();
        }


        private void InitializeComponents()
        {
            for (int x = 0; x < tiles.GetLength(0); x++)
                for (int y = 0; y < tiles.GetLength(1); y++)
                    this.tiles[x, y] = new GameTile(x, y);
        }

        public GameTile this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < this.tiles.GetLength(0) && y >= 0 && y < this.tiles.GetLength(1))
                    return this.tiles[x, y];
                return null;
            }
            set
            {
                if (x > 0 && x < this.tiles.GetLength(0) && y > 0 && y < this.tiles.GetLength(1))
                    this.tiles[x, y] = value;
            }
        }

        public void Draw(Point origin, PaintEventArgs e)
        {
            foreach(GameTile tile in this.tiles)
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

                this.RaiseMapChangedEvent();
            }
        }

        /// <summary>
        /// Modifie de façon intelligente les données de la carte selon la selection du tileset, à partir de la position donnée
        /// </summary>
        /// <param name="position">La position du premier tile en haut à gauche à modifier</param>
        public void SetTiles(int xPosition, int yPosition, BitmapImage texture)
        {
            if (texture != null && texture.BitmapSelection != null)
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
                                Point location = new Point(
                                    (selection.Location.X + texture.SelectionLocation.X) / GlobalData.TileSize.Width,
                                    (selection.Location.Y + texture.SelectionLocation.Y) / GlobalData.TileSize.Height);

                                tile.FormattedIndex = GameTile.EncodeFormattedIndex(location, tilesCount);
                                
                                tile.TextureIndex = this.RetrieveTextureIndex(texture.Path);
                                Debug.WriteLine("\n-----   Position : {0}   -----\nIndex : {1}\nPath : {2}\n", new Point(xPosition + x, yPosition + y), tile.FormattedIndex, tile.TextureIndex);
                            }
                        }
                    }
                }

                this.RaiseMapChangedEvent();
            }
        }

        private int RetrieveTextureIndex(string texture)
        {
            int value = 0;
            if(!this.textures.ContainsKey(texture))
            {
                this.textures.Add(texture, this.textures.Count);
            }
            this.textures.TryGetValue(texture, out value);

            return value;
        }

        /// <summary>
        /// Sauvegarde la carte de jeu dans un fichier de données
        /// </summary>
        public void Save()
        {
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.AssemblyFormat = FormatterAssemblyStyle.Simple;
            FileStream fs = new FileStream(string.Format("{0}.frog", this.Name), FileMode.Create);
            serializer.Serialize(fs, this);
        }

        /// <summary>
        /// Charge la carte de jeu depuis un fichier de données
        /// </summary>
        /// <param name="fileName">Chemin suivi du nom et de l'extension du fichier de données</param>
        /// <returns></returns>
        public static GameMap Load(string fileName)
        {
            BinaryFormatter deserializer = new BinaryFormatter();
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            return deserializer.Deserialize(fileStream) as GameMap;
        }

        /// <summary>
        /// La liste de noms des fichiers de dépendances textures
        /// </summary>
        public List<string> FilesDependences
        {
            get { return textures.Keys.ToList(); }
        }

        private void RaiseMapChangedEvent()
        {
            if (this.MapChanged != null)
            {
                this.MapChanged(this);
            }
        }

        public string Name
        {
            get { return this.name ?? string.Empty; }
            set { this.name = value; }
        }

        // TODO : Debug only
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            for(int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    str.Append("[" + tiles[x, y].FormattedIndex.ToString() + "]");
                }
                str.Append("\n");
            }
            

            return str.ToString();
        }
    }
}
