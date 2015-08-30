using System.Drawing;
using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    internal interface IDrawable
    {
        void Draw(Point origin, PaintEventArgs e);
    }
}