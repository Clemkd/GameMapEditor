using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    //[Serializable]
    class GameTile : IDrawable
    {
        private Bitmap texture; 

        public GameTile()
        {
            this.texture = null;
            this.Position = new Point();
        }

        public void Draw(PaintEventArgs e)
        {
            if(this.texture != null)
            {
                e.Graphics.DrawImage(this.texture, this.Position);
            }
        }

        public Bitmap Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Point Position
        {
            get;
            set;
        }
    }
}
