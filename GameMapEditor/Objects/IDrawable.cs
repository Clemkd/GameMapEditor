using System.Drawing;
using System.Windows.Forms;

namespace GameMapEditor
{
    internal interface IDrawable
    {
        void Draw(Point origin, PaintEventArgs e);
    }
}