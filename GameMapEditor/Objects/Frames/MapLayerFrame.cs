using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameMapEditor.Frames
{
    public delegate void MapLayerAddedEventArgs(object sender, GameMapLayer layer);

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
            this.textBoxName.Text = this.GetFreeLayerName(LayerPanel.Instance.LayerPane.ControlsName);
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
            if (this.textBoxName.Text.Length > 0)
            {
                GameMapLayer layer = new GameMapLayer(this.textBoxName.Text)
                {
                    Visible = this.checkBoxLayerState.Checked,
                    Type = this.checkBoxLayerType.Checked ? LayerType.Upper : LayerType.Lower
                };

                this.MapLayerAdded?.Invoke(this, layer);
                this.Close();
            }
            else
            {
                ConsolePanel.Instance.WriteLine("Impossible de créer une couche avec un nom vide", RowType.Error);
            }
        }

        private string GetFreeLayerName(List<string> existingNames)
        {
            int index = 1;
            string currentName = $"Couche {index}";
            while (existingNames.Exists(x => x == currentName))
            {
                index++;
                currentName = $"Couche {index}";
            }
            return currentName;
        }
    }
}
