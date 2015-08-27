using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    //[Serializable]
    class GameTileset
    {
        public GameTileset(Bitmap texture)
        {
            this.Texture = texture;
        }

        public int Index
        {
            get;
            set;
        }

        public Bitmap Texture
        {
            get;
            set;
        }

        public Bitmap GetCroppedTexture(Rectangle rectangle)
        {
            if (this.Texture != null)
            {
                return this.Texture.Clone(rectangle, PixelFormat.DontCare);
            }

            return null;
        }
    }
}
