using System;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class BrowserPanel : DockContent
    {
        public static BrowserPanel Instance = new BrowserPanel();

        private BrowserPanel()
        {
            this.HideOnClose = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeComponent();
        }
    }
}
