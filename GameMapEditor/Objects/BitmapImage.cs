using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects
{
    public class BitmapImage
    {
        public BitmapImage(string path, Bitmap bitmap)
        {
            this.Path = path;
            this.BitmapSource = bitmap;
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

        /// <summary>
        /// La position de la selection du tileset, identifiée par le point supérieur gauche
        /// </summary>
        public Point SelectionLocation
        {
            get;
            set;
        }
    }
}
