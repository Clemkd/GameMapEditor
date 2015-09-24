using System;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public partial class MapLayerFrame : Form
    {
        public event MapLayerAddedEventArgs MapLayerAdded;

        public MapLayerFrame()
        {
            InitializeComponent();
        }

        private void MapLayerFrame_Load(object sender, EventArgs e)
        {
            this.textBoxName.Select();
        }

        private void checkBoxVisibleState_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxLayerState.ImageIndex = this.checkBoxLayerState.Checked ? 0 : 1;
        }

        private void checkBoxLayerType_CheckedChanged(object sender, EventArgs e)
        {
            this.checkBoxLayerType.ImageIndex = this.checkBoxLayerType.Checked ? 0 : 1;
        }

        private void buttonValidNewOverlay_Click(object sender, EventArgs e)
        {
            GameMapLayer layer = new GameMapLayer(this.textBoxName.Text)
            {
                Visible = this.checkBoxLayerState.Checked,
                Type = this.checkBoxLayerType.Checked ? LayerType.Upper : LayerType.Lower
            };

            this.MapLayerAdded?.Invoke(this, layer);
            this.Close();
        }
    }
}
