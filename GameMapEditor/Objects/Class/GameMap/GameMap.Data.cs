using GameMapEditor.Objects;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static Rectangle DESTINATION_RECT_32 = new Rectangle(0, 0, 32, 32);

        private static Dictionary<string, Bitmap> Textures = new Dictionary<string, Bitmap>();
        private static Dictionary<string, Bitmap> CroppedTextures = new Dictionary<string, Bitmap>();

        /// <summary>
        /// Génère la clé de la texture en fonction de son index et de sa palette de textures parente 
        /// </summary>
        /// <param name="tileIndex">L'index de la texture</param>
        /// <param name="textureIndex">L'index de la palette de textures</param>
        /// <returns>La clé de la texture</returns>
        private static string RetrieveCroppedKey(int tileIndex, int textureIndex)
        {
            return string.Format("{0}:{1}", tileIndex, textureIndex);
        }

        /// <summary>
        /// Obtient la texture avec le nom spécifié
        /// </summary>
        /// <param name="filename">Le nom de la texture</param>
        /// <returns>La texture</returns>
        private Bitmap GetTexture(string filename)
        {
            if(!Textures.ContainsKey(filename) && File.Exists(filename))
                Textures.Add(filename, Image.FromFile(filename) as Bitmap);

            Bitmap resultBitmap;
            if (Textures.TryGetValue(filename, out resultBitmap))
                return resultBitmap;
            else throw new FileNotFoundException("La ressource \"" + filename + "\" est introuvable");
        }

        /// <summary>
        /// Obtient le tile à l'index spécifié sur la texture donnée
        /// </summary>
        /// <param name="tileIndex">L'index du tile</param>
        /// <param name="texture">La texture</param>
        /// <returns>Le tile</returns>
        private Bitmap GetCroppedTexture(int tileIndex, string textureName)
        {
            if (tileIndex == GameMapTile.EMPTY)
                return null;

            int textureIndex = this.RetrieveTextureIndex(textureName);
            string croppedKey = RetrieveCroppedKey(tileIndex, textureIndex);

            // Si le tile n'est pas en dictionnaire de données, on l'ajoute pour une utilisation future (évite de recharger la même texture)
            if (!CroppedTextures.ContainsKey(croppedKey))
            {
                Bitmap texture = this.GetTexture(textureName);
                Bitmap croppedTexture = new Bitmap(GlobalData.TileSize.Width, GlobalData.TileSize.Height);
                Graphics graphics = Graphics.FromImage(croppedTexture);
                GameVector2 vector = GameMapTile.DecodeFormattedIndex(tileIndex, texture.Width / GlobalData.TileSize.Width) * GlobalData.TileSize;
                Rectangle section = new Rectangle(vector.X, vector.Y, GlobalData.TileSize.Width, GlobalData.TileSize.Height);

                graphics.DrawImage(texture, DESTINATION_RECT_32, section, GraphicsUnit.Pixel);
                CroppedTextures.Add(croppedKey, croppedTexture);
            }

            Bitmap resultBitmap;
            if (CroppedTextures.TryGetValue(croppedKey, out resultBitmap))
                return resultBitmap;
            return null;
        }

        /// <summary>
        /// Charge les dépendances de la carte et la retourne en fin de chargement
        /// </summary>
        /// <returns>La carte chargée</returns>
        private async Task<GameMap> LoadDependences()
        {
            await Task.Run(() =>
            {
                
                foreach (GameMapLayer layer in layers)
                {
                    layer.LayerChanged += Layer_LayerChanged;

                    foreach (GameMapTile tile in layer.Tiles)
                    {
                        string texturesFilename = this.RetrieveTextureName(tile.TextureIndex);
                        if (texturesFilename != null)
                        {
                            tile.Texture = GetCroppedTexture(tile.FormattedIndex, texturesFilename);
                        }
                    }
                }
                
            });

            // TODO : Debug only
            Console.WriteLine("Textures : {0}, CroppedTextures : {1}", Textures.Count, CroppedTextures.Count);

            return this;
        }
    }
}
