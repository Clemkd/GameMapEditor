using System.Drawing;
using System.Windows.Forms;

namespace GameMapEditor
{
    internal interface IDrawable
    {
        void Draw(GameVector2 origin, PaintEventArgs e);
    }
}