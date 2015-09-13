using GameMapEditor.Frames;
using GameMapEditor.Objects;
using GameMapEditor.Objects.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace GameMapEditor
{
    public delegate void MapLayerAddedEventArgs(GameMapLayer layer);
    public delegate void MapLayerSelectionChangedEventArgs(int index);

    public partial class LayerPanel : DockContent, IDisposable
    {
        public event MapLayerAddedEventArgs MapLayerAdded;
        public event MapLayerSelectionChangedEventArgs MapLayerSelectionChanged;

        public LayerPanel()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            this.layerPanelCTM.Controls.Clear();
        }

        public void LoadFrom(GameMap map)
        {
            this.Clear();

            foreach (GameMapLayer layer in map?.Layers)
            {
                this.layerPanelCTM.Add(layer);
            }
        }

        public void LoadLayer(GameMapLayer layer)
        {
            this.layerPanelCTM.Add(0, layer);
        }

        public void RemoveLayer(int index)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            if (mapPanel != null)
            {
                mapPanel.Map.RemoveLayerAt(index);
                this.layerPanelCTM.Remove(index);
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

        private void toolStripButtonUpLayer_Click(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                int index1 = this.layerPanelCTM.SelectedIndex;
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index1 - 1))
                {
                    this.layerPanelCTM.Swap(index1, index1 - 1);
                }
            }
        }

        private void toolStripButtonDownLayer_Click(object sender, EventArgs e)
        {
            if(this.layerPanelCTM.Controls.Count > 0)
            {
                int index1 = this.layerPanelCTM.SelectedIndex;
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null && mapPanel.Map.SwapLayers(index1, index1 + 1))
                {
                    this.layerPanelCTM.Swap(index1, index1 + 1);
                }
            }   
        }

        private void toolStripButtonRemoveLayer_Click(object sender, EventArgs e)
        {
            if (this.layerPanelCTM.Controls.Count > 0)
            {
                this.RemoveLayer(this.layerPanelCTM.SelectedIndex);
            }
        }

        private void toolStripButtonSetVisibleState_Click(object sender, EventArgs e)
        {
            if(this.layerPanelCTM.Controls.Count > 0)
            {
                MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
                if (mapPanel != null)
                {
                    GameMapLayer layer = mapPanel.Map?.GetLayerAt(this.layerPanelCTM.SelectedIndex);
                    if (layer != null)
                    {
                        layer.Visible = !layer.Visible;
                        this.toolStripButtonSetVisibleState.Image = layer.Visible ? Properties.Resources.eye : Properties.Resources.eye_close;
                        this.layerPanelCTM.SelectedControl().Visible = layer.Visible;
                    }
                }
            }
        }

        private void layerPanelCTM_ItemSelectionChanged(object sender, int index)
        {
            MapPanel mapPanel = DockPanel.ActiveDocument as MapPanel;
            GameMapLayer layer = mapPanel.Map?.GetLayerAt(index);
            if (layer != null)
            {
                this.toolStripButtonSetVisibleState.Image = layer.Visible ? Properties.Resources.eye : Properties.Resources.eye_close;
            }

            this.RaiseLayerSelectionChangedEvent(index);
        }
    }
}
