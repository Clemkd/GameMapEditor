using GameMapEditor.Frames;
using GameMapEditor.Objects;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public delegate void MapLayerAddedEventArgs(GameMapLayer layer);
    public delegate void MapLayerSelectionChangedEventArgs(int index);
    public delegate void MapLayerIndexChangedEventArgs(int firstLayerIndex, int secondLayerIndex);

    public partial class LayerPanel : DockContent, IDisposable
    {
        public event MapLayerAddedEventArgs MapLayerAdded;
        public event MapLayerSelectionChangedEventArgs MapLayerSelectionChanged;
        public event MapLayerIndexChangedEventArgs MapLayerIndexChanged;

        public LayerPanel()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.listViewLayers.Items.Clear();
        }

        public void LoadFrom(GameMap map)
        {
            this.Clear();

            for (int i = map.Layers.Count - 1; i >= 0; i--)
                this.LoadLayer(map.Layers[i]);
        }

        public void LoadLayer(GameMapLayer layer)
        {
            ListViewItem item = new ListViewItem()
            {
                Name = layer.Name,
                StateImageIndex = layer.Visible ? 0 : 1,
                ImageIndex = (int)layer.Type/*,
                Group = this.listViewLayers.Groups[(int)layer.Type]*/
            };

            item.SubItems.Add(layer.Name);
            this.listViewLayers.Items.Insert(0, item);
            this.listViewLayers.Items[0].Selected = true;
        }

        public void RemoveLayer(int index)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Map.RemoveLayerAt(index);
                this.listViewLayers.SelectedItems[0].Remove();
            }
        }

        private void toolStripButtonAddLayer_Click(object sender, System.EventArgs e)
        {
            MapLayerFrame formular = new MapLayerFrame();
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
            this.MapLayerAdded?.Invoke(layer);
        }

        private void RaiseLayerSelectionChangedEvent(int index)
        {
            this.MapLayerSelectionChanged?.Invoke(index);
        }

        private void RaiseLayerIndexChangedEvent(int firstLayerIndex, int secondLayerIndex)
        {
            this.MapLayerIndexChanged?.Invoke(firstLayerIndex, secondLayerIndex);
        }

        private void listViewOverlay_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.BackColor = e.IsSelected ? Color.LightBlue : SystemColors.Window;
            e.Item.SubItems[1].Text = e.IsSelected ? "[ " + e.Item.Name + " ]" : e.Item.Name;

            if (e.IsSelected)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                GameMapLayer layer = mapPanel.Map?.GetLayerAt(e.ItemIndex);
                if (layer != null)
                {
                    this.toolStripButtonSetVisibleState.Image =  layer.Visible ? Properties.Resources.eye : Properties.Resources.eye_close;
                }

                this.RaiseLayerSelectionChangedEvent(e.ItemIndex);
            }
        }

        private void toolStripButtonUpLayer_Click(object sender, EventArgs e)
        {
            if (this.listViewLayers.SelectedItems.Count > 0)
            {
                ListViewItem item1 = this.listViewLayers.SelectedItems[0];
                int index1 = item1.Index;

                if (index1 - 1 >= 0)
                {
                    ListViewItem item2 = this.listViewLayers.Items[index1 - 1];
                    int index2 = item2.Index;

                    MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                    if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index2))
                    {
                        this.listViewLayers.Items.Remove(item2);
                        this.listViewLayers.Items.Remove(item1);
                        this.listViewLayers.Items.Insert(index2, item1);
                        this.listViewLayers.Items.Insert(index1, item2);
                    }
                }
            }
        }

        private void toolStripButtonDownLayer_Click(object sender, EventArgs e)
        {
            if(this.listViewLayers.SelectedItems.Count > 0)
            {
                ListViewItem item1 = this.listViewLayers.SelectedItems[0];
                int index1 = item1.Index;

                if (index1 + 1 < this.listViewLayers.Items.Count)
                {
                    ListViewItem item2 = this.listViewLayers.Items[index1 + 1];
                    int index2 = item2.Index;

                    MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                    if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index2))
                    {
                        this.listViewLayers.Items.Remove(item2);
                        this.listViewLayers.Items.Remove(item1);
                        this.listViewLayers.Items.Insert(index1, item2);
                        this.listViewLayers.Items.Insert(index2, item1);
                    }
                }
            }   
        }

        private void toolStripButtonRemoveLayer_Click(object sender, EventArgs e)
        {
            if (this.listViewLayers.SelectedItems.Count > 0)
            {
                int index = this.listViewLayers.SelectedItems[0].Index;
                this.RemoveLayer(index);
            }
        }

        private void toolStripButtonSetVisibleState_Click(object sender, EventArgs e)
        {
            if(this.listViewLayers.SelectedItems.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null)
                {
                    GameMapLayer layer = mapPanel.Map.GetLayerAt(this.listViewLayers.SelectedItems[0].Index);
                    if (layer != null)
                    {
                        layer.Visible = !layer.Visible;
                        this.toolStripButtonSetVisibleState.Image = layer.Visible ? Properties.Resources.eye : Properties.Resources.eye_close;
                        this.listViewLayers.SelectedItems[0].StateImageIndex = layer.Visible ? 0 : 1;
                    }
                }
            }
        }
    }
}
