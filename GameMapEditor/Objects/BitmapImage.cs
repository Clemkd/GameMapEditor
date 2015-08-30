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

        public string Path
        {
            get;
            set;
        }

        public Bitmap BitmapSource
        {
            get;
            set;
        }

        public Bitmap BitmapSelection
        {
            get;
            set;
        }
    }
}
