using System.Windows.Forms;

namespace GameMapEditor.Objects
{
    internal interface IDrawable
    {
        void Draw(PaintEventArgs e);
    }
}