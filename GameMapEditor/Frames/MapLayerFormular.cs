using GameMapEditor.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public delegate void MapLayerAddedEventArgs(GameMapLayer mapLayer);

    public partial class MapLayerFormular : Form
    {
        public event MapLayerAddedEventArgs MapLayerAdded;

        public MapLayerFormular()
        {
            InitializeComponent();
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
            GameMapLayer layer = new GameMapLayer()
            {
                Name = this.textBoxName.Text,
                Visible = this.checkBoxLayerState.Checked,
                Type = this.checkBoxLayerType.Checked ? LayerType.Upper : LayerType.Lower
            };
            
            this.RaiseMapLayerAddedEvent(layer);
            this.Close();
        }

        private void RaiseMapLayerAddedEvent(GameMapLayer layer)
        {
            if(this.MapLayerAdded != null)
            {
                this.MapLayerAdded(layer);
            }
        }
    }
}
