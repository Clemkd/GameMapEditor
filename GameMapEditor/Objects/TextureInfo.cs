using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects
{
    public class TextureInfo
    {
        /// <summary>
        /// Le rectangle définissant la zone de selection du tileset
        /// </summary>
        public Rectangle Selection;

        public TextureInfo(string path, Bitmap bitmap)
        {
            this.Path = path;
            this.BitmapSource = bitmap;
            this.Selection = new Rectangle();
        }

        /// <summary>
        /// Le chemin du fichier tileset
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Le tileset source de l'objet
        /// </summary>
        public Bitmap BitmapSource
        {
            get;
            set;
        }

        /// <summary>
        /// La texture correspondant à la selection du tileset
        /// </summary>
        public Bitmap BitmapSelection
        {
            get;
            set;
        }
    }
}
