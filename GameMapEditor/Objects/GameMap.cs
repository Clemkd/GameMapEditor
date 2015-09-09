
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
        [field: NonSerialized]
        public event MapChangedEventArgs MapChanged;

        [NonSerialized]
        private const ushort MAX_LAYER_COUNT = 10;

        #region Fields
        private string name;
        private Dictionary<string, int> textures;
        private List<GameMapLayer> layers;
        #endregion

        public GameMap(string mapName)
        {
            this.name = mapName;
            this.textures = new Dictionary<string, int>();
            this.layers = new List<GameMapLayer>();
            this.InitializeComponents();
        }

        /// <summary>
        /// Initialise les données internes de la carte
        /// </summary>
        private void InitializeComponents()
        {

            GameMapLayer layer = new GameMapLayer()
            {
                Name = "Défaut",
                Type = LayerType.Lower,
                Visible = true
            };
            layer.LayerChanged += Layer_LayerChanged;
            this.layers.Add(layer);
        }

        /// <summary>
        /// Lie les évènements de l'objet avec ses sous-objets
        /// </summary>
        /// <returns>L'objet</returns>
        public GameMap LinkEvents()
        {
            this.layers.ForEach(layer =>
            {
                layer.LayerChanged += Layer_LayerChanged;
            });
            return this;
        }

        /// <summary>
        /// Dessine la map sur le controle de dessin à l'origine spécifiée
        /// </summary>
        /// <param name="origin">L'origine de dessin, définie par le point supérieur gauche</param>
        /// <param name="e">L'évènement du control de dessin</param>
        public void Draw(Point origin, PaintEventArgs e)
        {
            for(int i = this.layers.Count - 1; i >= 0; i--)
            {
                this.layers.ElementAt(i).Draw(origin, e);
            }
        }

        /// <summary>
        /// Obtient le layer à l'index spécifié et retourne null si inexistante
        /// </summary>
        /// <param name="index">L'index de le layer</param>
        /// <returns>Le layer</returns>
        public GameMapLayer GetLayerAt(int index)
        {
            if(index >= 0 && index < this.layers.Count)
            {
                return this.layers.ElementAt(index);
            }
            return null;
        }

        /// <summary>
        /// Ajoute une layer de dessin dans la map
        /// </summary>
        /// <param name="layer">Le layer à ajouter</param>
        /// <returns>L'état résultant de l'ajout</returns>
        public bool AddLayer(GameMapLayer layer)
        {
            return this.InsertLayerAt(0, layer);
        }

        /// <summary>
        /// Insert le layer à la position spécifiée dans la liste des layers de map
        /// </summary>
        /// <param name="index">L'index d'insertion</param>
        /// <param name="layer">Le layer à insérer</param>
        /// <returns>L'état résultant de l'insertion</returns>
        public bool InsertLayerAt(int index, GameMapLayer layer)
        {
            if (this.layers.Count < MAX_LAYER_COUNT)
            {
                layer.LayerChanged += Layer_LayerChanged;
                this.layers.Insert(index, layer);
                this.RaiseMapChangedEvent();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Supprime le layer à l'index spécifiée
        /// </summary>
        /// <param name="index">L'index de le layer à supprimer</param>
        /// <returns>L'état résultant de la suppression</returns>
        public bool RemoveLayerAt(int index)
        {
            if(index >= 0 && index < this.layers.Count)
            {
                this.layers.ElementAt(index).LayerChanged -= Layer_LayerChanged;
                this.layers.RemoveAt(index);
                this.RaiseMapChangedEvent();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remplace le layer à l'index spcifiée par le layer donnée
        /// </summary>
        /// <param name="index">L'index de le layer à remplacer</param>
        /// <param name="layer">Le layer remplaçante</param>
        /// <returns>L'état resultant du remplacement</returns>
        public bool ReplaceLayerAt(int index, GameMapLayer layer)
        {
            if(this.RemoveLayerAt(index))
                return this.InsertLayerAt(index, layer);
            return false;
        }

        /// <summary>
        /// Réalise un échange de position entre deux layers
        /// </summary>
        /// <param name="index1">L'index de la première layer</param>
        /// <param name="index2">L'index de la seconde layer</param>
        /// <returns>L'état résultant de l'échange</returns>
        public bool SwapLayers(int index1, int index2)
        {
            if (index1 == index2)
                return true;

            if (index1 < this.layers.Count && index2 < this.layers.Count && index1 >= 0 && index2 >= 0)
            {
                GameMapLayer layer1 = this.layers.ElementAt(index1);
                GameMapLayer layer2 = this.layers.ElementAt(index2);
                if (!this.ReplaceLayerAt(index1, layer2)) return false;
                if (!this.ReplaceLayerAt(index2, layer1)) return false;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remplis la map par la texture selectionnée du tileset
        /// </summary>
        public void Fill(int layerIndex, BitmapImage texture)
        {
            if (texture.BitmapSelection.Width >= GlobalData.TileSize.Width &&
                texture.BitmapSelection.Height >= GlobalData.TileSize.Height)
            {
                int tmpWidth = texture.BitmapSelection.Width / GlobalData.TileSize.Width;
                int tmpHeight = texture.BitmapSelection.Height / GlobalData.TileSize.Height;

                for (int y = 0; y < GlobalData.MapSize.Height; y += tmpHeight)
                    for (int x = 0; x < GlobalData.MapSize.Width; x += tmpWidth)
                        this.SetTiles(layerIndex, x, y, texture);

                this.RaiseMapChangedEvent();
            }
        }

        /// <summary>
        /// Modifie de façon intelligente les données de la carte selon la selection du tileset, à partir de la position donnée
        /// </summary>
        /// <param name="position">La position du premier tile en haut à gauche à modifier</param>
        public void SetTiles(int layerIndex, int xPosition, int yPosition, BitmapImage texture)
        {
            if (texture != null && texture.BitmapSelection != null && layerIndex >= 0 && layerIndex < this.layers.Count)
            {
                int tmpWidth = texture.BitmapSelection.Width / GlobalData.TileSize.Width;
                int tmpHeight = texture.BitmapSelection.Height / GlobalData.TileSize.Height;
                int tilesCount = texture.BitmapSource.Width / GlobalData.TileSize.Width;

                for (int x = 0; x < tmpWidth; x++)
                {
                    for (int y = 0; y < tmpHeight; y++)
                    {
                        GameTile tile = this.layers.ElementAt(layerIndex)[xPosition + x, yPosition + y];
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
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retourne l'index unique de la texture si existante, sinon créer un nouvel index
        /// </summary>
        /// <param name="texture">Le nom de fichier de la texture</param>
        /// <returns>L'index de la texture</returns>
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
            using (FileStream fs = new FileStream(string.Format("{0}.frog", this.Name), FileMode.Create))
            {
                serializer.Serialize(fs, this);
            }
        }

        /// <summary>
        /// Charge la carte de jeu depuis un fichier de données
        /// </summary>
        /// <param name="fileName">Chemin suivi du nom et de l'extension du fichier de données</param>
        /// <returns></returns>
        public static GameMap Load(string fileName)
        {
            BinaryFormatter deserializer = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                return deserializer.Deserialize(fileStream) as GameMap;
            }
        }

        private void Layer_LayerChanged(object sender)
        {
            this.RaiseMapChangedEvent();
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

        /// <summary>
        /// Obtient ou définit le nom de la map
        /// </summary>
        public string Name
        {
            get { return this.name ?? string.Empty; }
            set { this.name = value; }
        }

        /// <summary>
        /// La liste de layer de la map
        /// </summary>
        public List<GameMapLayer> Layers
        {
            get { return this.layers; }
        }
    }
}
