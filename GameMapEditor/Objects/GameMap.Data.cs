using GameMapEditor.Objects;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor
{
    public partial class GameMap
    {
        private static Dictionary<string, Bitmap> Textures = new Dictionary<string, Bitmap>();
        private static Dictionary<int, Bitmap> CroppedTextures = new Dictionary<int, Bitmap>();

        /// <summary>
        /// Obtient la texture avec le nom spécifié
        /// </summary>
        /// <param name="filename">Le nom de la texture</param>
        /// <returns>La texture</returns>
        private static Bitmap GetTexture(string filename)
        {
            if(!Textures.ContainsKey(filename) && File.Exists(filename))
                Textures.Add(filename, Image.FromFile(filename) as Bitmap);

            Bitmap resultBitmap;
            if (Textures.TryGetValue(filename, out resultBitmap))
                return resultBitmap;
            else throw new FileNotFoundException("La ressource \"" + filename + "\" est inexistante.");
        }

        /// <summary>
        /// Obtient le tile à l'index spécifié sur la texture donnée
        /// </summary>
        /// <param name="index">L'index du tile</param>
        /// <param name="texture">La texture</param>
        /// <returns>Le tile</returns>
        private static Bitmap GetCroppedTexture(int index, Bitmap texture)
        {
            if (index == GameTile.EMPTY)
                return null;

            // Si le tile n'est pas en dictionnaire de données, on l'ajoute pour une utilisation future (évite de recharger la même texture)
            if (!CroppedTextures.ContainsKey(index))
            {
                GameVector2 vector = GameTile.DecodeFormattedIndex(index, texture.Width / GlobalData.TileSize.Width) * GlobalData.TileSize;
                CroppedTextures.Add(index, texture.Clone(new Rectangle(vector.X, vector.Y, GlobalData.TileSize.Width, GlobalData.TileSize.Height),
                    System.Drawing.Imaging.PixelFormat.DontCare));
            }

            Bitmap resultBitmap;
            if (CroppedTextures.TryGetValue(index, out resultBitmap))
                return resultBitmap;
            return null;
        }

        /// <summary>
        /// Charge les dépendances de la carte et la retourne en fin de chargement
        /// </summary>
        /// <returns>La carte chargée</returns>
        private async Task<GameMap> LoadDependences()
        {
            Task<Exception> task = Task.Run(() =>
            {
                foreach (GameMapLayer layer in layers)
                {
                    layer.LayerChanged += Layer_LayerChanged;

                    foreach (GameTile tile in layer.Tiles)
                    {
                        string texturesFilename = this.RetrieveTextureName(tile.TextureIndex);
                        if (texturesFilename != null)
                        {
                            try
                            {
                                tile.Texture = GetCroppedTexture(tile.FormattedIndex, GetTexture(texturesFilename));
                            }
                            catch (Exception ex)
                            {
                                return ex;
                            }
                        }
                    }
                }
                return null;
            });

            await task;

            // Si le resultat de la tâche est une exception
            if (task.Result != null)
            {
                throw task.Result;
            }

            // TODO : Debug only
            Console.WriteLine("Textures : {0}, CroppedTextures : {1}", Textures.Count, CroppedTextures.Count);

            return this;
        }
    }
}
