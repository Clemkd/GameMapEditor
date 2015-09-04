using System;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class MapBrowserPanel : DockContent
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeComponent();
        }
    }
}
