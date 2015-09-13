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
        private static Dictionary<String, Bitmap> Textures = new Dictionary<string, Bitmap>();
        private static Dictionary<int, Bitmap> CroppedTextures = new Dictionary<int, Bitmap>();

        private static Bitmap GetTexture(string filename)
        {
            if(!Textures.ContainsKey(filename) && File.Exists(filename))
                Textures.Add(filename, Bitmap.FromFile(filename) as Bitmap);

            Bitmap resultBitmap;
            if (Textures.TryGetValue(filename, out resultBitmap))
                return resultBitmap;
            else throw new FileNotFoundException("La ressource \"" + filename + "\" est inexistante.");
        }

        private static Bitmap GetCroppedTexture(int index, Bitmap palette)
        {
            if (index == GameTile.EMPTY)
                return null;

            if (!CroppedTextures.ContainsKey(index))
            {
                GameVector2 vector = GameTile.DecodeFormattedIndex(index, palette.Width / GlobalData.TileSize.Width) * 32;
                CroppedTextures.Add(index, palette.Clone(new Rectangle(vector.X, vector.Y, GlobalData.TileSize.Width, GlobalData.TileSize.Height),
                    System.Drawing.Imaging.PixelFormat.DontCare));
            }

            Bitmap resultBitmap;
            if (CroppedTextures.TryGetValue(index, out resultBitmap))
                return resultBitmap;
            return null;
        }

        private async Task<GameMap> LoadDependences()
        {
            Task<Exception> task = Task<Exception>.Run(() =>
            {
                foreach (GameMapLayer layer in layers)
                {
                    layer.LayerChanged += Layer_LayerChanged;

                    foreach (GameTile tile in layer.Tiles)
                    {
                        System.Threading.Thread.Sleep(1);
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

            return this;
        }
    }
}
