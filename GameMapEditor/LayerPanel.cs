using GameMapEditor.Frames;
using GameMapEditor.Objects;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public partial class LayerPanel : DockContent, IDisposable
    {
        public event MapLayerAddedEventArgs MapLayerAdded;

        public LayerPanel()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.listViewLayers.Items.Clear();
        }

        public void AddLayer(GameMapLayer layer)
        {
            ListViewItem item = new ListViewItem()
            {
                Name = layer.Name,
                StateImageIndex = layer.Visible ? 0 : 1,
                ImageIndex = (int)layer.Type,    
                Group = this.listViewLayers.Groups[(int)layer.Type]        
            };

            item.SubItems.Add(layer.Name);
            this.listViewLayers.Items.Add(item);
        }

        private void toolStripButtonAddLayer_Click(object sender, System.EventArgs e)
        {
            MapLayerFormular formular = new MapLayerFormular();
            formular.MapLayerAdded += Formular_MapLayerAdded;
            formular.ShowDialog();
            formular.MapLayerAdded -= Formular_MapLayerAdded;
        }

        private void Formular_MapLayerAdded(GameMapLayer layer)
        {
            RaiseLayerAddedEvent(layer);
        }

        private void RaiseLayerAddedEvent(GameMapLayer layer)
        {
            if(this.MapLayerAdded != null)
            {
                this.MapLayerAdded(layer);
            }
        }

        private void listViewOverlay_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.BackColor = e.IsSelected ? System.Drawing.Color.LightBlue : System.Drawing.Color.WhiteSmoke;
            e.Item.SubItems[1].Text = e.IsSelected ? e.Item.Name + " (selectionnée)" : e.Item.Name;
        }
    }
}
